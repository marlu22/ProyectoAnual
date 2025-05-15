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

    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Usuario> GetAll()
        {
            return _context.Usuarios.ToList();
        }

        public Usuario GetById(int id)
        {
            return _context.Usuarios.FirstOrDefault(u => u.IdUsuario == id);
        }

        public void Add(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public void Update(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }

        public void Delete(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
        }
    }
}
