using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public interface IPasswordService
    {
        Task RecuperarContrasena(string username, Dictionary<int, string> respuestas);
        void CambiarContrasena(string username, string newPassword, string oldPassword);
    }
}
