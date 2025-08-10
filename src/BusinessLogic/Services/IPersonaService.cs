using System.Collections.Generic;
using BusinessLogic.Models;

namespace BusinessLogic.Services
{
    public interface IPersonaService
    {
        void CrearPersona(PersonaRequest request);
        void UpdatePersona(PersonaDto persona);
        void DeletePersona(int personaId);
        List<PersonaDto> GetPersonas();
        PersonaDto? GetPersonaById(int personaId);
    }
}
