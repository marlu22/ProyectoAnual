using BusinessLogic.Services;
using BusinessLogic.Security;
using DataAccess.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using BusinessLogic.Models;
using DataAccess.Entities;
using System.Threading.Tasks;
using BusinessLogic.Factories;
using BusinessLogic.Exceptions;

namespace BusinessLogic.Tests
{
    public class UserManagementServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IPersonaRepository> _personaRepositoryMock;
        private readonly Mock<ISecurityRepository> _securityRepositoryMock;
        private readonly Mock<IEmailService> _emailServiceMock;
        private readonly Mock<ILogger<UserManagementService>> _loggerMock;
        private readonly Mock<IPasswordHasher> _passwordHasherMock;
        private readonly Mock<IUsuarioFactory> _usuarioFactoryMock;
        private readonly Mock<IPersonaFactory> _personaFactoryMock;
        private readonly UserManagementService _sut; // System Under Test

        public UserManagementServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _personaRepositoryMock = new Mock<IPersonaRepository>();
            _securityRepositoryMock = new Mock<ISecurityRepository>();
            _emailServiceMock = new Mock<IEmailService>();
            _loggerMock = new Mock<ILogger<UserManagementService>>();
            _passwordHasherMock = new Mock<IPasswordHasher>();
            _usuarioFactoryMock = new Mock<IUsuarioFactory>();
            _personaFactoryMock = new Mock<IPersonaFactory>();

