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
using BusinessLogic.Exceptions;
using BusinessLogic.Security;
using BusinessLogic.Factories;
using BusinessLogic.Mappers;

namespace BusinessLogic.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPersonaRepository _personaRepository;
        private readonly ISecurityRepository _securityRepository;
        private readonly IEmailService _emailService;
        private readonly ILogger<UserManagementService> _logger;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IPersonaFactory _personaFactory;
        private readonly IUsuarioFactory _usuarioFactory;

        public UserManagementService(
            IUserRepository userRepository,
            IPersonaRepository personaRepository,
            ISecurityRepository securityRepository,
            IEmailService emailService,
            ILogger<UserManagementService> logger,
            IPasswordHasher passwordHasher,
            IPersonaFactory personaFactory,
            IUsuarioFactory usuarioFactory)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _personaRepository = personaRepository ?? throw new ArgumentNullException(nameof(personaRepository));
            _securityRepository = securityRepository ?? throw new ArgumentNullException(nameof(securityRepository));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _personaFactory = personaFactory ?? throw new ArgumentNullException(nameof(personaFactory));
            _usuarioFactory = usuarioFactory ?? throw new ArgumentNullException(nameof(usuarioFactory));
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

        public Task CrearPersonaAsync(PersonaRequest request)
        {
            ExecuteServiceOperation(() =>
            {
                _logger.LogInformation("Iniciando la creación de la persona con legajo: {Legajo}", request.Legajo);
                var persona = _personaFactory.Create(request);
                _logger.LogInformation("Llamando a AddPersona en el repositorio.");
                _personaRepository.AddPersona(persona);
                _logger.LogInformation("Persona creada con éxito en el repositorio.");
            }, "creating a person");
            return Task.CompletedTask;
        }

        public async Task CrearUsuarioAsync(UserRequest request) => await ExecuteServiceOperationAsync(async () =>
        {
            var (usuario, plainPassword) = _usuarioFactory.Create(request);

            await _userRepository.AddUsuarioAsync(usuario);

            var persona = _personaRepository.GetPersonaById(usuario.IdPersona)!; // We know the persona exists from the factory

            await _emailService.SendPasswordResetEmailAsync(persona.Correo!, plainPassword);
        }, "creating a user");

        public PoliticaSeguridadDto? GetPoliticaSeguridad() => ExecuteServiceOperation(() =>
        {
            var politica = _securityRepository.GetPoliticaSeguridad();
            return PoliticaSeguridadMapper.MapToPoliticaSeguridadDto(politica);
        }, "getting security policy");

        public void UpdatePoliticaSeguridad(PoliticaSeguridadDto politicaDto) => ExecuteServiceOperation(() =>
        {
            var politica = _securityRepository.GetPoliticaSeguridad()
                ?? throw new ValidationException("No se encontró la política de seguridad para actualizar.");
            politica.Update(politicaDto.MayusYMinus, politicaDto.LetrasYNumeros, politicaDto.CaracterEspecial, politicaDto.Autenticacion2FA, politicaDto.NoRepetirAnteriores, politicaDto.SinDatosPersonales, politicaDto.MinCaracteres, politicaDto.CantPreguntas);
            _securityRepository.UpdatePoliticaSeguridad(politica);
        }, "updating security policy");

        public async Task<List<UserDto>> GetAllUsersAsync() => await ExecuteServiceOperationAsync(async () =>
        {
            var usuarios = await _userRepository.GetAllUsersAsync();
            var personas = _personaRepository.GetAllPersonas().ToDictionary(p => p.IdPersona);

            return usuarios.Select(u =>
            {
                var userDto = UserMapper.MapToUserDto(u)!;
                if (personas.TryGetValue(u.IdPersona, out var persona))
                {
                    userDto.Nombre = persona.Nombre;
                    userDto.Apellido = persona.Apellido;
                    userDto.Correo = persona.Correo;
                }
                return userDto;
            }).ToList();
        }, "getting all users");

        public async Task UpdateUserAsync(UserDto userDto) => await ExecuteServiceOperationAsync(async () =>
        {
            var usuario = await _userRepository.GetUsuarioByNombreUsuarioAsync(userDto.Username)
                ?? throw new ValidationException($"Usuario '{userDto.Username}' not found");

            // The admin username should come from the current session context in a real app
            const string adminUsername = "Admin";

            usuario.ChangeRole(userDto.IdRol);
            usuario.SetExpiration(userDto.FechaExpiracion);
            usuario.ForcePasswordChange(userDto.CambioContrasenaObligatorio);

            if (userDto.Habilitado)
            {
                usuario.Habilitar();
            }
            else
            {
                usuario.Deshabilitar(adminUsername);
            }

            await _userRepository.UpdateUsuarioAsync(usuario);
        }, "updating user");

        public async Task DeleteUserAsync(int userId) => await ExecuteServiceOperationAsync(async () =>
            await _userRepository.DeleteUsuarioAsync(userId),
            "deleting user");

        public Task UpdatePersonaAsync(PersonaDto personaDto)
        {
            ExecuteServiceOperation(() =>
            {
                var persona = _personaRepository.GetPersonaById(personaDto.IdPersona)
                    ?? throw new ValidationException($"Persona with id {personaDto.IdPersona} not found");

                persona.Update(personaDto.Legajo, personaDto.Nombre, personaDto.Apellido, personaDto.IdTipoDoc, personaDto.NumDoc, personaDto.FechaNacimiento, personaDto.Cuil, personaDto.Calle, personaDto.Altura, personaDto.IdLocalidad, personaDto.IdGenero, personaDto.Correo, personaDto.Celular, personaDto.FechaIngreso);

                _personaRepository.UpdatePersona(persona);
            }, "updating persona");
            return Task.CompletedTask;
        }

        public Task DeletePersonaAsync(int personaId)
        {
            ExecuteServiceOperation(() => _personaRepository.DeletePersona(personaId), "deleting persona");
            return Task.CompletedTask;
        }

        public async Task<UserDto?> GetUserByUsernameAsync(string username) => await ExecuteServiceOperationAsync(async () =>
        {
            var usuario = await _userRepository.GetUsuarioByNombreUsuarioAsync(username);
            return UserMapper.MapToUserDto(usuario);
        }, "getting user by username");

        public Task<PersonaDto?> GetPersonaByIdAsync(int personaId)
        {
            return Task.FromResult(ExecuteServiceOperation(() =>
            {
                var persona = _personaRepository.GetPersonaById(personaId);
                return PersonaMapper.MapToPersonaDto(persona);
            }, "getting persona by id"));
        }

        public Task<List<PersonaDto>> GetPersonasAsync()
        {
            return Task.FromResult(ExecuteServiceOperation(() =>
                _personaRepository.GetAllPersonas().Select(p => PersonaMapper.MapToPersonaDto(p)!).ToList(),
                "getting all people"));
        }
    }
}
