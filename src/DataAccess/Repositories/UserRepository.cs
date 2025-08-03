// src/DataAccess/Repositories/UserRepository.cs
using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UserManagementSystem.DataAccess.Exceptions;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private T ExecuteDbOperation<T>(Func<T> operation, string operationName)
        {
            try
            {
                return operation();
            }
            catch (SqlException ex)
            {
                throw new InfrastructureException($"A database error occurred during {operationName}.", ex);
            }
            catch (Exception ex)
            {
                throw new InfrastructureException($"An unexpected error occurred during {operationName}.", ex);
            }
        }

        private void ExecuteDbOperation(Action operation, string operationName)
        {
            try
            {
                operation();
            }
            catch (SqlException ex)
            {
                throw new InfrastructureException($"A database error occurred during {operationName}.", ex);
            }
            catch (Exception ex)
            {
                throw new InfrastructureException($"An unexpected error occurred during {operationName}.", ex);
            }
        }

        public TipoDoc? GetTipoDocByNombre(string nombre) => ExecuteDbOperation(() => _context.TiposDoc.FirstOrDefault(t => t.Nombre == nombre), "getting document type by name");

        public Localidad? GetLocalidadByNombre(string nombre) => ExecuteDbOperation(() => _context.Localidades.FirstOrDefault(l => l.Nombre == nombre), "getting location by name");

        public Genero? GetGeneroByNombre(string nombre) => ExecuteDbOperation(() => _context.Generos.FirstOrDefault(g => g.Nombre == nombre), "getting gender by name");

        public Rol? GetRolByNombre(string nombre) => ExecuteDbOperation(() => _context.Roles.FirstOrDefault(r => r.Nombre == nombre), "getting role by name");

        public void AddPersona(Persona persona) => ExecuteDbOperation(() =>
        {
            _context.Personas.Add(persona);
            _context.SaveChanges();
        }, "adding a person");

        public void AddUsuario(Usuario usuario) => ExecuteDbOperation(() =>
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }, "adding a user");

        public Usuario? GetUsuarioByNombreUsuario(string nombre) => ExecuteDbOperation(() => _context.Usuarios
            .Include(u => u.Rol)
            .FirstOrDefault(u => u.UsuarioNombre == nombre), "getting user by username");

        public void UpdateUsuario(Usuario usuario) => ExecuteDbOperation(() =>
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }, "updating a user");

        public List<RespuestaSeguridad>? GetRespuestasSeguridadByUsuarioId(int idUsuario) => ExecuteDbOperation(() => _context.RespuestasSeguridad
            .Where(rs => rs.IdUsuario == idUsuario)
            .ToList(), "getting security answers by user id");

        public List<TipoDoc> GetAllTiposDoc() => ExecuteDbOperation(() => _context.TiposDoc.ToList(), "getting all document types");

        public List<Localidad> GetAllLocalidades() => ExecuteDbOperation(() => _context.Localidades.ToList(), "getting all locations");

        public List<Genero> GetAllGeneros() => ExecuteDbOperation(() => _context.Generos.ToList(), "getting all genders");

        public List<Persona> GetAllPersonas() => ExecuteDbOperation(() => _context.Personas.ToList(), "getting all people");

        public Persona? GetPersonaById(int id) => ExecuteDbOperation(() => _context.Personas.Find(id), "getting person by id");

        public List<Rol> GetAllRoles() => ExecuteDbOperation(() => _context.Roles.ToList(), "getting all roles");

        public PoliticaSeguridad? GetPoliticaSeguridad() => ExecuteDbOperation(() => _context.PoliticasSeguridad.FirstOrDefault(), "getting security policy");

        public void UpdatePoliticaSeguridad(PoliticaSeguridad politica) => ExecuteDbOperation(() =>
        {
            _context.Set<PoliticaSeguridad>().Update(politica);
            _context.SaveChanges();
        }, "updating security policy");

        public List<Usuario> GetAllUsers() => ExecuteDbOperation(() => _context.Usuarios
            .Include(u => u.Rol)
            .Include(u => u.Persona)
            .ToList(), "getting all users");

        public List<HistorialContrasena> GetHistorialContrasenasByUsuarioId(int idUsuario) => ExecuteDbOperation(() => _context.HistorialContrasenas
            .Where(h => h.IdUsuario == idUsuario)
            .ToList(), "getting password history by user id");

        public void AddHistorialContrasena(HistorialContrasena historial) => ExecuteDbOperation(() =>
        {
            _context.HistorialContrasenas.Add(historial);
            _context.SaveChanges();
        }, "adding password history");

        public void DeleteUsuario(int usuarioId) => ExecuteDbOperation(() =>
        {
            var usuario = _context.Usuarios.Find(usuarioId);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();
            }
        }, "deleting a user");

        public void AddRespuestaSeguridad(RespuestaSeguridad respuesta) => ExecuteDbOperation(() =>
        {
            _context.RespuestasSeguridad.Add(respuesta);
            _context.SaveChanges();
        }, "adding security answer");

        public List<PreguntaSeguridad> GetPreguntasSeguridad() => ExecuteDbOperation(() => _context.PreguntasSeguridad.ToList(), "getting all security questions");
    }
}