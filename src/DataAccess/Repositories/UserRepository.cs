using System.Collections.Generic;
using System.Linq;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Usuarios
        public IEnumerable<Usuario> GetAll() => _context.Usuarios.ToList();

        public Usuario GetById(int id) => _context.Usuarios.FirstOrDefault(u => u.IdUsuario == id);

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

        public void AddUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        // Personas
        public void AddPersona(Persona persona)
        {
            _context.Personas.Add(persona);
            _context.SaveChanges();
        }

        public IEnumerable<Persona> GetAllPersonas() => _context.Personas.ToList();

        public Persona GetPersonaById(int id) => _context.Personas.FirstOrDefault(p => p.IdPersona == id);

        // Tipos de Documento
        public TipoDoc GetTipoDocByNombre(string nombre) => _context.TiposDoc.FirstOrDefault(t => t.Nombre == nombre);

        public IEnumerable<TipoDoc> GetAllTipoDocs() => _context.TiposDoc.ToList();

        // Localidades
        public Localidad GetLocalidadByNombre(string nombre) => _context.Localidades.FirstOrDefault(l => l.Nombre == nombre);

        public IEnumerable<Localidad> GetAllLocalidades() => _context.Localidades.ToList();

        // Géneros
        public Genero GetGeneroByNombre(string nombre) => _context.Generos.FirstOrDefault(g => g.Nombre == nombre);

        public IEnumerable<Genero> GetAllGeneros() => _context.Generos.ToList();

        // Roles
        public Rol GetRolByNombre(string nombre) => _context.Roles.FirstOrDefault(r => r.Nombre == nombre);

        public IEnumerable<Rol> GetAllRoles() => _context.Roles.ToList();

    }
}
