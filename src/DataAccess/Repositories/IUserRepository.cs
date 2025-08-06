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
        void Set2faCode(string username, string? code, System.DateTime? expiry);
        List<RespuestaSeguridad>? GetRespuestasSeguridadByUsuarioId(int idUsuario);
        List<TipoDoc> GetAllTiposDoc();
        List<Provincia> GetAllProvincias();
        List<Partido> GetPartidosByProvinciaId(int provinciaId);
        List<Localidad> GetLocalidadesByPartidoId(int partidoId);
        List<Genero> GetAllGeneros();
        List<Persona> GetAllPersonas();
        Persona? GetPersonaById(int id);
        List<Rol> GetAllRoles();
        PoliticaSeguridad? GetPoliticaSeguridad();
        void UpdatePoliticaSeguridad(PoliticaSeguridad politica);
        List<Usuario> GetAllUsers();
        List<HistorialContrasena> GetHistorialContrasenasByUsuarioId(int idUsuario);
        void AddHistorialContrasena(HistorialContrasena historial);
        void AddRespuestaSeguridad(RespuestaSeguridad respuesta);
        List<PreguntaSeguridad> GetPreguntasSeguridad();
        void DeleteUsuario(int usuarioId);
        void DeleteRespuestasSeguridadByUsuarioId(int usuarioId);
        void UpdatePersona(Persona persona);
        void DeletePersona(int personaId);
    }
}