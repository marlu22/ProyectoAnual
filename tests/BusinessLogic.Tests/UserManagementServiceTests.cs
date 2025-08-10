using BusinessLogic.Services;
using BusinessLogic.Security;
using DataAccess.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using BusinessLogic.Models;
using DataAccess.Entities;
using System.Threading.Tasks;

namespace BusinessLogic.Tests
{
    public class UserManagementServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IEmailService> _emailServiceMock;
        private readonly Mock<ILogger<UserManagementService>> _loggerMock;
        private readonly Mock<IPasswordHasher> _passwordHasherMock;
        private readonly UserManagementService _sut; // System Under Test

        public UserManagementServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _emailServiceMock = new Mock<IEmailService>();
            _loggerMock = new Mock<ILogger<UserManagementService>>();
            _passwordHasherMock = new Mock<IPasswordHasher>();

            _sut = new UserManagementService(
                _userRepositoryMock.Object,
                _emailServiceMock.Object,
                _loggerMock.Object,
                _passwordHasherMock.Object
            );
        }

        [Fact]
        public void CrearUsuario_WithValidData_ShouldCallAddUsuarioAndSendEmail()
        {
            // Arrange
            var userRequest = new UserRequest
            {
                PersonaId = "1",
                Username = "testuser",
                Rol = "Usuario"
            };

            var persona = new Persona(123, "Test", "User", 1, "12345678", System.DateTime.Now, "12345678901", "Street", "123", 1, 1, "test@example.com", "123456", System.DateTime.Now);

            _userRepositoryMock.Setup(r => r.GetPersonaById(1)).Returns(persona);
            _userRepositoryMock.Setup(r => r.GetRolByNombre("Usuario")).Returns(new Rol { IdRol = 2, Nombre = "Usuario" });
            _passwordHasherMock.Setup(h => h.Hash("testuser", It.IsAny<string>())).Returns(new byte[] { 1, 2, 3 });

            // Act
            _sut.CrearUsuario(userRequest);

            // Assert
            _userRepositoryMock.Verify(r => r.AddUsuario(It.IsAny<Usuario>()), Times.Once);
            _emailServiceMock.Verify(s => s.SendPasswordResetEmailAsync(persona.Correo!, It.IsAny<string>()), Times.Once);
        }
    }
}
