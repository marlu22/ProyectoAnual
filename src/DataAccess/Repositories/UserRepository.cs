// src/DataAccess/Repositories/UserRepository.cs
using System.Collections.Generic;
using System.Linq;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TipoDoc? GetTipoDocByNombre(string nombre)
        {
            return _context.TiposDoc.FirstOrDefault(t => t.Nombre == nombre);
        }

        public Localidad? GetLocalidadByNombre(string nombre)
        {
            return _context.Localidades.FirstOrDefault(l => l.Nombre == nombre);
        }

        public Genero? GetGeneroByNombre(string nombre)
        {
            return _context.Generos.FirstOrDefault(g => g.Nombre == nombre);
        }

        public Rol? GetRolByNombre(string nombre)
        {
            return _context.Roles.FirstOrDefault(r => r.Nombre == nombre);
        }

        public void AddPersona(Persona persona)
        {
            _context.Personas.Add(persona);
            _context.SaveChanges();
        }

        public void AddUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public Usuario? GetUsuarioByNombreUsuario(string nombre)
        {
            return _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefault(u => u.UsuarioNombre == nombre);
        }

        public void UpdateUsuario(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }

        public List<RespuestaSeguridad>? GetRespuestasSeguridadByUsuarioId(int idUsuario)
        {
            return _context.RespuestasSeguridad
                .Where(rs => rs.IdUsuario == idUsuario)
                .ToList();
        }

        public List<TipoDoc> GetAllTiposDoc()
        {
            return _context.TiposDoc.ToList();
        }

        public List<Localidad> GetAllLocalidades()
        {
            return _context.Localidades.ToList();
        }

        public List<Genero> GetAllGeneros()
        {
            return _context.Generos.ToList();
        }

        public List<Persona> GetAllPersonas()
        {
            return _context.Personas.ToList();
        }

        public Persona? GetPersonaById(int id)
        {
            return _context.Personas.Find(id);
        }

        public List<Rol> GetAllRoles()
        {
            return _context.Roles.ToList();
        }

        public PoliticaSeguridad? GetPoliticaSeguridad()
        {
            return _context.PoliticasSeguridad.FirstOrDefault();
        }

        public void UpdatePoliticaSeguridad(PoliticaSeguridad politica)
        {
            _context.PoliticasSeguridad.Update(politica);
            _context.SaveChanges();
        }

        public List<Usuario> GetAllUsers()
        {
            return _context.Usuarios
                .Include(u => u.Rol)
                .Include(u => u.Persona)
                .ToList();
        }

        public List<HistorialContrasena> GetHistorialContrasenasByUsuarioId(int idUsuario)
        {
            return _context.HistorialContrasenas
                .Where(h => h.IdUsuario == idUsuario)
                .ToList();
        }

        public void AddHistorialContrasena(HistorialContrasena historial)
        {
            _context.HistorialContrasenas.Add(historial);
            _context.SaveChanges();
        }

        public void AddRespuestaSeguridad(RespuestaSeguridad respuesta)
        {
            _context.RespuestasSeguridad.Add(respuesta);
            _context.SaveChanges();
        }
    }
}