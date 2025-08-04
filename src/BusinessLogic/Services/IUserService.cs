// src/BusinessLogic/Services/IUserService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Models;
using DataAccess.Entities;

namespace BusinessLogic.Services
{
    public interface IUserService
    {
        void CrearPersona(PersonaRequest request);
        void CrearUsuario(UserRequest request);
        Task<AuthenticationResult> AuthenticateAsync(string username, string password);
        Task<UserResponse?> Validate2faAsync(string username, string code);
        Task RecuperarContrasena(string username, Dictionary<int, string> respuestas);
        void CambiarContrasena(string username, string newPassword, string oldPassword);
        List<TipoDoc> GetTiposDoc();
        List<Localidad> GetLocalidades();
        List<Genero> GetGeneros();
        List<Persona> GetPersonas();
        List<Rol> GetRoles();
        PoliticaSeguridad? GetPoliticaSeguridad();
        void UpdatePoliticaSeguridad(PoliticaSeguridad politica);
        List<Usuario> GetAllUsers();
        void UpdateUser(UserDto user);
        void DeleteUser(int userId);
        void GuardarRespuestasSeguridad(string username, Dictionary<int, string> respuestas);
        List<PreguntaSeguridad> GetPreguntasSeguridad();
        List<PreguntaSeguridad> GetPreguntasDeUsuario(string username);
    }
}