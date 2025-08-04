using System;
using System.Collections.Generic;
using DataAccess.Entities;
using DataAccess.Repositories;
using BusinessLogic.Services;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using BusinessLogic.Models;

public class MockEmailService : IEmailService
{
    public Task SendPasswordResetEmailAsync(string toEmail, string newPassword)
    {
        Console.WriteLine($"Simulando envío de correo a {toEmail} con la contraseña {newPassword}");
        return Task.CompletedTask;
    }

    public Task Send2faCodeEmailAsync(string toEmail, string code)
    {
        Console.WriteLine($"Simulando envío de correo 2FA a {toEmail} con el código {code}");
        return Task.CompletedTask;
    }
}

public class MockUserRepository : IUserRepository
{
    private readonly List<Usuario> _users = new List<Usuario>();
    private readonly List<Persona> _personas = new List<Persona>();
    private readonly PoliticaSeguridad _politica = new PoliticaSeguridad { Autenticacion2FA = false };

    public MockUserRepository()
    {
        var username = "testuser";
        var password = "password123";
        var hashedPassword = HashUsuarioContrasena(username, password);

        var persona = new Persona { IdPersona = 1, Correo = "test@test.com", Nombre = "Test", Apellido = "User" };
        _personas.Add(persona);

        _users.Add(new Usuario
        {
            IdUsuario = 1,
            UsuarioNombre = username,
            ContrasenaScript = hashedPassword,
            IdPersona = 1,
            IdRol = 1,
            CambioContrasenaObligatorio = false,
            FechaBloqueo = new DateTime(9999, 12, 31),
            Rol = new Rol { IdRol = 1, Nombre = "Administrador" }
        });
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
    public void AddHistorialContrasena(HistorialContrasena historial) => throw new NotImplementedException();
    public void AddPersona(Persona persona) => throw new NotImplementedException();
    public void AddRespuestaSeguridad(RespuestaSeguridad respuesta) => throw new NotImplementedException();
    public void AddUsuario(Usuario usuario) => throw new NotImplementedException();
    public void DeleteUsuario(int usuarioId) => throw new NotImplementedException();
    public List<Genero> GetAllGeneros() => throw new NotImplementedException();
    public List<Localidad> GetAllLocalidades() => throw new NotImplementedException();
    public List<Persona> GetAllPersonas() => throw new NotImplementedException();
    public List<Rol> GetAllRoles() => throw new NotImplementedException();
    public List<TipoDoc> GetAllTiposDoc() => throw new NotImplementedException();
    public List<Usuario> GetAllUsers() => throw new NotImplementedException();
    public Genero? GetGeneroByNombre(string nombre) => throw new NotImplementedException();
    public List<HistorialContrasena> GetHistorialContrasenasByUsuarioId(int idUsuario) => throw new NotImplementedException();
    public Localidad? GetLocalidadByNombre(string nombre) => throw new NotImplementedException();
    public List<PreguntaSeguridad> GetPreguntasDeUsuario(string username) => throw new NotImplementedException();
    public List<PreguntaSeguridad> GetPreguntasSeguridad() => throw new NotImplementedException();
    public List<RespuestaSeguridad>? GetRespuestasSeguridadByUsuarioId(int idUsuario) => throw new NotImplementedException();
    public Rol? GetRolByNombre(string nombre) => throw new NotImplementedException();
    public TipoDoc? GetTipoDocByNombre(string nombre) => throw new NotImplementedException();
    public void UpdatePoliticaSeguridad(PoliticaSeguridad politica) => throw new NotImplementedException();
    public void UpdateUsuario(Usuario usuario) => throw new NotImplementedException();

    private static byte[] HashUsuarioContrasena(string username, string password)
    {
        using (var sha256 = SHA256.Create())
        {
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(username + password));
        }
    }
}

public class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            Console.WriteLine("Iniciando prueba de autenticación...");

            var userRepository = new MockUserRepository();
            var emailService = new MockEmailService();
            var userService = new UserService(userRepository, emailService);

            var username = "testuser";
            var password = "password123";

            Console.WriteLine("Intentando autenticar...");
            var authResult = await userService.AuthenticateAsync(username, password);

            if (authResult.Success && !authResult.Requires2fa)
            {
                Console.WriteLine($"¡Autenticación exitosa para {authResult.User?.Username}!");
                Console.WriteLine("La solución funciona correctamente.");
            }
            else if (authResult.Requires2fa)
            {
                 Console.WriteLine("Error: La autenticación requiere 2FA, pero no se esperaba en esta prueba.");
            }
            else
            {
                Console.WriteLine($"Error: La autenticación falló. Motivo: {authResult.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Se produjo una excepción inesperada: {ex.ToString()}");
        }
    }
}
