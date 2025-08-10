using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.Extensions.Logging;
using UserManagementSystem.BusinessLogic.Exceptions;
using BusinessLogic.Security;

namespace BusinessLogic.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly ILogger<UserAuthenticationService> _logger;
        private readonly IPasswordHasher _passwordHasher;

        public UserAuthenticationService(IUserRepository userRepository, IEmailService emailService, ILogger<UserAuthenticationService> logger, IPasswordHasher passwordHasher)
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

        public async Task<AuthenticationResult> AuthenticateAsync(string username, string password)
        {
            return await ExecuteServiceOperationAsync(async () =>
            {
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                    return AuthenticationResult.Failed("Usuario o contraseña no pueden estar vacíos.");

                var usuario = _userRepository.GetUsuarioByNombreUsuario(username);
                if (usuario == null)
                    return AuthenticationResult.Failed("Usuario o contraseña incorrectos.");

                var hash = _passwordHasher.Hash(username, password);
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
            // This is not ideal, we are modifying the entity directly.
            // A future refactoring could be to create a method on Usuario like `ResetPassword`.
            usuario.ContrasenaScript = _passwordHasher.Hash(username, newPassword);
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

            var oldPasswordHash = _passwordHasher.Hash(username, oldPassword);
            if (!oldPasswordHash.SequenceEqual(usuario.ContrasenaScript))
            {
                throw new ValidationException("La contraseña actual es incorrecta. Por favor, intente de nuevo.");
            }

            var persona = _userRepository.GetPersonaById(usuario.IdPersona)
                ?? throw new ValidationException("Persona no encontrada");

            ValidatePasswordPolicy(newPassword, username, persona);

            var newPasswordHash = _passwordHasher.Hash(username, newPassword);

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

        public PoliticaSeguridadDto? GetPoliticaSeguridad() => ExecuteServiceOperation(() =>
        {
            var politica = _userRepository.GetPoliticaSeguridad();
            return MapToPoliticaSeguridadDto(politica);
        }, "getting security policy");

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
    }
}
