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

            // Configuración de claves compuestas para tablas intermedias
            modelBuilder.Entity<RolPermiso>()
                .HasKey(rp => new { rp.IdRol, rp.IdPermiso });

            modelBuilder.Entity<PermisoUsuario>()
                .HasKey(pu => new { pu.IdUsuario, pu.IdPermiso });

            modelBuilder.Entity<RespuestaSeguridad>()
                .HasKey(rs => new { rs.IdUsuario, rs.IdPregunta });

            // Puedes agregar más configuraciones aquí si lo necesitas
        }
    }
}
