// src/BusinessLogic/Services/IUserService.cs
using System.Collections.Generic;
using BusinessLogic.Models;
using DataAccess.Entities;

namespace BusinessLogic.Services
{
    public interface IUserService
    {
        void CrearPersona(PersonaRequest request);
        void CrearUsuario(UserRequest request);
        UserResponse? Authenticate(string username, string password);
        void RecuperarContrasena(string username, string[] respuestas);
        void CambiarContrasena(string username, string newPassword);
        List<TipoDoc> GetTiposDoc();
        List<Localidad> GetLocalidades();
        List<Genero> GetGeneros();
        List<Persona> GetPersonas();
        List<Rol> GetRoles();
        PoliticaSeguridad? GetPoliticaSeguridad();
        void UpdatePoliticaSeguridad(PoliticaSeguridad politica);
        List<Usuario> GetAllUsers();
        void GuardarRespuestasSeguridad(string username, string[] respuestas);
    }
}