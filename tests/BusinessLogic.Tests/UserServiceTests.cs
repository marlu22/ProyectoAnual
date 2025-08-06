using Xunit;
using System.Threading.Tasks;
using BusinessLogic.Services;
using DataAccess.Repositories;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using BusinessLogic.Models;
using Microsoft.Extensions.Logging;

public class MockEmailService : IEmailService
{
    public Task SendPasswordResetEmailAsync(string toEmail, string newPassword)
    {
        // In a real test, you might want to record that this was called.
        return Task.CompletedTask;
    }

    public Task Send2faCodeEmailAsync(string toEmail, string code)
    {
        // In a real test, you might want to record that this was called.
        return Task.CompletedTask;
    }
}

public class MockUserRepository : IUserRepository
{
    private readonly List<Usuario> _users = new List<Usuario>();
    private readonly List<Persona> _personas = new List<Persona>();
    private PoliticaSeguridad _politica = new PoliticaSeguridad { Autenticacion2FA = false };

    public MockUserRepository()
    {
        var persona = new Persona { IdPersona = 1, Correo = "test@test.com", Nombre = "Test", Apellido = "User" };
        _personas.Add(persona);

        // Regular user
        _users.Add(new Usuario
        {
            IdUsuario = 1,
            UsuarioNombre = "testuser",
            ContrasenaScript = HashUsuarioContrasena("testuser", "password123"),
            IdPersona = 1,
            IdRol = 1,
            CambioContrasenaObligatorio = false,
            FechaBloqueo = new DateTime(9999, 12, 31),
            Rol = new Rol { IdRol = 1, Nombre = "Administrador" }
        });

        // Expired user
        _users.Add(new Usuario
        {
            IdUsuario = 2,
            UsuarioNombre = "expireduser",
            ContrasenaScript = HashUsuarioContrasena("expireduser", "password"),
            IdPersona = 1,
            IdRol = 1,
            CambioContrasenaObligatorio = false,
            FechaBloqueo = new DateTime(9999, 12, 31),
            FechaExpiracion = DateTime.Now.AddDays(-1), // Expired yesterday
            Rol = new Rol { IdRol = 1, Nombre = "Usuario" }
        });

        // Disabled user
        _users.Add(new Usuario
        {
            IdUsuario = 3,
            UsuarioNombre = "disableduser",
            ContrasenaScript = HashUsuarioContrasena("disableduser", "password"),
            IdPersona = 1,
            IdRol = 1,
            CambioContrasenaObligatorio = false,
            FechaBloqueo = DateTime.Now.AddDays(-1), // Blocked yesterday
            Rol = new Rol { IdRol = 1, Nombre = "Usuario" }
        });

        // User with 2FA
        _users.Add(new Usuario
        {
            IdUsuario = 4,
            UsuarioNombre = "2fauser",
            ContrasenaScript = HashUsuarioContrasena("2fauser", "password"),
            IdPersona = 1,
            IdRol = 1,
            CambioContrasenaObligatorio = false,
            FechaBloqueo = new DateTime(9999, 12, 31),
            Rol = new Rol { IdRol = 1, Nombre = "Usuario" }
        });
    }

    public void Enable2FA()
    {
        _politica = new PoliticaSeguridad { Autenticacion2FA = true };
    }

    public Usuario? GetUsuarioByNombreUsuario(string nombre) => _users.Find(u => u.UsuarioNombre == nombre);
    public PoliticaSeguridad? GetPoliticaSeguridad() => _politica;
    public Persona? GetPersonaById(int id) => _personas.Find(p => p.IdPersona == id);
    public void Set2faCode(string username, string? code, DateTime? expiry) {
        var user = _users.Find(u => u.UsuarioNombre == username);
        if (user != null)
        {
            user.Codigo2FA = code;
            user.Codigo2FAExpiracion = expiry;
        }
    }

    // Implement other IUserRepository methods as needed, throwing NotImplementedException for simplicity
    public void AddHistorialContrasena(HistorialContrasena historial) { /* Do nothing for this mock */ }
    public void AddPersona(Persona persona) => throw new NotImplementedException();
    public void AddRespuestaSeguridad(RespuestaSeguridad respuesta) => throw new NotImplementedException();
    public void AddUsuario(Usuario usuario) => throw new NotImplementedException();
    public void DeleteUsuario(int usuarioId) => throw new NotImplementedException();
    public List<Genero> GetAllGeneros() => throw new NotImplementedException();
    public List<Provincia> GetAllProvincias() => new List<Provincia>();
    public List<Partido> GetPartidosByProvinciaId(int provinciaId) => new List<Partido>();
    public List<Localidad> GetLocalidadesByPartidoId(int partidoId) => new List<Localidad>();
    public List<Persona> GetAllPersonas() => throw new NotImplementedException();
    public List<Rol> GetAllRoles() => throw new NotImplementedException();
    public List<TipoDoc> GetAllTiposDoc() => throw new NotImplementedException();
    public List<Usuario> GetAllUsers() => throw new NotImplementedException();
    public Genero? GetGeneroByNombre(string nombre) => throw new NotImplementedException();
    public List<HistorialContrasena> GetHistorialContrasenasByUsuarioId(int idUsuario) => new List<HistorialContrasena>();
    public Localidad? GetLocalidadByNombre(string nombre) => throw new NotImplementedException();
    public List<PreguntaSeguridad> GetPreguntasDeUsuario(string username) => throw new NotImplementedException();
    public List<PreguntaSeguridad> GetPreguntasSeguridad() => throw new NotImplementedException();
    public List<RespuestaSeguridad>? GetRespuestasSeguridadByUsuarioId(int idUsuario) => throw new NotImplementedException();
    public Rol? GetRolByNombre(string nombre) => throw new NotImplementedException();
    public TipoDoc? GetTipoDocByNombre(string nombre) => throw new NotImplementedException();
    public void UpdatePoliticaSeguridad(PoliticaSeguridad politica) => throw new NotImplementedException();
    public void UpdateUsuario(Usuario usuario) {
        var userIndex = _users.FindIndex(u => u.IdUsuario == usuario.IdUsuario);
        if (userIndex != -1)
        {
            _users[userIndex] = usuario;
        }
    }
    public void DeleteRespuestasSeguridadByUsuarioId(int usuarioId) => throw new NotImplementedException();

