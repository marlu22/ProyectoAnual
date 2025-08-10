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
        private readonly Mock<IEmailService> _emailServiceMock;
        private readonly Mock<ILogger<UserManagementService>> _loggerMock;
        private readonly Mock<IPasswordHasher> _passwordHasherMock;
        private readonly Mock<IUsuarioFactory> _usuarioFactoryMock;
        private readonly Mock<IPersonaFactory> _personaFactoryMock;
        private readonly UserManagementService _sut; // System Under Test

        public UserManagementServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _emailServiceMock = new Mock<IEmailService>();
            _loggerMock = new Mock<ILogger<UserManagementService>>();
            _passwordHasherMock = new Mock<IPasswordHasher>();
            _usuarioFactoryMock = new Mock<IUsuarioFactory>();
            _personaFactoryMock = new Mock<IPersonaFactory>();

            _sut = new UserManagementService(
                _userRepositoryMock.Object,
                _emailServiceMock.Object,
                _loggerMock.Object,
                _passwordHasherMock.Object,
                _personaFactoryMock.Object,
                _usuarioFactoryMock.Object
            );
        }

        [Fact]
        public void CrearUsuario_WithValidData_ShouldCallAddUsuarioAndSendEmail()
        {
            // Arrange
            var userRequest = new UserRequest { PersonaId = "1", Username = "testuser", Rol = "Usuario" };
            var persona = new Persona(1, "Test", "User", 1, "12345678", System.DateTime.Now, "20123456789", "Test Street", "123", 1, 1, "test@example.com", "1234567890", System.DateTime.Now);
            var usuario = new Usuario("testuser", new byte[0], 1, 1, 1);
            var plainPassword = "plainPassword123";

            _usuarioFactoryMock.Setup(f => f.Create(userRequest)).Returns((usuario, plainPassword));
            _userRepositoryMock.Setup(r => r.GetPersonaById(1)).Returns(persona);


            // Act
            _sut.CrearUsuario(userRequest);

            // Assert
            _userRepositoryMock.Verify(r => r.AddUsuario(usuario), Times.Once);
            _emailServiceMock.Verify(s => s.SendPasswordResetEmailAsync(persona.Correo!, plainPassword), Times.Once);
        }

        [Fact]
        public void UpdateUser_WhenUserExists_ShouldUpdateAndCallRepository()
        {
            // Arrange
            var userDto = new UserDto { Username = "testuser", IdRol = 1, FechaExpiracion = null, Habilitado = true };
            var usuario = new Usuario("testuser", new byte[0], 1, 1, 1);
            _userRepositoryMock.Setup(r => r.GetUsuarioByNombreUsuario("testuser")).Returns(usuario);

            // Act
            _sut.UpdateUser(userDto);

            // Assert
            _userRepositoryMock.Verify(r => r.UpdateUsuario(It.Is<Usuario>(u => u.UsuarioNombre == "testuser")), Times.Once);
            Assert.True(usuario.FechaBloqueo > DateTime.Now); // Habilitado
        }

        [Fact]
        public void UpdateUser_WhenUserDoesNotExist_ShouldThrowValidationException()
        {
            // Arrange
            var userDto = new UserDto { Username = "nonexistent", IdRol = 1 };
            _userRepositoryMock.Setup(r => r.GetUsuarioByNombreUsuario("nonexistent")).Returns((Usuario)null);

            // Act & Assert
            var ex = Assert.Throws<ValidationException>(() => _sut.UpdateUser(userDto));
            Assert.Contains("'nonexistent' not found", ex.Message);
        }

        [Fact]
        public void DeleteUser_WhenUserExists_ShouldCallRepository()
        {
            // Arrange
            int userId = 1;

            // Act
            _sut.DeleteUser(userId);

            // Assert
            _userRepositoryMock.Verify(r => r.DeleteUsuario(userId), Times.Once);
        }

        [Fact]
        public void GetAllUsers_WhenUsersExist_ReturnsMappedDtos()
        {
            // Arrange
            // In a unit test, we cannot easily mock the Persona navigation property.
            // The mapper is designed to handle a null Persona, so we test that path.
            var users = new List<Usuario>
            {
                new Usuario("user1", new byte[0], 1, 1, 1),
                new Usuario("user2", new byte[0], 2, 2, 1)
            };
            _userRepositoryMock.Setup(r => r.GetAllUsers()).Returns(users);

            // Act
            var result = _sut.GetAllUsers();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("user1", result[0].Username);
            Assert.Equal("N/A", result[0].NombreCompleto); // Expect "N/A" as Persona is null
        }

        [Fact]
        public void UpdatePoliticaSeguridad_WhenPolicyExists_CallsRepository()
        {
            // Arrange
            var politicaDto = new PoliticaSeguridadDto { IdPolitica = 1, MinCaracteres = 10, CantPreguntas = 3 };
            var politica = new PoliticaSeguridad(1, true, true, true, true, true, true, 8, 3);
            _userRepositoryMock.Setup(r => r.GetPoliticaSeguridad()).Returns(politica);

            // Act
            _sut.UpdatePoliticaSeguridad(politicaDto);

            // Assert
            _userRepositoryMock.Verify(r => r.UpdatePoliticaSeguridad(It.Is<PoliticaSeguridad>(p => p.MinCaracteres == 10)), Times.Once);
        }

        [Fact]
        public void CrearPersona_WithValidData_CallsFactoryAndRepository()
        {
            // Arrange
            var personaRequest = new PersonaRequest { Nombre = "Test", Apellido = "Person" };
            var persona = new Persona(1, "Test", "Person", 1, "12345678", System.DateTime.Now, "20123456789", "Test Street", "123", 1, 1, "test@example.com", "1234567890", System.DateTime.Now);
            _personaFactoryMock.Setup(f => f.Create(personaRequest)).Returns(persona);

            // Act
            _sut.CrearPersona(personaRequest);

            // Assert
            _personaFactoryMock.Verify(f => f.Create(personaRequest), Times.Once);
            _userRepositoryMock.Verify(r => r.AddPersona(persona), Times.Once);
        }
    }
}
