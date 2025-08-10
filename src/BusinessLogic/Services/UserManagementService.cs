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
        private readonly IEmailService _emailService;
        private readonly ILogger<UserManagementService> _logger;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IPersonaFactory _personaFactory;
        private readonly IUsuarioFactory _usuarioFactory;

        public UserManagementService(IUserRepository userRepository, IEmailService emailService, ILogger<UserManagementService> logger, IPasswordHasher passwordHasher, IPersonaFactory personaFactory, IUsuarioFactory usuarioFactory)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
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

        public void CrearPersona(PersonaRequest request) => ExecuteServiceOperation(() =>
        {
            _logger.LogInformation("Iniciando la creación de la persona con legajo: {Legajo}", request.Legajo);
            var persona = _personaFactory.Create(request);
            _logger.LogInformation("Llamando a AddPersona en el repositorio.");
            _userRepository.AddPersona(persona);
            _logger.LogInformation("Persona creada con éxito en el repositorio.");
        }, "creating a person");

        public void CrearUsuario(UserRequest request) => ExecuteServiceOperation(() =>
        {
            var (usuario, plainPassword) = _usuarioFactory.Create(request);

            _userRepository.AddUsuario(usuario);

            var persona = _userRepository.GetPersonaById(usuario.IdPersona)!; // We know the persona exists from the factory

            var task = _emailService.SendPasswordResetEmailAsync(persona.Correo!, plainPassword);
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
            return PoliticaSeguridadMapper.MapToPoliticaSeguridadDto(politica);
        }, "getting security policy");

        public void UpdatePoliticaSeguridad(PoliticaSeguridadDto politicaDto) => ExecuteServiceOperation(() =>
        {
            var politica = _userRepository.GetPoliticaSeguridad()
                ?? throw new ValidationException("No se encontró la política de seguridad para actualizar.");
            politica.Update(politicaDto.MayusYMinus, politicaDto.LetrasYNumeros, politicaDto.CaracterEspecial, politicaDto.Autenticacion2FA, politicaDto.NoRepetirAnteriores, politicaDto.SinDatosPersonales, politicaDto.MinCaracteres, politicaDto.CantPreguntas);
            _userRepository.UpdatePoliticaSeguridad(politica);
        }, "updating security policy");

        public List<UserDto> GetAllUsers() => ExecuteServiceOperation(() =>
            _userRepository.GetAllUsers().Select(u => UserMapper.MapToUserDto(u)!).ToList(),
            "getting all users");

        public void UpdateUser(UserDto userDto) => ExecuteServiceOperation(() =>
        {
            var usuario = _userRepository.GetUsuarioByNombreUsuario(userDto.Username)
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

            _userRepository.UpdateUsuario(usuario);
        }, "updating user");

        public void DeleteUser(int userId) => ExecuteServiceOperation(() =>
            _userRepository.DeleteUsuario(userId),
            "deleting user");

        public void UpdatePersona(PersonaDto personaDto) => ExecuteServiceOperation(() =>
        {
            var persona = _userRepository.GetPersonaById(personaDto.IdPersona)
                ?? throw new ValidationException($"Persona with id {personaDto.IdPersona} not found");

            persona.Update(personaDto.Legajo, personaDto.Nombre, personaDto.Apellido, personaDto.IdTipoDoc, personaDto.NumDoc, personaDto.FechaNacimiento, personaDto.Cuil, personaDto.Calle, personaDto.Altura, personaDto.IdLocalidad, personaDto.IdGenero, personaDto.Correo, personaDto.Celular, personaDto.FechaIngreso);

            _userRepository.UpdatePersona(persona);
        }, "updating persona");

        public void DeletePersona(int personaId) => ExecuteServiceOperation(() =>
            _userRepository.DeletePersona(personaId),
            "deleting persona");

        public UserDto? GetUserByUsername(string username) => ExecuteServiceOperation(() =>
        {
            var usuario = _userRepository.GetUsuarioByNombreUsuario(username);
            return UserMapper.MapToUserDto(usuario);
        }, "getting user by username");

        public PersonaDto? GetPersonaById(int personaId) => ExecuteServiceOperation(() =>
        {
            var persona = _userRepository.GetPersonaById(personaId);
            return PersonaMapper.MapToPersonaDto(persona);
        }, "getting persona by id");

        public List<PersonaDto> GetPersonas() => ExecuteServiceOperation(() =>
            _userRepository.GetAllPersonas().Select(p => PersonaMapper.MapToPersonaDto(p)!).ToList(),
            "getting all people");
    }
}
