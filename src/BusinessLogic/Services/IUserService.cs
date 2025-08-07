// src/BusinessLogic/Services/IUserService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Models;

namespace BusinessLogic.Services
{
    public interface IUserService
    {
        void CrearPersona(PersonaRequest request);
        void CrearUsuario(UserRequest request);
        Task<AuthenticationResult> AuthenticateAsync(string username, string password);
        Task<AuthenticationResult> Validate2faAsync(string username, string code);
        Task RecuperarContrasena(string username, Dictionary<int, string> respuestas);
        void CambiarContrasena(string username, string newPassword, string oldPassword);
        List<TipoDocDto> GetTiposDoc();
        List<ProvinciaDto> GetProvincias();
        List<PartidoDto> GetPartidosByProvinciaId(int provinciaId);
        List<LocalidadDto> GetLocalidadesByPartidoId(int partidoId);
        List<GeneroDto> GetGeneros();
        List<PersonaDto> GetPersonas();
        List<RolDto> GetRoles();
        PoliticaSeguridadDto? GetPoliticaSeguridad();
        void UpdatePoliticaSeguridad(PoliticaSeguridadDto politica);
        List<UserDto> GetAllUsers();
        void UpdateUser(UserDto user);
        void DeleteUser(int userId);
        void UpdatePersona(PersonaDto persona);
        void DeletePersona(int personaId);
        void GuardarRespuestasSeguridad(string username, Dictionary<int, string> respuestas);
        List<PreguntaSeguridadDto> GetPreguntasSeguridad();
        List<PreguntaSeguridadDto> GetPreguntasDeUsuario(string username);
        UserDto? GetUserByUsername(string username);
        PersonaDto? GetPersonaById(int personaId);
    }
}