            _sut = new UserManagementService(
                _userRepositoryMock.Object,
                _personaRepositoryMock.Object,
                _securityRepositoryMock.Object,
                _emailServiceMock.Object,
                _loggerMock.Object,
                _passwordHasherMock.Object,
                _personaFactoryMock.Object,
                _usuarioFactoryMock.Object
            );
        }

        [Fact]
        public async Task CrearUsuarioAsync_WithValidData_ShouldCallAddUsuarioAndSendEmail()
        {
            // Arrange
            var userRequest = new UserRequest { PersonaId = "1", Username = "testuser", Rol = "Usuario" };
            var persona = new Persona(1, "Test", "User", 1, "12345678", System.DateTime.Now, "20123456789", "Test Street", "123", 1, 1, "test@example.com", "1234567890", System.DateTime.Now);
            var usuario = new Usuario("testuser", new byte[0], 1, 1, 1);
            var plainPassword = "plainPassword123";

            _usuarioFactoryMock.Setup(f => f.Create(userRequest)).Returns((usuario, plainPassword));
            _personaRepositoryMock.Setup(r => r.GetPersonaById(1)).Returns(persona);
            _userRepositoryMock.Setup(r => r.AddUsuarioAsync(usuario)).Returns(Task.CompletedTask);
            _emailServiceMock.Setup(s => s.SendPasswordResetEmailAsync(persona.Correo!, plainPassword)).Returns(Task.CompletedTask);

            // Act
            await _sut.CrearUsuarioAsync(userRequest);

            // Assert
            _userRepositoryMock.Verify(r => r.AddUsuarioAsync(usuario), Times.Once);
            _emailServiceMock.Verify(s => s.SendPasswordResetEmailAsync(persona.Correo!, plainPassword), Times.Once);
        }

        [Fact]
        public async Task UpdateUserAsync_WhenUserExists_ShouldUpdateAndCallRepository()
        {
            // Arrange
            var userDto = new UserDto { Username = "testuser", IdRol = 1, FechaExpiracion = null, Habilitado = true };
            var usuario = new Usuario("testuser", new byte[0], 1, 1, 1);
            _userRepositoryMock.Setup(r => r.GetUsuarioByNombreUsuarioAsync("testuser")).ReturnsAsync(usuario);
            _userRepositoryMock.Setup(r => r.UpdateUsuarioAsync(It.IsAny<Usuario>())).Returns(Task.CompletedTask);

            // Act
            await _sut.UpdateUserAsync(userDto);

            // Assert
            _userRepositoryMock.Verify(r => r.UpdateUsuarioAsync(It.Is<Usuario>(u => u.UsuarioNombre == "testuser")), Times.Once);
            Assert.True(usuario.FechaBloqueo > DateTime.Now); // Habilitado
        }

        [Fact]
        public async Task UpdateUserAsync_WhenUserDoesNotExist_ShouldThrowValidationException()
        {
            // Arrange
            var userDto = new UserDto { Username = "nonexistent", IdRol = 1 };
            _userRepositoryMock.Setup(r => r.GetUsuarioByNombreUsuarioAsync("nonexistent")).ReturnsAsync((Usuario?)null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ValidationException>(() => _sut.UpdateUserAsync(userDto));
            Assert.Contains("'nonexistent' not found", ex.Message);
        }

        [Fact]
        public async Task DeleteUserAsync_WhenUserExists_ShouldCallRepository()
        {
            // Arrange
            int userId = 1;
            _userRepositoryMock.Setup(r => r.DeleteUsuarioAsync(userId)).Returns(Task.CompletedTask);

            // Act
            await _sut.DeleteUserAsync(userId);

            // Assert
            _userRepositoryMock.Verify(r => r.DeleteUsuarioAsync(userId), Times.Once);
        }

        [Fact]
        public async Task GetAllUsersAsync_WhenUsersAndPersonasExist_ReturnsCorrectlyMappedDtos()
        {
            // Arrange
            var users = new List<Usuario>
            {
                new Usuario("user1", new byte[0], 1, 1, 1) { IdUsuario = 1 },
                new Usuario("user2", new byte[0], 2, 2, 1) { IdUsuario = 2 }
            };
            var personas = new List<Persona>
            {
                new Persona(1, "John", "Doe", 1, "123", DateTime.Now, "123", "street", "123", 1, 1, "john.doe@test.com", "123", DateTime.Now) { IdPersona = 1 },
                new Persona(2, "Jane", "Doe", 1, "456", DateTime.Now, "456", "street", "456", 1, 1, "jane.doe@test.com", "456", DateTime.Now) { IdPersona = 2 }
            };

            _userRepositoryMock.Setup(r => r.GetAllUsersAsync()).ReturnsAsync(users);
            _personaRepositoryMock.Setup(r => r.GetAllPersonas()).Returns(personas);

            // Act
            var result = await _sut.GetAllUsersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);

            var user1 = result.First(u => u.Username == "user1");
            Assert.Equal("John", user1.Nombre);
            Assert.Equal("Doe", user1.Apellido);
            Assert.Equal("john.doe@test.com", user1.Correo);

            var user2 = result.First(u => u.Username == "user2");
            Assert.Equal("Jane", user2.Nombre);
            Assert.Equal("Doe", user2.Apellido);
            Assert.Equal("jane.doe@test.com", user2.Correo);
        }

        [Fact]
        public void UpdatePoliticaSeguridad_WhenPolicyExists_CallsRepository()
        {
            // Arrange
            var politicaDto = new PoliticaSeguridadDto { IdPolitica = 1, MinCaracteres = 10, CantPreguntas = 3 };
            var politica = new PoliticaSeguridad(1, true, true, true, true, true, true, 8, 3);
            _securityRepositoryMock.Setup(r => r.GetPoliticaSeguridad()).Returns(politica);

            // Act
            _sut.UpdatePoliticaSeguridad(politicaDto);

            // Assert
            _securityRepositoryMock.Verify(r => r.UpdatePoliticaSeguridad(It.Is<PoliticaSeguridad>(p => p.MinCaracteres == 10)), Times.Once);
        }

        [Fact]
        public async Task CrearPersonaAsync_WithValidData_CallsFactoryAndRepository()
        {
            // Arrange
            var personaRequest = new PersonaRequest { Nombre = "Test", Apellido = "Person" };
            var persona = new Persona(1, "Test", "Person", 1, "12345678", System.DateTime.Now, "20123456789", "Test Street", "123", 1, 1, "test@example.com", "1234567890", System.DateTime.Now);
            _personaFactoryMock.Setup(f => f.Create(personaRequest)).Returns(persona);
            // AddPersona is still synchronous in the mock, as per the decision to not make all repos async yet
            _personaRepositoryMock.Setup(r => r.AddPersona(persona));

            // Act
            await _sut.CrearPersonaAsync(personaRequest);

            // Assert
            _personaFactoryMock.Verify(f => f.Create(personaRequest), Times.Once);
            _personaRepositoryMock.Verify(r => r.AddPersona(persona), Times.Once);
        }
    }
}
