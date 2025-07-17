using System;
using DataAccess;
using DataAccess.Entities;
using DataAccess.Repositories;
using BusinessLogic.Services;
using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Iniciando prueba de autenticación...");

            // 1. Crear DbContext
            var dbContext = new ApplicationDbContext();
            dbContext.Database.EnsureCreated(); // Asegura que la BD en memoria exista

            // 2. Agregar datos de prueba (Rol y Usuario)
            var rol = new Rol { Nombre = "Administrador" };
            dbContext.Roles.Add(rol);
            dbContext.SaveChanges();

            var username = "testuser";
            var password = "password123";
            var hashedPassword = UserService.HashUsuarioContrasena(username, password);

            var user = new Usuario
            {
                UsuarioNombre = username,
                ContrasenaScript = Encoding.UTF8.GetBytes(hashedPassword),
                IdPersona = 1, // ID de persona de prueba
                IdRol = rol.IdRol,
                FechaUltimoCambio = DateTime.Now,
                CambioContrasenaObligatorio = false,
                FechaBloqueo = null, // Importante: probando el caso nulo
                NombreUsuarioBloqueo = null // Importante: probando el caso nulo
            };
            dbContext.Usuarios.Add(user);
            dbContext.SaveChanges();

            Console.WriteLine("Usuario de prueba creado.");

            // 3. Crear servicios
            var userRepository = new UserRepository(dbContext);
            var userService = new UserService(userRepository);

            // 4. Autenticar
            Console.WriteLine("Intentando autenticar...");
            var authenticatedUser = userService.Authenticate(username, password);

            if (authenticatedUser != null)
            {
                Console.WriteLine($"¡Autenticación exitosa para {authenticatedUser.Username}!");
                Console.WriteLine("La solución funciona correctamente.");
            }
            else
            {
                Console.WriteLine("Error: La autenticación falló.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Se produjo una excepción inesperada: {ex.ToString()}");
        }
    }
}
