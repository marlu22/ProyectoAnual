// src/DataAccess/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using DataAccess.Entities;
using System.Security.Cryptography;
using System.Text;

namespace DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Provincia> Provincias { get; set; } = null!;
        public DbSet<Partido> Partidos { get; set; } = null!;
        public DbSet<Localidad> Localidades { get; set; } = null!;
        public DbSet<TipoDoc> TiposDoc { get; set; } = null!;
        public DbSet<Genero> Generos { get; set; } = null!;
        public DbSet<Persona> Personas { get; set; } = null!;
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Rol> Roles { get; set; } = null!;
        public DbSet<Permiso> Permisos { get; set; } = null!;
        public DbSet<RolPermiso> RolPermisos { get; set; } = null!;
        public DbSet<PermisoUsuario> PermisosUsuarios { get; set; } = null!;
        public DbSet<HistorialContrasena> HistorialContrasenas { get; set; } = null!;
        public DbSet<PreguntaSeguridad> PreguntasSeguridad { get; set; } = null!;
        public DbSet<RespuestaSeguridad> RespuestasSeguridad { get; set; } = null!;
        public DbSet<PoliticaSeguridad> PoliticasSeguridad { get; set; } = null!;
        public DbSet<Contacto> Contactos { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mark Contacto as keyless
            modelBuilder.Entity<Contacto>().HasNoKey();

            // Explicitly define primary keys
            modelBuilder.Entity<Genero>().HasKey(g => g.IdGenero);
            modelBuilder.Entity<Provincia>().HasKey(p => p.IdProvincia);
            modelBuilder.Entity<Partido>().HasKey(p => p.IdPartido);
            modelBuilder.Entity<Localidad>().HasKey(l => l.IdLocalidad);
            modelBuilder.Entity<TipoDoc>().HasKey(t => t.IdTipoDoc);
            modelBuilder.Entity<Persona>().HasKey(p => p.IdPersona);
            modelBuilder.Entity<Usuario>().HasKey(u => u.IdUsuario);
            modelBuilder.Entity<Rol>().HasKey(r => r.IdRol);
            modelBuilder.Entity<Permiso>().HasKey(p => p.IdPermiso);
            modelBuilder.Entity<RolPermiso>().HasKey(rp => new { rp.IdRol, rp.IdPermiso });
            modelBuilder.Entity<PermisoUsuario>().HasKey(pu => new { pu.IdUsuario, pu.IdPermiso });
            modelBuilder.Entity<HistorialContrasena>().HasKey(h => h.IdHistorial);
            modelBuilder.Entity<PreguntaSeguridad>().HasKey(ps => ps.IdPregunta);
            modelBuilder.Entity<RespuestaSeguridad>().HasKey(rs => rs.IdRespuesta);
            modelBuilder.Entity<PoliticaSeguridad>().HasKey(ps => ps.IdPolitica);

            // TipoDoc (2 rows)
            modelBuilder.Entity<TipoDoc>().HasData(
                new TipoDoc { IdTipoDoc = 1, Nombre = "DNI" },
                new TipoDoc { IdTipoDoc = 2, Nombre = "Pasaporte" }
            );

            // Generos (2 rows)
            modelBuilder.Entity<Genero>().HasData(
                new Genero { IdGenero = 1, Nombre = "Masculino" },
                new Genero { IdGenero = 2, Nombre = "Femenino" }
            );

            // Provincias (24 rows)
            modelBuilder.Entity<Provincia>().HasData(
                new Provincia { IdProvincia = 1, Nombre = "Buenos Aires" },
                new Provincia { IdProvincia = 2, Nombre = "Catamarca" },
                new Provincia { IdProvincia = 3, Nombre = "Chaco" },
                new Provincia { IdProvincia = 4, Nombre = "Chubut" },
                new Provincia { IdProvincia = 5, Nombre = "Córdoba" },
                new Provincia { IdProvincia = 6, Nombre = "Corrientes" },
                new Provincia { IdProvincia = 7, Nombre = "Entre Ríos" },
                new Provincia { IdProvincia = 8, Nombre = "Formosa" },
                new Provincia { IdProvincia = 9, Nombre = "Jujuy" },
                new Provincia { IdProvincia = 10, Nombre = "La Pampa" },
                new Provincia { IdProvincia = 11, Nombre = "La Rioja" },
                new Provincia { IdProvincia = 12, Nombre = "Mendoza" },
                new Provincia { IdProvincia = 13, Nombre = "Misiones" },
                new Provincia { IdProvincia = 14, Nombre = "Neuquén" },
                new Provincia { IdProvincia = 15, Nombre = "Río Negro" },
                new Provincia { IdProvincia = 16, Nombre = "Salta" },
                new Provincia { IdProvincia = 17, Nombre = "San Juan" },
                new Provincia { IdProvincia = 18, Nombre = "San Luis" },
                new Provincia { IdProvincia = 19, Nombre = "Santa Cruz" },
                new Provincia { IdProvincia = 20, Nombre = "Santa Fe" },
                new Provincia { IdProvincia = 21, Nombre = "Santiago del Estero" },
                new Provincia { IdProvincia = 22, Nombre = "Tierra del Fuego" },
                new Provincia { IdProvincia = 23, Nombre = "Tucumán" },
                new Provincia { IdProvincia = 24, Nombre = "CABA" }
            );

            // Partidos (10 rows)
            modelBuilder.Entity<Partido>().HasData(
                new Partido { IdPartido = 1, IdProvincia = 1, Nombre = "La Plata" },
                new Partido { IdPartido = 2, IdProvincia = 1, Nombre = "Quilmes" },
                new Partido { IdPartido = 3, IdProvincia = 1, Nombre = "Lomas de Zamora" },
                new Partido { IdPartido = 4, IdProvincia = 5, Nombre = "Córdoba Capital" },
                new Partido { IdPartido = 5, IdProvincia = 5, Nombre = "Río Cuarto" },
                new Partido { IdPartido = 6, IdProvincia = 12, Nombre = "Mendoza Capital" },
                new Partido { IdPartido = 7, IdProvincia = 12, Nombre = "Godoy Cruz" },
                new Partido { IdPartido = 8, IdProvincia = 20, Nombre = "Rosario" },
                new Partido { IdPartido = 9, IdProvincia = 20, Nombre = "Santa Fe Capital" },
                new Partido { IdPartido = 10, IdProvincia = 24, Nombre = "Comuna 1" }
            );

            // Localidades (33 rows)
            modelBuilder.Entity<Localidad>().HasData(
                new Localidad { IdLocalidad = 1, IdPartido = 1, Nombre = "La Plata" },
                new Localidad { IdLocalidad = 2, IdPartido = 1, Nombre = "City Bell" },
                new Localidad { IdLocalidad = 3, IdPartido = 1, Nombre = "Gonnet" },
                new Localidad { IdLocalidad = 4, IdPartido = 2, Nombre = "Quilmes" },
                new Localidad { IdLocalidad = 5, IdPartido = 2, Nombre = "Bernal" },
                new Localidad { IdLocalidad = 6, IdPartido = 3, Nombre = "Lomas de Zamora" },
                new Localidad { IdLocalidad = 7, IdPartido = 3, Nombre = "Banfield" },
                new Localidad { IdLocalidad = 8, IdPartido = 4, Nombre = "Córdoba" },
                new Localidad { IdLocalidad = 9, IdPartido = 4, Nombre = "Alta Córdoba" },
                new Localidad { IdLocalidad = 10, IdPartido = 5, Nombre = "Río Cuarto" },
                new Localidad { IdLocalidad = 11, IdPartido = 5, Nombre = "Las Higueras" },
                new Localidad { IdLocalidad = 12, IdPartido = 6, Nombre = "Mendoza" },
                new Localidad { IdLocalidad = 13, IdPartido = 6, Nombre = "Guaymallén" },
                new Localidad { IdLocalidad = 14, IdPartido = 7, Nombre = "Godoy Cruz" },
                new Localidad { IdLocalidad = 15, IdPartido = 7, Nombre = "Las Heras" },
                new Localidad { IdLocalidad = 16, IdPartido = 8, Nombre = "Rosario" },
                new Localidad { IdLocalidad = 17, IdPartido = 8, Nombre = "Funes" },
                new Localidad { IdLocalidad = 18, IdPartido = 9, Nombre = "Santa Fe" },
                new Localidad { IdLocalidad = 19, IdPartido = 9, Nombre = "Santo Tomé" },
                new Localidad { IdLocalidad = 20, IdPartido = 10, Nombre = "Retiro" },
                new Localidad { IdLocalidad = 21, IdPartido = 10, Nombre = "San Nicolás" },
                new Localidad { IdLocalidad = 22, IdPartido = 1, Nombre = "Tolosa" },
                new Localidad { IdLocalidad = 23, IdPartido = 2, Nombre = "Ezpeleta" },
                new Localidad { IdLocalidad = 24, IdPartido = 3, Nombre = "Temperley" },
                new Localidad { IdLocalidad = 25, IdPartido = 4, Nombre = "Nueva Córdoba" },
                new Localidad { IdLocalidad = 26, IdPartido = 5, Nombre = "Holmberg" },
                new Localidad { IdLocalidad = 27, IdPartido = 6, Nombre = "Luján de Cuyo" },
                new Localidad { IdLocalidad = 28, IdPartido = 7, Nombre = "Maipú" },
                new Localidad { IdLocalidad = 29, IdPartido = 8, Nombre = "Roldán" },
                new Localidad { IdLocalidad = 30, IdPartido = 9, Nombre = "Recreo" },
                new Localidad { IdLocalidad = 31, IdPartido = 10, Nombre = "Recoleta" },
                new Localidad { IdLocalidad = 32, IdPartido = 10, Nombre = "Palermo" },
                new Localidad { IdLocalidad = 33, IdPartido = 10, Nombre = "Belgrano" }
            );

            // Roles (1 row)
            modelBuilder.Entity<Rol>().HasData(
                new Rol { IdRol = 1, Nombre = "Administrador" }
            );

            // Personas (1 row)
            modelBuilder.Entity<Persona>().HasData(
            new Persona
            {
                IdPersona = 1,
                Legajo = "1001", // Likely causing CS0029 if Legajo is string
                Nombre = "Juan",
                Apellido = "Pérez",
                IdTipoDoc = 1,
                NumDoc = "12345678",
                Cuil = "20-12345678-9",
                Calle = "Calle Falsa",
                Altura = "123",
                IdLocalidad = 1,
                IdGenero = 1,
                Correo = "juan.perez@example.com",
                FechaIngreso = new DateTime(2025, 1, 1)
            }
        );

            // Usuarios (1 row)
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    IdUsuario = 1,
                    IdPersona = 1,
                    UsuarioNombre = "admin",
                    ContrasenaScript = HashUsuarioContrasena("admin", "testpassword"),
                    IdRol = 1,
                    FechaUltimoCambio = new DateTime(2025, 1, 1),
                    FechaBloqueo = new DateTime(9999, 12, 31),
                    CambioContrasenaObligatorio = true
                }
            );

            // Configure relationships
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Persona)
                .WithMany()
                .HasForeignKey(u => u.IdPersona);

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany()
                .HasForeignKey(u => u.IdRol);

            modelBuilder.Entity<Persona>()
                .HasOne(p => p.TipoDoc)
                .WithMany()
                .HasForeignKey(p => p.IdTipoDoc);

            modelBuilder.Entity<Persona>()
                .HasOne(p => p.Localidad)
                .WithMany()
                .HasForeignKey(p => p.IdLocalidad);

            modelBuilder.Entity<Persona>()
                .HasOne(p => p.Genero)
                .WithMany()
                .HasForeignKey(p => p.IdGenero);

            modelBuilder.Entity<Localidad>()
                .HasOne(l => l.Partido)
                .WithMany()
                .HasForeignKey(l => l.IdPartido);

            modelBuilder.Entity<Partido>()
                .HasOne(p => p.Provincia)
                .WithMany()
                .HasForeignKey(p => p.IdProvincia);
                // src/DataAccess/ApplicationDbContext.cs (add to OnModelCreating)
            modelBuilder.Entity<PoliticaSeguridad>().HasData(
                new PoliticaSeguridad
                {
                    IdPolitica = 1,
                    MayusYMinus = true,
                    LetrasYNumeros = true,
                    CaracterEspecial = true,
                    Autenticacion2FA = false,
                    NoRepetirAnteriores = true,
                    SinDatosPersonales = true,
                    MinCaracteres = 8,
                    CantPreguntas = 2
                }
            );
        }

        private static byte[] HashUsuarioContrasena(string username, string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var salted = $"{username}:{password}";
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(salted));
            }
        }
    }
}