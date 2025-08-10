using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using UserManagementSystem.BusinessLogic.Exceptions;
using BusinessLogic.Security;

namespace BusinessLogic.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly ILogger<UserManagementService> _logger;
        private readonly IPasswordHasher _passwordHasher;

        public UserManagementService(IUserRepository userRepository, IEmailService emailService, ILogger<UserManagementService> logger, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
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
                throw new ValidationException("El número de documento debe ser numérico.");
            if (!string.IsNullOrWhiteSpace(request.Cuil) && !long.TryParse(request.Cuil, out _))
                throw new ValidationException("El CUIL debe ser numérico.");
            if (string.IsNullOrWhiteSpace(request.Calle))
                throw new ValidationException("La calle no puede estar vacía.");
            if (!int.TryParse(request.Altura, out _))
                throw new ValidationException("La altura de la dirección debe ser un número.");
            if (string.IsNullOrWhiteSpace(request.Correo) || !IsValidEmail(request.Correo))
                throw new ValidationException("El formato del correo electrónico no es válido.");
            if (!int.TryParse(request.Localidad, out int localidadId))
            {
                _logger.LogWarning("El ID de localidad '{Localidad}' no es válido.", request.Localidad);
                throw new ValidationException("El ID de localidad no es válido.");
            }

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

            // The constructor for Usuario should be enriched in a future refactoring
            var usuario = new Usuario
            {
                IdPersona = int.Parse(request.PersonaId),
                UsuarioNombre = request.Username,
                ContrasenaScript = _passwordHasher.Hash(request.Username, passwordToUse),
                IdRol = _userRepository.GetRolByNombre(request.Rol)?.IdRol ?? throw new ValidationException("Rol no encontrado"),
                FechaUltimoCambio = DateTime.Now,
                // We should use the Habilitar() method here, but the entity is not instantiated yet.
                // This highlights a need for a Factory for Usuario as well.
                FechaBloqueo = new DateTime(9999, 12, 31),
                CambioContrasenaObligatorio = true
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

        public PoliticaSeguridadDto? GetPoliticaSeguridad() => ExecuteServiceOperation(() =>
        {
            var politica = _userRepository.GetPoliticaSeguridad();
            return MapToPoliticaSeguridadDto(politica);
        }, "getting security policy");

        public void UpdatePoliticaSeguridad(PoliticaSeguridadDto politicaDto) => ExecuteServiceOperation(() =>
        {
            // This logic could also be moved to the PoliticaSeguridad entity.
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

            // This logic should be moved to the entity.
            usuario.UsuarioNombre = userDto.Username;
            usuario.IdRol = userDto.IdRol;
            usuario.FechaExpiracion = userDto.FechaExpiracion;
            usuario.CambioContrasenaObligatorio = userDto.CambioContrasenaObligatorio;

            if (userDto.Habilitado)
            {
                usuario.Habilitar();
            }
            else
            {
                usuario.Deshabilitar("Admin"); // Placeholder for admin user
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

            // This logic should be moved to the entity.
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

        public List<PersonaDto> GetPersonas() => ExecuteServiceOperation(() =>
            _userRepository.GetAllPersonas().Select(p => MapToPersonaDto(p)!).ToList(),
            "getting all people");

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
                        new PasswordPolicyValidator().Validate(password, username, persona, politica);
                    }
                    catch (ValidationException)
                    {
                        continue;
                    }
                }
                return password;
            }
        }
        #endregion
    }
}
