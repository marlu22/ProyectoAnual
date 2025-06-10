using System.Collections.Generic;
using BusinessLogic.Models;
using DataAccess.Entities;

public interface IUserService
{
    IEnumerable<UserDto> GetAllUsers();
    UserDto CreateUser(UserRequest request);

    void CrearPersona(PersonaRequest persona);
    void CrearUsuario(UserRequest usuario);
    List<PersonaDto> GetPersonas();

    List<TipoDoc> GetTiposDoc();
    List<Localidad> GetLocalidades();
    List<Genero> GetGeneros();
    List<Rol> GetRoles();
    void CambiarContrasena(string usuario, string nuevaContrasena);
    void RecuperarContrasena(string usuario, string[] respuestas);
}
