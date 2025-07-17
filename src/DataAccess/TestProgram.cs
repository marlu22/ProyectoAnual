// src/DataAccess/TestProgram.cs
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DataAccess
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer("Server=localhost;Database=login2;Trusted_Connection=True;TrustServerCertificate=True;")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var adminUser = await context.Usuarios
                    .Include(u => u.Rol)
                    .FirstOrDefaultAsync(u => u.UsuarioNombre == "admin");
                if (adminUser != null)
                {
                    Console.WriteLine($"User: {adminUser.UsuarioNombre}, CambioContrasenaObligatorio: {adminUser.CambioContrasenaObligatorio}, Rol: {adminUser.Rol?.Nombre}");
                }

                var politica = await context.PoliticasSeguridad.FirstOrDefaultAsync();
                if (politica != null)
                {
                    Console.WriteLine($"PoliticaSeguridad: MayusYMinus={politica.MayusYMinus}, MinCaracteres={politica.MinCaracteres}, CantPreguntas={politica.CantPreguntas}");
                }

                var provinciasCount = await context.Provincias.CountAsync();
                Console.WriteLine($"Provincias: {provinciasCount}"); // Should print 24
            }
        }
    }
}