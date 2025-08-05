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
using UserManagementSystem.DataAccess.Exceptions;
using UserManagementSystem.BusinessLogic.Exceptions;

namespace BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public UserService(IUserRepository userRepository, IEmailService emailService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        private T ExecuteServiceOperation<T>(Func<T> operation, string operationName)
        {
            try
            {
                return operation();
            }
            catch (Exception ex)
            {
                // Log the exception ex here
                throw new BusinessLogicException($"An unexpected error occurred during {operationName}.", ex);
            }
        }

        private async Task ExecuteServiceOperationAsync(Func<Task> operation, string operationName)
        {
            try
            {
                await operation();
            }
            catch (Exception ex)
            {
                // Log the exception ex here
                throw new BusinessLogicException($"An unexpected error occurred during {operationName}.", ex);
            }
        }

        private async Task<T> ExecuteServiceOperationAsync<T>(Func<Task<T>> operation, string operationName)
        {
            try
            {
                return await operation();
            }
            catch (Exception ex)
            {
                // Log the exception ex here
                throw new BusinessLogicException($"An unexpected error occurred during {operationName}.", ex);
            }
        }

        private void ExecuteServiceOperation(Action operation, string operationName)
        {
            try
            {
                operation();
            }
            catch (Exception ex)
            {
                // Log the exception ex here
                throw new BusinessLogicException($"An unexpected error occurred during {operationName}.", ex);
            }
        }

        public void CrearPersona(PersonaRequest request) => ExecuteServiceOperation(() =>
        {
            if (!int.TryParse(request.Legajo, out int legajo))
                throw new ValidationException("El legajo debe ser un número válido.");

            if (string.IsNullOrWhiteSpace(request.Nombre))
                throw new ValidationException("El nombre no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(request.Apellido))
                throw new ValidationException("El apellido no puede estar vacío.");

            if (!long.TryParse(request.NumDoc, out _))
                throw new ValidationException("El número de documento debe ser numérico.");

            if (!long.TryParse(request.Cuil, out _))
                throw new ValidationException("El CUIL debe ser numérico.");

            if (string.IsNullOrWhiteSpace(request.Calle))
                throw new ValidationException("La calle no puede estar vacía.");

            if (!int.TryParse(request.Altura, out _))
                throw new ValidationException("La altura de la dirección debe ser un número.");

            if (string.IsNullOrWhiteSpace(request.Correo) || !IsValidEmail(request.Correo))
                throw new ValidationException("El formato del correo electrónico no es válido.");

            if (!int.TryParse(request.Localidad, out int localidadId))
                throw new ValidationException("El ID de localidad no es válido.");

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
            _userRepository.AddPersona(persona);
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

            // Enviar la contraseña generada por correo
            var task = _emailService.SendPasswordResetEmailAsync(persona.Correo, passwordToUse);
            task.ContinueWith(t => {
                // Log exception if email sending fails
                if (t.IsFaulted)
                {
                    // Log t.Exception
                }
            });
        }, "creating a user");

        public async Task<AuthenticationResult> AuthenticateAsync(string username, string password)
        {
            return await ExecuteServiceOperationAsync<AuthenticationResult>(async () =>
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
                    CambioContrasenaObligatorio = usuario.CambioContrasenaObligatorio
                };
                return AuthenticationResult.Succeeded(userResponse);
            }, "authenticating user");
        }

        public Task<UserResponse?> Validate2faAsync(string username, string code)
        {
            return ExecuteServiceOperationAsync<UserResponse?>(() =>
            {
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(code))
                {
                    return Task.FromResult<UserResponse?>(null);
                }

                var usuario = _userRepository.GetUsuarioByNombreUsuario(username);
                if (usuario == null || usuario.Codigo2FA != code || usuario.Codigo2FAExpiracion < DateTime.UtcNow)
                {
                    return Task.FromResult<UserResponse?>(null);
                }

                _userRepository.Set2faCode(username, null, null);

                var userResponse = new UserResponse
                {
                    Username = usuario.UsuarioNombre,
                    Rol = usuario.Rol?.Nombre,
                    CambioContrasenaObligatorio = usuario.CambioContrasenaObligatorio
                };
                return Task.FromResult<UserResponse?>(userResponse);
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

            // Convertir a diccionario para búsqueda fácil
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

            // Enviar correo con la nueva contraseña usando el servicio de email
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
                // This case should ideally not happen if data is consistent
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

        public List<TipoDoc> GetTiposDoc() => ExecuteServiceOperation(() => _userRepository.GetAllTiposDoc(), "getting all document types");

        public List<Provincia> GetProvincias() => ExecuteServiceOperation(() => _userRepository.GetAllProvincias(), "getting all provinces");

        public List<Partido> GetPartidosByProvinciaId(int provinciaId) => ExecuteServiceOperation(() => _userRepository.GetPartidosByProvinciaId(provinciaId), "getting partidos by provincia");

        public List<Localidad> GetLocalidadesByPartidoId(int partidoId) => ExecuteServiceOperation(() => _userRepository.GetLocalidadesByPartidoId(partidoId), "getting localidades by partido");

        public List<Genero> GetGeneros() => ExecuteServiceOperation(() => _userRepository.GetAllGeneros(), "getting all genders");

        public List<Persona> GetPersonas() => ExecuteServiceOperation(() => _userRepository.GetAllPersonas(), "getting all people");

        public List<Rol> GetRoles() => ExecuteServiceOperation(() => _userRepository.GetAllRoles(), "getting all roles");

        public PoliticaSeguridad? GetPoliticaSeguridad() => ExecuteServiceOperation(() => _userRepository.GetPoliticaSeguridad(), "getting security policy");

        public void UpdatePoliticaSeguridad(PoliticaSeguridad politica) => ExecuteServiceOperation(() => _userRepository.UpdatePoliticaSeguridad(politica), "updating security policy");

        public List<Usuario> GetAllUsers() => ExecuteServiceOperation(() => _userRepository.GetAllUsers(), "getting all users");

        public void UpdateUser(UserDto userDto) => ExecuteServiceOperation(() =>
        {
            var usuario = _userRepository.GetUsuarioByNombreUsuario(userDto.Username)
                ?? throw new ValidationException($"Usuario '{userDto.Username}' not found");

            usuario.UsuarioNombre = userDto.Username;
            usuario.IdRol = userDto.IdRol;
            usuario.FechaExpiracion = userDto.FechaExpiracion;

            if (userDto.Habilitado)
            {
                usuario.FechaBloqueo = new DateTime(9999, 12, 31);
                usuario.NombreUsuarioBloqueo = null;
            }
            else
            {
                usuario.FechaBloqueo = DateTime.Now;
                usuario.NombreUsuarioBloqueo = "Admin"; // Placeholder for current admin user
            }

            _userRepository.UpdateUsuario(usuario);
        }, "updating user");

        public void DeleteUser(int userId) => ExecuteServiceOperation(() =>
        {
            _userRepository.DeleteUsuario(userId);
        }, "deleting user");

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Use Regex for a more robust validation
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
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
                // Concatenate password and username (as salt) as per security requirements
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
                        // Use the existing validation logic to check the generated password
                        ValidatePasswordPolicy(password, username, persona);
                    }
                    catch (ValidationException)
                    {
                        continue; // Regenerate password if it fails validation
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

        public void GuardarRespuestasSeguridad(string username, Dictionary<int, string> respuestas) => ExecuteServiceOperation(() =>
        {
            var usuario = _userRepository.GetUsuarioByNombreUsuario(username)
                ?? throw new ValidationException($"Usuario '{username}' not found");

            var politica = _userRepository.GetPoliticaSeguridad() ?? new PoliticaSeguridad { CantPreguntas = 3 };
            if (respuestas.Count != politica.CantPreguntas)
                throw new ValidationException($"Se requieren exactamente {politica.CantPreguntas} respuestas de seguridad.");

            // Borrar respuestas de seguridad anteriores para evitar duplicados
            _userRepository.DeleteRespuestasSeguridadByUsuarioId(usuario.IdUsuario);

            foreach (var par in respuestas)
            {
                var respuesta = new RespuestaSeguridad
                {
                    IdUsuario = usuario.IdUsuario,
                    IdPregunta = par.Key,
                    Respuesta = par.Value // Idealmente, las respuestas también deberían ser hasheadas
                };
                _userRepository.AddRespuestaSeguridad(respuesta);
            }
        }, "saving security answers");

        public List<PreguntaSeguridad> GetPreguntasSeguridad() => ExecuteServiceOperation(
            () => _userRepository.GetPreguntasSeguridad(), "getting security questions");

        public List<PreguntaSeguridad> GetPreguntasDeUsuario(string username) => ExecuteServiceOperation(() =>
        {
            var usuario = _userRepository.GetUsuarioByNombreUsuario(username)
                ?? throw new ValidationException($"Usuario '{username}' not found");

            var respuestas = _userRepository.GetRespuestasSeguridadByUsuarioId(usuario.IdUsuario)
                ?? throw new ValidationException("No se han configurado las preguntas de seguridad.");

            var idPreguntas = respuestas.Select(r => r.IdPregunta).ToList();

            var preguntas = _userRepository.GetPreguntasSeguridad();

            return preguntas.Where(p => idPreguntas.Contains(p.IdPregunta)).ToList();

        }, "getting user security questions");
    }
}