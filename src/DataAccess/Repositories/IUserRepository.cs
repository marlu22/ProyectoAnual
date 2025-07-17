// src/DataAccess/Repositories/IUserRepository.cs
using System.Collections.Generic;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public interface IUserRepository
    {
        TipoDoc? GetTipoDocByNombre(string nombre);
        Localidad? GetLocalidadByNombre(string nombre);
        Genero? GetGeneroByNombre(string nombre);
        Rol? GetRolByNombre(string nombre);
        void AddPersona(Persona persona);
        void AddUsuario(Usuario usuario);
        Usuario? GetUsuarioByNombreUsuario(string nombre);
        void UpdateUsuario(Usuario usuario);
        List<RespuestaSeguridad>? GetRespuestasSeguridadByUsuarioId(int idUsuario);
        List<TipoDoc> GetAllTiposDoc();
        List<Localidad> GetAllLocalidades();
        List<Genero> GetAllGeneros();
        List<Persona> GetAllPersonas();
        List<Rol> GetAllRoles();
        PoliticaSeguridad? GetPoliticaSeguridad();
        void UpdatePoliticaSeguridad(PoliticaSeguridad politica);
        List<Usuario> GetAllUsers();
    }
}