    public void UpdatePersona(Persona persona)
    {
        var index = _personas.FindIndex(p => p.IdPersona == persona.IdPersona);
        if (index != -1)
        {
            _personas[index] = persona;
        }
    }

    public void DeletePersona(int personaId)
    {
        _personas.RemoveAll(p => p.IdPersona == personaId);
    }

    private static byte[] HashUsuarioContrasena(string username, string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var saltedPassword = password + username;
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
        }
    }
}

public class UserServiceTests
{
    private readonly MockUserRepository _userRepository;
    private readonly IEmailService _emailService;
    private readonly IUserService _userService;

    public UserServiceTests()
    {
        _userRepository = new MockUserRepository();
        _emailService = new MockEmailService();
        _userService = new UserService(_userRepository, _emailService, new Microsoft.Extensions.Logging.Abstractions.NullLogger<UserService>());
    }

    [Fact]
    public async Task AuthenticateAsync_ValidCredentials_ReturnsSuccess()
    {
        // Arrange
        var username = "testuser";
        var password = "password123";

        // Act
        var result = await _userService.AuthenticateAsync(username, password);

        // Assert
        Assert.True(result.Success);
        Assert.False(result.Requires2fa);
        Assert.NotNull(result.User);
        Assert.Equal(username, result.User.Username);
    }

    [Fact]
    public async Task AuthenticateAsync_ExpiredUser_ReturnsFailure()
    {
        // Arrange
        var username = "expireduser";
        var password = "password";

        // Act
        var result = await _userService.AuthenticateAsync(username, password);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("La cuenta ha expirado.", result.ErrorMessage);
    }

    [Fact]
    public async Task AuthenticateAsync_DisabledUser_ReturnsFailure()
    {
        // Arrange
        var username = "disableduser";
        var password = "password";

        // Act
        var result = await _userService.AuthenticateAsync(username, password);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("La cuenta se encuentra deshabilitada.", result.ErrorMessage);
    }

    [Fact]
    public async Task AuthenticateAsync_IncorrectPassword_ReturnsFailure()
    {
        // Arrange
        var username = "testuser";
        var password = "wrongpassword";

        // Act
        var result = await _userService.AuthenticateAsync(username, password);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Usuario o contraseña incorrectos.", result.ErrorMessage);
    }

    [Fact]
    public async Task AuthenticateAsync_UserNotFound_ReturnsFailure()
    {
        // Arrange
        var username = "nonexistentuser";
        var password = "password";

        // Act
        var result = await _userService.AuthenticateAsync(username, password);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Usuario o contraseña incorrectos.", result.ErrorMessage);
    }

    [Fact]
    public async Task AuthenticateAsync_2faRequired_ReturnsRequires2fa()
    {
        // Arrange
        _userRepository.Enable2FA();
        var username = "2fauser";
        var password = "password";

        // Act
        var result = await _userService.AuthenticateAsync(username, password);

        // Assert
        Assert.True(result.Success);
        Assert.True(result.Requires2fa);
    }

    [Fact]
    public void CambiarContrasena_IncorrectOldPassword_ThrowsValidationException()
    {
        // Arrange
        var username = "testuser";
        var newPassword = "newPassword123";
        var incorrectOldPassword = "wrongpassword";

        // Act & Assert
        var exception = Assert.Throws<UserManagementSystem.BusinessLogic.Exceptions.ValidationException>(() =>
            _userService.CambiarContrasena(username, newPassword, incorrectOldPassword));

        Assert.Equal("La contraseña actual es incorrecta. Por favor, intente de nuevo.", exception.Message);
    }

    [Fact]
    public void UpdatePersona_UpdatesPersonaInRepository()
    {
        // Arrange
        var persona = new Persona { IdPersona = 1, Nombre = "Updated Name" };

        // Act
        _userService.UpdatePersona(persona);

        // Assert
        var updatedPersona = _userRepository.GetPersonaById(1);
        Assert.NotNull(updatedPersona);
        Assert.Equal("Updated Name", updatedPersona.Nombre);
    }

    [Fact]
    public void DeletePersona_RemovesPersonaFromRepository()
    {
        // Arrange
        var personaId = 1;

        // Act
        _userService.DeletePersona(personaId);

        // Assert
        var deletedPersona = _userRepository.GetPersonaById(1);
        Assert.Null(deletedPersona);
    }
}
