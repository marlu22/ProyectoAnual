using System;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Exceptions;
using BusinessLogic.Models;
using BusinessLogic.Security;
using DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPersonaRepository _personaRepository;
        private readonly IEmailService _emailService;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ISecurityPolicyService _securityPolicyService;

        private const string AdminRole = "Administrador";

        public AuthenticationService(
            IUserRepository userRepository,
            IPersonaRepository personaRepository,
            IEmailService emailService,
            ILogger<AuthenticationService> logger,
            IPasswordHasher passwordHasher,
            ISecurityPolicyService securityPolicyService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _personaRepository = personaRepository ?? throw new ArgumentNullException(nameof(personaRepository));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _securityPolicyService = securityPolicyService ?? throw new ArgumentNullException(nameof(securityPolicyService));
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

                var politica = _securityPolicyService.GetPoliticaSeguridad();
                if (politica?.Autenticacion2FA ?? false)
                {
                    var persona = _personaRepository.GetPersonaById(usuario.IdPersona);
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

                var nextAction = DetermineNextAction(userResponse);
                return AuthenticationResult.Succeeded(userResponse, nextAction);
            }, "authenticating user");
        }

        public async Task<AuthenticationResult> Validate2faAsync(string username, string code)
        {
            return await ExecuteServiceOperationAsync(async () =>
            {
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(code))
                {
                    return AuthenticationResult.Failed("El código 2FA es requerido.");
                }

                var usuario = _userRepository.GetUsuarioByNombreUsuario(username);
                if (usuario == null || usuario.Codigo2FA != code || usuario.Codigo2FAExpiracion < DateTime.UtcNow)
                {
                    return AuthenticationResult.Failed("El código 2FA es inválido o ha expirado.");
                }

                usuario.SetTwoFactorCode(null, null);
                _userRepository.UpdateUsuario(usuario);

                var userResponse = new UserResponse
                {
                    Username = usuario.UsuarioNombre,
                    Rol = usuario.Rol?.Nombre,
                    CambioContrasenaObligatorio = usuario.CambioContrasenaObligatorio,
                    IdPersona = usuario.IdPersona
                };

                var nextAction = DetermineNextAction(userResponse);
                return AuthenticationResult.Succeeded(userResponse, nextAction);
            }, "validating 2FA");
        }

        private PostLoginAction DetermineNextAction(UserResponse user)
        {
            if (user.CambioContrasenaObligatorio)
            {
                return PostLoginAction.ChangePassword;
            }
            if (user.Rol == AdminRole)
            {
                return PostLoginAction.ShowAdminDashboard;
            }
            return PostLoginAction.ShowUserDashboard;
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
    }
}
