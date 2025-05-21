using Microsoft.EntityFrameworkCore;
using DataAccess.Entities;

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

            // Tipos de documento
            modelBuilder.Entity<TipoDoc>(entity =>
            {
                entity.ToTable("tipo_doc");
                entity.HasKey(e => e.IdTipoDoc);
                entity.Property(e => e.IdTipoDoc).HasColumnName("id_tipo_doc");
                entity.Property(e => e.Nombre).HasColumnName("tipo_doc");
            });

            // Géneros
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
                // Agrega aquí el mapeo de las demás columnas según tu modelo y SQL
            });

            // Contactos
            modelBuilder.Entity<Contacto>(entity =>
            {
                entity.ToTable("contactos");
                entity.HasKey(e => e.IdContacto);
                entity.Property(e => e.IdContacto).HasColumnName("id_contacto");
                // Agrega aquí el mapeo de las demás columnas según tu modelo y SQL
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
                // Agrega aquí el mapeo de las demás columnas según tu modelo y SQL
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

            // RolPermiso (tabla intermedia)
            modelBuilder.Entity<RolPermiso>(entity =>
            {
                entity.ToTable("rol_permiso");
                entity.HasKey(e => new { e.IdRol, e.IdPermiso });
                entity.Property(e => e.IdRol).HasColumnName("id_rol");
                entity.Property(e => e.IdPermiso).HasColumnName("id_permiso");
            });

            // PermisoUsuario (tabla intermedia)
            modelBuilder.Entity<PermisoUsuario>(entity =>
            {
                entity.ToTable("permiso_usuario");
                entity.HasKey(e => new { e.IdUsuario, e.IdPermiso });
                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
                entity.Property(e => e.IdPermiso).HasColumnName("id_permiso");
            });

            // HistorialContrasena
            modelBuilder.Entity<HistorialContrasena>(entity =>
            {
                entity.ToTable("historial_contrasena");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
                entity.Property(e => e.FechaCambio).HasColumnName("fecha_cambio");
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

            // RespuestaSeguridad (tabla intermedia)
            modelBuilder.Entity<RespuestaSeguridad>(entity =>
            {
                entity.ToTable("respuestas_seguridad");
                entity.HasKey(e => new { e.IdUsuario, e.IdPregunta });
                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
                entity.Property(e => e.IdPregunta).HasColumnName("id_pregunta");
                // Agrega aquí el mapeo de las demás columnas según tu modelo y SQL
            });

            // PoliticaSeguridad
            modelBuilder.Entity<PoliticaSeguridad>(entity =>
            {
                entity.ToTable("politicas_seguridad");
                entity.HasKey(e => e.IdPolitica);
                entity.Property(e => e.IdPolitica).HasColumnName("id_politica");
                // Agrega aquí el mapeo de las demás columnas según tu modelo y SQL
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
    }
}
