using System.Collections.Generic;
using BusinessLogic.Models;

namespace BusinessLogic.Services
{
    public interface ISecurityQuestionService
    {
        void GuardarRespuestasSeguridad(string username, Dictionary<int, string> respuestas);
        List<PreguntaSeguridadDto> GetPreguntasDeUsuario(string username);
        List<PreguntaSeguridadDto> GetPreguntasSeguridad();
        PoliticaSeguridadDto? GetPoliticaSeguridad();
    }
}
