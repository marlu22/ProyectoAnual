using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Models;

namespace BusinessLogic.Services
{
    public interface IUserAuthenticationService
    {
        Task<AuthenticationResult> AuthenticateAsync(string username, string password);
        Task<AuthenticationResult> Validate2faAsync(string username, string code);
        Task RecuperarContrasena(string username, Dictionary<int, string> respuestas);
        void CambiarContrasena(string username, string newPassword, string oldPassword);
        void GuardarRespuestasSeguridad(string username, Dictionary<int, string> respuestas);
        List<PreguntaSeguridadDto> GetPreguntasDeUsuario(string username);
        List<PreguntaSeguridadDto> GetPreguntasSeguridad();
        PoliticaSeguridadDto? GetPoliticaSeguridad();
    }
}
