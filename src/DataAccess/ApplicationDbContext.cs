using Microsoft.EntityFrameworkCore;
using DataAccess.Entities;
using System;
using System.Linq;

namespace DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext() { }

        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Partido> Partidos { get; set; }
        public DbSet<Localidad> Localidades { get; set; }
        public DbSet<TipoDoc> TiposDoc { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Contacto> Contactos { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<RolPermiso> RolPermisos { get; set; }
        public DbSet<PermisoUsuario> PermisosUsuarios { get; set; }
        public DbSet<HistorialContrasena> HistorialContrasenas { get; set; }
        public DbSet<PreguntaSeguridad> PreguntasSeguridad { get; set; }
        public DbSet<RespuestaSeguridad> RespuestasSeguridad { get; set; }
        public DbSet<PoliticaSeguridad> PoliticasSeguridad { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Provincias
            modelBuilder.Entity<Provincia>(entity =>
            {
                entity.ToTable("provincias");
                entity.HasKey(e => e.IdProvincia);
                entity.Property(e => e.IdProvincia).HasColumnName("id_provincia");
                entity.Property(e => e.Nombre).HasColumnName("provincia");
            });

            // Partidos
            modelBuilder.Entity<Partido>(entity =>
            {
                entity.ToTable("partidos");
                entity.HasKey(e => e.IdPartido);
                entity.Property(e => e.IdPartido).HasColumnName("id_partido");
                entity.Property(e => e.Nombre).HasColumnName("partido");
                entity.Property(e => e.IdProvincia).HasColumnName("id_provincia");
            });

            // Localidades
            modelBuilder.Entity<Localidad>(entity =>
            {
                entity.ToTable("localidades");
                entity.HasKey(e => e.IdLocalidad);
                entity.Property(e => e.IdLocalidad).HasColumnName("id_localidad");
                entity.Property(e => e.Nombre).HasColumnName("localidad");
                entity.Property(e => e.IdPartido).HasColumnName("id_partido");
            });

            // TipoDoc
            modelBuilder.Entity<TipoDoc>(entity =>
            {
                entity.ToTable("tipo_doc");
                entity.HasKey(e => e.IdTipoDoc);
                entity.Property(e => e.IdTipoDoc).HasColumnName("id_tipo_doc");
                entity.Property(e => e.Nombre).HasColumnName("tipo_doc");
            });

            // Generos
            modelBuilder.Entity<Genero>(entity =>
            {
                entity.ToTable("generos");
                entity.HasKey(e => e.IdGenero);
                entity.Property(e => e.IdGenero).HasColumnName("id_genero");
                entity.Property(e => e.Nombre).HasColumnName("genero");
            });

            // Personas
            modelBuilder.Entity<Persona>(entity =>
            {
                entity.ToTable("personas");
                entity.HasKey(e => e.IdPersona);
                entity.Property(e => e.IdPersona).HasColumnName("id_persona");
                entity.Property(e => e.Legajo).HasColumnName("legajo");
                entity.Property(e => e.Nombre).HasColumnName("nombre");
                entity.Property(e => e.Apellido).HasColumnName("apellido");
                entity.Property(e => e.IdTipoDoc).HasColumnName("id_tipo_doc");
                entity.Property(e => e.NumDoc).HasColumnName("num_doc");
                entity.Property(e => e.Cuil).HasColumnName("cuil");
                entity.Property(e => e.Calle).HasColumnName("calle");
                entity.Property(e => e.Altura).HasColumnName("altura");
                entity.Property(e => e.IdLocalidad).HasColumnName("id_localidad");
                entity.Property(e => e.IdGenero).HasColumnName("id_genero");
                entity.Property(e => e.Correo).HasColumnName("correo");
            });

            // Contactos
            modelBuilder.Entity<Contacto>(entity =>
            {
                entity.ToTable("contactos");
                entity.HasKey(e => e.IdContacto);
                entity.Property(e => e.IdContacto).HasColumnName("id_contacto");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.Celular).HasColumnName("celular");
                entity.Property(e => e.IdPersona).HasColumnName("id_persona");
            });

            // Roles
            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("roles");
                entity.HasKey(e => e.IdRol);
                entity.Property(e => e.IdRol).HasColumnName("id_rol");
                entity.Property(e => e.Nombre).HasColumnName("rol");
            });

            // Usuarios
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios");
                entity.HasKey(e => e.IdUsuario);
                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
                entity.Property(e => e.UsuarioNombre).HasColumnName("usuario");
                entity.Property(e => e.ContrasenaScript).HasColumnName("contrasena_script");
                entity.Property(e => e.IdPersona).HasColumnName("id_persona");
                entity.Property(e => e.FechaBloqueo).HasColumnName("fecha_bloqueo");
                entity.Property(e => e.NombreUsuarioBloqueo).HasColumnName("nombre_usuario_bloqueo");
                entity.Property(e => e.FechaUltimoCambio).HasColumnName("fecha_ultimo_cambio");
                entity.Property(e => e.IdRol).HasColumnName("id_rol");
                entity.Property(e => e.CambioContrasenaObligatorio).HasColumnName("CambioContrasenaObligatorio");
            });

            // Permisos
            modelBuilder.Entity<Permiso>(entity =>
            {
                entity.ToTable("permisos");
                entity.HasKey(e => e.IdPermiso);
                entity.Property(e => e.IdPermiso).HasColumnName("id_permiso");
                entity.Property(e => e.Nombre).HasColumnName("permiso");
                entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            });

            // RolPermiso
            modelBuilder.Entity<RolPermiso>(entity =>
            {
                entity.ToTable("rol_permiso");
                entity.HasKey(e => new { e.IdRol, e.IdPermiso });
                entity.Property(e => e.IdRol).HasColumnName("id_rol");
                entity.Property(e => e.IdPermiso).HasColumnName("id_permiso");
            });

            // PermisoUsuario
            modelBuilder.Entity<PermisoUsuario>(entity =>
            {
                entity.ToTable("permisos_usuarios");
                entity.HasKey(e => new { e.IdUsuario, e.IdPermiso });
                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
                entity.Property(e => e.IdPermiso).HasColumnName("id_permiso");
                entity.Property(e => e.FechaVencimiento).HasColumnName("fecha_vencimiento");
            });

            // HistorialContrasena
            modelBuilder.Entity<HistorialContrasena>(entity =>
            {
                entity.ToTable("historial_contrasena");
                entity.HasKey(e => e.IdHistorial);
                entity.Property(e => e.IdHistorial).HasColumnName("id");
                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
                entity.Property(e => e.ContrasenaScript).HasColumnName("contrasena_script");
            });

            // PreguntaSeguridad
            modelBuilder.Entity<PreguntaSeguridad>(entity =>
            {
                entity.ToTable("preguntas_seguridad");
                entity.HasKey(e => e.IdPregunta);
                entity.Property(e => e.IdPregunta).HasColumnName("id_pregunta");
                entity.Property(e => e.Pregunta).HasColumnName("pregunta");
            });

            // RespuestaSeguridad
            modelBuilder.Entity<RespuestaSeguridad>(entity =>
            {
                entity.ToTable("respuestas_seguridad");
                entity.HasKey(e => new { e.IdUsuario, e.IdPregunta });
                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
                entity.Property(e => e.IdPregunta).HasColumnName("id_pregunta");
                entity.Property(e => e.Respuesta).HasColumnName("respuesta");
            });

            // PoliticaSeguridad
            modelBuilder.Entity<PoliticaSeguridad>(entity =>
            {
                entity.ToTable("politicas_seguridad");
                entity.HasKey(e => e.IdPolitica);
                entity.Property(e => e.IdPolitica).HasColumnName("id_politica");
                entity.Property(e => e.MinCaracteres).HasColumnName("min_caracteres");
                entity.Property(e => e.CantPreguntas).HasColumnName("cant_preguntas");
                entity.Property(e => e.MayusYMinus).HasColumnName("mayus_y_minus");
                entity.Property(e => e.LetrasYNumeros).HasColumnName("letras_y_numeros");
                entity.Property(e => e.CaracterEspecial).HasColumnName("caracter_especial");
                entity.Property(e => e.Autenticacion2FA).HasColumnName("autenticacion_2fa");
                entity.Property(e => e.NoRepetirAnteriores).HasColumnName("no_repetir_anteriores");
                entity.Property(e => e.SinDatosPersonales).HasColumnName("sin_datos_personales");
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Usa la cadena de conexión de tu appsettings.json o ponla aquí directamente para pruebas
                optionsBuilder.UseSqlServer("Server=localhost;Database=login2;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        public void TestConnection()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var usuarios = context.Usuarios.ToList();
                    // ...
                }
            }
            catch (Exception ex)
            {
                // Lanza la excepción para que la maneje la capa de presentación
                throw new Exception($"Error: {ex.Message}\nUsuario: {Environment.UserName}", ex);
            }
        }
    }
}
