// src/BusinessLogic/Services/UserService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BusinessLogic.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.Extensions.Logging;
using UserManagementSystem.DataAccess.Exceptions;
using UserManagementSystem.BusinessLogic.Exceptions;

namespace BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IEmailService emailService, ILogger<UserService> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private T ExecuteServiceOperation<T>(Func<T> operation, string operationName)
        {
            try
            {
                return operation();
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during {OperationName}", operationName);
                throw new BusinessLogicException($"An unexpected error occurred during {operationName}.", ex);
            }
        }

        private async Task ExecuteServiceOperationAsync(Func<Task> operation, string operationName)
        {
            try
            {
                await operation();
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during {OperationName}", operationName);
                throw new BusinessLogicException($"An unexpected error occurred during {operationName}.", ex);
            }
        }

        private async Task<T> ExecuteServiceOperationAsync<T>(Func<Task<T>> operation, string operationName)
        {
            try
            {
                return await operation();
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during {OperationName}", operationName);
                throw new BusinessLogicException($"An unexpected error occurred during {operationName}.", ex);
            }
        }

        private void ExecuteServiceOperation(Action operation, string operationName)
        {
            try
            {
                operation();
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during {OperationName}", operationName);
                throw new BusinessLogicException($"An unexpected error occurred during {operationName}.", ex);
            }
        }

        public void CrearPersona(PersonaRequest request) => ExecuteServiceOperation(() =>
        {
            _logger.LogInformation("Iniciando la creación de la persona con legajo: {Legajo}", request.Legajo);

            if (!int.TryParse(request.Legajo, out int legajo))
            {
                _logger.LogWarning("El legajo '{Legajo}' no es un número válido.", request.Legajo);
                throw new ValidationException("El legajo debe ser un número válido.");
            }

            if (string.IsNullOrWhiteSpace(request.Nombre))
                throw new ValidationException("El nombre no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(request.Apellido))
                throw new ValidationException("El apellido no puede estar vacío.");

            if (!long.TryParse(request.NumDoc, out _))
            {
                _logger.LogWarning("El número de documento '{NumDoc}' no es numérico.", request.NumDoc);
                throw new ValidationException("El número de documento debe ser numérico.");
            }

            if (!long.TryParse(request.Cuil, out _))
            {
                _logger.LogWarning("El CUIL '{Cuil}' no es numérico.", request.Cuil);
                throw new ValidationException("El CUIL debe ser numérico.");
            }

            if (string.IsNullOrWhiteSpace(request.Calle))
                throw new ValidationException("La calle no puede estar vacía.");

            if (!int.TryParse(request.Altura, out _))
            {
                _logger.LogWarning("La altura '{Altura}' no es un número.", request.Altura);
                throw new ValidationException("La altura de la dirección debe ser un número.");
            }

            if (string.IsNullOrWhiteSpace(request.Correo) || !IsValidEmail(request.Correo))
                throw new ValidationException("El formato del correo electrónico no es válido.");

            if (!int.TryParse(request.Localidad, out int localidadId))
            {
                _logger.LogWarning("El ID de localidad '{Localidad}' no es válido.", request.Localidad);
                throw new ValidationException("El ID de localidad no es válido.");
            }

            _logger.LogInformation("Mapeando PersonaRequest a la entidad Persona.");
            var persona = new Persona
            {
                Legajo = legajo,
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                IdTipoDoc = _userRepository.GetTipoDocByNombre(request.TipoDoc)?.IdTipoDoc ?? throw new ValidationException("Tipo de documento no encontrado"),
                NumDoc = request.NumDoc,
                FechaNacimiento = request.FechaNacimiento,
                Cuil = request.Cuil,
                Calle = request.Calle,
                Altura = request.Altura,
                IdLocalidad = localidadId,
                IdGenero = _userRepository.GetGeneroByNombre(request.Genero)?.IdGenero ?? throw new ValidationException("Género no encontrado"),
                Correo = request.Correo,
                Celular = request.Celular,
                FechaIngreso = request.FechaIngreso
            };

            _logger.LogInformation("Llamando a AddPersona en el repositorio.");
            _userRepository.AddPersona(persona);
            _logger.LogInformation("Persona creada con éxito en el repositorio.");
        }, "creating a person");

        public void CrearUsuario(UserRequest request) => ExecuteServiceOperation(() =>
        {
            var persona = _userRepository.GetPersonaById(int.Parse(request.PersonaId))
                ?? throw new ValidationException("Persona no encontrada");

            if (string.IsNullOrWhiteSpace(persona.Correo))
            {
                throw new ValidationException("La persona seleccionada no tiene un correo electrónico para enviar la contraseña.");
            }

            string passwordToUse = GenerateRandomPassword(request.Username, persona);

            var usuario = new Usuario
            {
                IdPersona = int.Parse(request.PersonaId),
                UsuarioNombre = request.Username,
                ContrasenaScript = HashUsuarioContrasena(request.Username, passwordToUse),
                IdRol = _userRepository.GetRolByNombre(request.Rol)?.IdRol ?? throw new ValidationException("Rol no encontrado"),
                FechaUltimoCambio = DateTime.Now,
                FechaBloqueo = new DateTime(9999, 12, 31),
                CambioContrasenaObligatorio = true // Forzar cambio de contraseña en el primer login
            };
            _userRepository.AddUsuario(usuario);

            var task = _emailService.SendPasswordResetEmailAsync(persona.Correo, passwordToUse);
            task.ContinueWith(t => {
                if (t.IsFaulted)
                {
                    _logger.LogError(t.Exception, "Failed to send password reset email.");
                }
            });
        }, "creating a user");

        public async Task<AuthenticationResult> AuthenticateAsync(string username, string password)
        {
            return await ExecuteServiceOperationAsync(async () =>
            {
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                    return AuthenticationResult.Failed("Usuario o contraseña no pueden estar vacíos.");

                var usuario = _userRepository.GetUsuarioByNombreUsuario(username);
                if (usuario == null)
                    return AuthenticationResult.Failed("Usuario o contraseña incorrectos.");

                var hash = HashUsuarioContrasena(username, password);
                if (!hash.SequenceEqual(usuario.ContrasenaScript))
                    return AuthenticationResult.Failed("Usuario o contraseña incorrectos.");

                if (usuario.FechaBloqueo < DateTime.Now)
                {
                    return AuthenticationResult.Failed("La cuenta se encuentra deshabilitada.");
                }

                if (usuario.FechaExpiracion.HasValue && usuario.FechaExpiracion.Value < DateTime.Now)
                {
                    return AuthenticationResult.Failed("La cuenta ha expirado.");
                }

                var politica = _userRepository.GetPoliticaSeguridad();
                if (politica?.Autenticacion2FA ?? false)
                {
                    var persona = _userRepository.GetPersonaById(usuario.IdPersona);
                    if (persona == null || string.IsNullOrWhiteSpace(persona.Correo))
                    {
                        return AuthenticationResult.Failed("No se puede usar 2FA sin un correo configurado.");
                    }

                    var code = new Random().Next(100000, 999999).ToString();
                    var expiry = DateTime.UtcNow.AddMinutes(5);

                    _userRepository.Set2faCode(username, code, expiry);
                    await _emailService.Send2faCodeEmailAsync(persona.Correo, code);

                    return AuthenticationResult.TwoFactorRequired();
                }

                var userResponse = new UserResponse
                {
                    Username = usuario.UsuarioNombre,
                    Rol = usuario.Rol?.Nombre,
                    CambioContrasenaObligatorio = usuario.CambioContrasenaObligatorio,
                    IdPersona = usuario.IdPersona
                };

                var nextAction = PostLoginAction.None;
                if (usuario.CambioContrasenaObligatorio)
                {
                    nextAction = PostLoginAction.ChangePassword;
                }
                else if (userResponse.Rol == "Administrador")
                {
                    nextAction = PostLoginAction.ShowAdminDashboard;
                }
                else
                {
                    nextAction = PostLoginAction.ShowUserDashboard;
                }

                return AuthenticationResult.Succeeded(userResponse, nextAction);
            }, "authenticating user");
        }

        public Task<AuthenticationResult> Validate2faAsync(string username, string code)
        {
            return ExecuteServiceOperationAsync(() =>
            {
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(code))
                {
                    return Task.FromResult(AuthenticationResult.Failed("El código 2FA es requerido."));
                }

                var usuario = _userRepository.GetUsuarioByNombreUsuario(username);
                if (usuario == null || usuario.Codigo2FA != code || usuario.Codigo2FAExpiracion < DateTime.UtcNow)
                {
                    return Task.FromResult(AuthenticationResult.Failed("El código 2FA es inválido o ha expirado."));
                }

                _userRepository.Set2faCode(username, null, null);

                var userResponse = new UserResponse
                {
                    Username = usuario.UsuarioNombre,
                    Rol = usuario.Rol?.Nombre,
                    CambioContrasenaObligatorio = usuario.CambioContrasenaObligatorio,
                    IdPersona = usuario.IdPersona
                };

                var nextAction = PostLoginAction.None;
                if (usuario.CambioContrasenaObligatorio)
                {
                    nextAction = PostLoginAction.ChangePassword;
                }
                else if (userResponse.Rol == "Administrador")
                {
                    nextAction = PostLoginAction.ShowAdminDashboard;
                }
                else
                {
                    nextAction = PostLoginAction.ShowUserDashboard;
                }

                return Task.FromResult(AuthenticationResult.Succeeded(userResponse, nextAction));
            }, "validating 2FA");
        }

        public async Task RecuperarContrasena(string username, Dictionary<int, string> respuestas) => await ExecuteServiceOperationAsync(async () =>
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ValidationException("Username is required");

            var politica = _userRepository.GetPoliticaSeguridad() ?? new PoliticaSeguridad { CantPreguntas = 3 };
            if (respuestas == null || respuestas.Count != politica.CantPreguntas || respuestas.Any(r => string.IsNullOrWhiteSpace(r.Value)))
                throw new ValidationException($"Se requieren {politica.CantPreguntas} respuestas de seguridad.");

            var usuario = _userRepository.GetUsuarioByNombreUsuario(username)
                ?? throw new ValidationException($"Usuario '{username}' not found");

            var persona = _userRepository.GetPersonaById(usuario.IdPersona)
                ?? throw new ValidationException("Persona no encontrada");

            var respuestasGuardadas = _userRepository.GetRespuestasSeguridadByUsuarioId(usuario.IdUsuario)
                ?? throw new ValidationException("No se han configurado las preguntas de seguridad.");

            if (respuestasGuardadas.Count != politica.CantPreguntas)
                throw new ValidationException("La cantidad de respuestas guardadas no coincide con la política de seguridad.");

            var respuestasGuardadasDict = respuestasGuardadas.ToDictionary(r => r.IdPregunta, r => r.Respuesta);

            foreach (var respuesta in respuestas)
            {
                if (!respuestasGuardadasDict.TryGetValue(respuesta.Key, out var respuestaGuardada) || respuestaGuardada != respuesta.Value)
                {
                    throw new ValidationException("Una o más respuestas de seguridad son incorrectas.");
                }
            }

            var newPassword = GenerateRandomPassword(username, persona);
            usuario.ContrasenaScript = HashUsuarioContrasena(username, newPassword);
            usuario.FechaUltimoCambio = DateTime.Now;
            usuario.CambioContrasenaObligatorio = true;
            _userRepository.UpdateUsuario(usuario);

            if (string.IsNullOrEmpty(persona.Correo))
            {
                throw new ValidationException("El usuario no tiene una dirección de correo electrónico configurada.");
            }

            await _emailService.SendPasswordResetEmailAsync(persona.Correo, newPassword);
        }, "recovering password");

        public void CambiarContrasena(string username, string newPassword, string oldPassword) => ExecuteServiceOperation(() =>
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(oldPassword))
                throw new ValidationException("Todos los campos son requeridos.");

            var usuario = _userRepository.GetUsuarioByNombreUsuario(username)
                ?? throw new ValidationException($"Usuario '{username}' not found");

            var oldPasswordHash = HashUsuarioContrasena(username, oldPassword);
            if (!oldPasswordHash.SequenceEqual(usuario.ContrasenaScript))
            {
                throw new ValidationException("La contraseña actual es incorrecta. Por favor, intente de nuevo.");
            }

            var persona = _userRepository.GetPersonaById(usuario.IdPersona)
                ?? throw new ValidationException("Persona no encontrada");

            ValidatePasswordPolicy(newPassword, username, persona);

            var newPasswordHash = HashUsuarioContrasena(username, newPassword);

            var politica = _userRepository.GetPoliticaSeguridad();
            if (politica?.NoRepetirAnteriores ?? false)
            {
                var historial = _userRepository.GetHistorialContrasenasByUsuarioId(usuario.IdUsuario);
                if (historial.Any(h => h.ContrasenaScript.SequenceEqual(newPasswordHash)))
                {
                    throw new ValidationException("La nueva contraseña no puede ser igual a ninguna de las contraseñas anteriores.");
                }
            }

            var currentPasswordHash = usuario.ContrasenaScript;
            if (currentPasswordHash == null)
            {
                throw new InvalidOperationException("User password hash cannot be null.");
            }
            _userRepository.AddHistorialContrasena(new HistorialContrasena
            {
                IdUsuario = usuario.IdUsuario,
                ContrasenaScript = currentPasswordHash,
                FechaCambio = DateTime.Now
            });

            usuario.ContrasenaScript = newPasswordHash;
            usuario.FechaUltimoCambio = DateTime.Now;
            usuario.CambioContrasenaObligatorio = false;
            _userRepository.UpdateUsuario(usuario);
        }, "changing password");

        public List<TipoDocDto> GetTiposDoc() => ExecuteServiceOperation(() =>
            _userRepository.GetAllTiposDoc().Select(t => new TipoDocDto { IdTipoDoc = t.IdTipoDoc, Nombre = t.Nombre }).ToList(),
            "getting all document types");

        public List<ProvinciaDto> GetProvincias() => ExecuteServiceOperation(() =>
            _userRepository.GetAllProvincias().Select(p => new ProvinciaDto { IdProvincia = p.IdProvincia, Nombre = p.Nombre }).ToList(),
            "getting all provinces");

        public List<PartidoDto> GetPartidosByProvinciaId(int provinciaId) => ExecuteServiceOperation(() =>
            _userRepository.GetPartidosByProvinciaId(provinciaId).Select(p => new PartidoDto { IdPartido = p.IdPartido, Nombre = p.Nombre, IdProvincia = p.IdProvincia }).ToList(),
            "getting partidos by provincia");

        public List<LocalidadDto> GetLocalidadesByPartidoId(int partidoId) => ExecuteServiceOperation(() =>
            _userRepository.GetLocalidadesByPartidoId(partidoId).Select(l => new LocalidadDto { IdLocalidad = l.IdLocalidad, Nombre = l.Nombre, IdPartido = l.IdPartido }).ToList(),
            "getting localidades by partido");

        public List<GeneroDto> GetGeneros() => ExecuteServiceOperation(() =>
            _userRepository.GetAllGeneros().Select(g => new GeneroDto { IdGenero = g.IdGenero, Nombre = g.Nombre }).ToList(),
            "getting all genders");

        public List<PersonaDto> GetPersonas() => ExecuteServiceOperation(() =>
            _userRepository.GetAllPersonas().Select(p => MapToPersonaDto(p)!).ToList(),
            "getting all people");

        public List<RolDto> GetRoles() => ExecuteServiceOperation(() =>
            _userRepository.GetAllRoles().Select(r => new RolDto { IdRol = r.IdRol, Nombre = r.Nombre }).ToList(),
            "getting all roles");

        public PoliticaSeguridadDto? GetPoliticaSeguridad() => ExecuteServiceOperation(() =>
        {
            var politica = _userRepository.GetPoliticaSeguridad();
            return MapToPoliticaSeguridadDto(politica);
        }, "getting security policy");

        public void UpdatePoliticaSeguridad(PoliticaSeguridadDto politicaDto) => ExecuteServiceOperation(() =>
        {
            var politica = _userRepository.GetPoliticaSeguridad() ?? new PoliticaSeguridad { IdPolitica = politicaDto.IdPolitica };
            politica.MayusYMinus = politicaDto.MayusYMinus;
            politica.LetrasYNumeros = politicaDto.LetrasYNumeros;
            politica.CaracterEspecial = politicaDto.CaracterEspecial;
            politica.Autenticacion2FA = politicaDto.Autenticacion2FA;
            politica.NoRepetirAnteriores = politicaDto.NoRepetirAnteriores;
            politica.SinDatosPersonales = politicaDto.SinDatosPersonales;
            politica.MinCaracteres = politicaDto.MinCaracteres;
            politica.CantPreguntas = politicaDto.CantPreguntas;
            _userRepository.UpdatePoliticaSeguridad(politica);
        }, "updating security policy");

        public List<UserDto> GetAllUsers() => ExecuteServiceOperation(() =>
            _userRepository.GetAllUsers().Select(u => MapToUserDto(u)!).ToList(),
            "getting all users");

        public void UpdateUser(UserDto userDto) => ExecuteServiceOperation(() =>
        {
            var usuario = _userRepository.GetUsuarioByNombreUsuario(userDto.Username)
                ?? throw new ValidationException($"Usuario '{userDto.Username}' not found");

            usuario.UsuarioNombre = userDto.Username;
            usuario.IdRol = userDto.IdRol;
            usuario.FechaExpiracion = userDto.FechaExpiracion;
            usuario.CambioContrasenaObligatorio = userDto.CambioContrasenaObligatorio;

            if (userDto.Habilitado)
            {
                if (usuario.FechaBloqueo < DateTime.Now)
                {
                    usuario.FechaBloqueo = new DateTime(9999, 12, 31);
                    usuario.NombreUsuarioBloqueo = null;
                }
            }
            else
            {
                if (usuario.FechaBloqueo > DateTime.Now)
                {
                    usuario.FechaBloqueo = DateTime.Now;
                    usuario.NombreUsuarioBloqueo = "Admin"; // Placeholder
                }
            }

            _userRepository.UpdateUsuario(usuario);
        }, "updating user");

        public void DeleteUser(int userId) => ExecuteServiceOperation(() =>
            _userRepository.DeleteUsuario(userId),
            "deleting user");

        public void UpdatePersona(PersonaDto personaDto) => ExecuteServiceOperation(() =>
        {
            var persona = _userRepository.GetPersonaById(personaDto.IdPersona)
                ?? throw new ValidationException($"Persona with id {personaDto.IdPersona} not found");

            persona.Legajo = personaDto.Legajo;
            persona.Nombre = personaDto.Nombre;
            persona.Apellido = personaDto.Apellido;
            persona.IdTipoDoc = personaDto.IdTipoDoc;
            persona.NumDoc = personaDto.NumDoc;
            persona.FechaNacimiento = personaDto.FechaNacimiento;
            persona.Cuil = personaDto.Cuil;
            persona.Calle = personaDto.Calle;
            persona.Altura = personaDto.Altura;
            persona.IdLocalidad = personaDto.IdLocalidad;
            persona.IdGenero = personaDto.IdGenero;
            persona.Correo = personaDto.Correo;
            persona.Celular = personaDto.Celular;
            persona.FechaIngreso = personaDto.FechaIngreso;

            _userRepository.UpdatePersona(persona);
        }, "updating persona");

        public void DeletePersona(int personaId) => ExecuteServiceOperation(() =>
            _userRepository.DeletePersona(personaId),
            "deleting persona");

        public void GuardarRespuestasSeguridad(string username, Dictionary<int, string> respuestas) => ExecuteServiceOperation(() =>
        {
            var usuario = _userRepository.GetUsuarioByNombreUsuario(username)
                ?? throw new ValidationException($"Usuario '{username}' not found");

            var politica = _userRepository.GetPoliticaSeguridad() ?? new PoliticaSeguridad { CantPreguntas = 3 };
            if (respuestas.Count != politica.CantPreguntas)
                throw new ValidationException($"Se requieren exactamente {politica.CantPreguntas} respuestas de seguridad.");

            _userRepository.DeleteRespuestasSeguridadByUsuarioId(usuario.IdUsuario);

            foreach (var par in respuestas)
            {
                var respuesta = new RespuestaSeguridad
                {
                    IdUsuario = usuario.IdUsuario,
                    IdPregunta = par.Key,
                    Respuesta = par.Value
                };
                _userRepository.AddRespuestaSeguridad(respuesta);
            }
        }, "saving security answers");

        public List<PreguntaSeguridadDto> GetPreguntasSeguridad() => ExecuteServiceOperation(() =>
            _userRepository.GetPreguntasSeguridad().Select(p => new PreguntaSeguridadDto { IdPregunta = p.IdPregunta, Pregunta = p.Pregunta }).ToList(),
            "getting security questions");

        public List<PreguntaSeguridadDto> GetPreguntasDeUsuario(string username) => ExecuteServiceOperation(() =>
        {
            var usuario = _userRepository.GetUsuarioByNombreUsuario(username)
                ?? throw new ValidationException($"Usuario '{username}' not found");

            var respuestas = _userRepository.GetRespuestasSeguridadByUsuarioId(usuario.IdUsuario)
                ?? throw new ValidationException("No se han configurado las preguntas de seguridad.");

            var idPreguntas = respuestas.Select(r => r.IdPregunta).ToList();
            var preguntas = _userRepository.GetPreguntasSeguridadByIds(idPreguntas);

            return preguntas.Select(p => new PreguntaSeguridadDto { IdPregunta = p.IdPregunta, Pregunta = p.Pregunta }).ToList();
        }, "getting user security questions");

        public UserDto? GetUserByUsername(string username) => ExecuteServiceOperation(() =>
        {
            var usuario = _userRepository.GetUsuarioByNombreUsuario(username);
            return MapToUserDto(usuario);
        }, "getting user by username");

        public PersonaDto? GetPersonaById(int personaId) => ExecuteServiceOperation(() =>
        {
            var persona = _userRepository.GetPersonaById(personaId);
            return MapToPersonaDto(persona);
        }, "getting persona by id");

        #region Mappers
        private UserDto? MapToUserDto(Usuario? u)
        {
            if (u == null) return null;
            return new UserDto
            {
                IdUsuario = u.IdUsuario,
                Username = u.UsuarioNombre,
                NombreCompleto = u.Persona != null ? $"{u.Persona.Nombre} {u.Persona.Apellido}" : "N/A",
                Rol = u.Rol?.Nombre,
                IdRol = u.IdRol,
                IdPersona = u.IdPersona,
                CambioContrasenaObligatorio = u.CambioContrasenaObligatorio,
                FechaExpiracion = u.FechaExpiracion,
                Habilitado = u.FechaBloqueo > DateTime.Now
            };
        }

        private PersonaDto? MapToPersonaDto(Persona? p)
        {
            if (p == null) return null;
            return new PersonaDto
            {
                IdPersona = p.IdPersona,
                Legajo = p.Legajo,
                Nombre = p.Nombre,
                Apellido = p.Apellido,
                NombreCompleto = p.NombreCompleto,
                IdTipoDoc = p.IdTipoDoc,
                TipoDocNombre = p.TipoDoc?.Nombre,
                NumDoc = p.NumDoc,
                FechaNacimiento = p.FechaNacimiento,
                Cuil = p.Cuil,
                Calle = p.Calle,
                Altura = p.Altura,
                IdLocalidad = p.IdLocalidad,
                LocalidadNombre = p.Localidad?.Nombre,
                IdPartido = p.Localidad?.IdPartido ?? 0,
                PartidoNombre = p.Localidad?.Partido?.Nombre,
                IdProvincia = p.Localidad?.Partido?.IdProvincia ?? 0,
                ProvinciaNombre = p.Localidad?.Partido?.Provincia?.Nombre,
                IdGenero = p.IdGenero,
                GeneroNombre = p.Genero?.Nombre,
                Correo = p.Correo,
                Celular = p.Celular,
                FechaIngreso = p.FechaIngreso
            };
        }

        private PoliticaSeguridadDto? MapToPoliticaSeguridadDto(PoliticaSeguridad? politica)
        {
            if (politica == null) return null;
            return new PoliticaSeguridadDto
            {
                IdPolitica = politica.IdPolitica,
                MayusYMinus = politica.MayusYMinus,
                LetrasYNumeros = politica.LetrasYNumeros,
                CaracterEspecial = politica.CaracterEspecial,
                Autenticacion2FA = politica.Autenticacion2FA,
                NoRepetirAnteriores = politica.NoRepetirAnteriores,
                SinDatosPersonales = politica.SinDatosPersonales,
                MinCaracteres = politica.MinCaracteres,
                CantPreguntas = politica.CantPreguntas
            };
        }
        #endregion

        #region Private Helpers
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            try
            {
                return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private byte[] HashUsuarioContrasena(string username, string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = password + username;
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
            }
        }

        private string GenerateRandomPassword(string? username = null, Persona? persona = null)
        {
            var politica = _userRepository.GetPoliticaSeguridad() ?? new PoliticaSeguridad();
            var random = new Random();

            while (true)
            {
                var minLength = politica.MinCaracteres > 0 ? politica.MinCaracteres : 12;
                var passwordChars = new List<char>();
                const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                const string lower = "abcdefghijklmnopqrstuvwxyz";
                const string digits = "0123456789";
                const string specials = "!@#$%^&*()";
                var allChars = new StringBuilder(upper).Append(lower).Append(digits).Append(specials).ToString();

                if (politica.MayusYMinus)
                {
                    passwordChars.Add(upper[random.Next(upper.Length)]);
                    passwordChars.Add(lower[random.Next(lower.Length)]);
                }
                if (politica.LetrasYNumeros)
                {
                    passwordChars.Add(digits[random.Next(digits.Length)]);
                }
                if (politica.CaracterEspecial)
                {
                    passwordChars.Add(specials[random.Next(specials.Length)]);
                }

                while (passwordChars.Count < minLength)
                {
                    passwordChars.Add(allChars[random.Next(allChars.Length)]);
                }

                var password = new string(passwordChars.OrderBy(c => random.Next()).ToArray());

                if (politica.SinDatosPersonales && username != null && persona != null)
                {
                    try
                    {
                        ValidatePasswordPolicy(password, username, persona);
                    }
                    catch (ValidationException)
                    {
                        continue;
                    }
                }
                return password;
            }
        }

        private void ValidatePasswordPolicy(string password, string username, Persona persona)
        {
            var politica = _userRepository.GetPoliticaSeguridad();
            if (politica == null) return;

            if (password.Length < politica.MinCaracteres)
                throw new ValidationException($"La contraseña debe tener al menos {politica.MinCaracteres} caracteres.");

            if (politica.MayusYMinus && (!password.Any(char.IsUpper) || !password.Any(char.IsLower)))
                throw new ValidationException("La contraseña debe contener mayúsculas y minúsculas.");

            if (politica.LetrasYNumeros && (!password.Any(char.IsLetter) || !password.Any(char.IsDigit)))
                throw new ValidationException("La contraseña debe contener letras y números.");

            if (politica.CaracterEspecial && !password.Any(c => !char.IsLetterOrDigit(c)))
                throw new ValidationException("La contraseña debe contener caracteres especiales.");

            if (politica.SinDatosPersonales)
            {
                if (password.Contains(username, StringComparison.OrdinalIgnoreCase) ||
                    password.Contains(persona.Nombre, StringComparison.OrdinalIgnoreCase) ||
                    password.Contains(persona.Apellido, StringComparison.OrdinalIgnoreCase))
                {
                    throw new ValidationException("La contraseña no debe contener datos personales (nombre de usuario, nombre o apellido).");
                }

                if (persona.FechaNacimiento.HasValue)
                {
                    string[] dateFormats = { "ddMMyyyy", "yyyyMMdd", "ddMM", "MMdd" };
                    foreach (var format in dateFormats)
                    {
                        if (password.Contains(persona.FechaNacimiento.Value.ToString(format)))
                        {
                            throw new ValidationException("La contraseña no debe contener su fecha de nacimiento.");
                        }
                    }
                }
            }
        }
        #endregion
    }
}