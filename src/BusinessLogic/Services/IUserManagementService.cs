using System.Collections.Generic;
using BusinessLogic.Models;

namespace BusinessLogic.Services
{
    public interface IUserManagementService
    {
        void CrearPersona(PersonaRequest request);
        void CrearUsuario(UserRequest request);
        void UpdateUser(UserDto user);
        void DeleteUser(int userId);
        void UpdatePersona(PersonaDto persona);
        void DeletePersona(int personaId);
        List<UserDto> GetAllUsers();
        UserDto? GetUserByUsername(string username);
        List<PersonaDto> GetPersonas();
        PersonaDto? GetPersonaById(int personaId);
        PoliticaSeguridadDto? GetPoliticaSeguridad();
        void UpdatePoliticaSeguridad(PoliticaSeguridadDto politica);
    }
}
