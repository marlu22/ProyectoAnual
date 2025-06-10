using System.Collections.Generic;
using DataAccess.Entities;

public interface IUserRepository
{
    IEnumerable<Usuario> GetAll();
    Usuario GetById(int id);
    void Add(Usuario usuario);
    void Update(Usuario usuario);
    void Delete(Usuario usuario);

    void AddPersona(Persona persona);
    IEnumerable<Persona> GetAllPersonas();
    void AddUsuario(Usuario usuario);

    Persona GetPersonaById(int id);
    TipoDoc GetTipoDocByNombre(string nombre);
    Localidad GetLocalidadByNombre(string nombre);
    Genero GetGeneroByNombre(string nombre);
    Rol GetRolByNombre(string nombre);
    IEnumerable<TipoDoc> GetAllTipoDocs();
    IEnumerable<Localidad> GetAllLocalidades();
    IEnumerable<Genero> GetAllGeneros();
    IEnumerable<Rol> GetAllRoles();

    Usuario GetByUsername(string username);
    bool ValidarRespuestasSeguridad(int idUsuario, string[] respuestas);
    void EnviarCorreoRecuperacion(Usuario user, string nuevaContrasena);
}
