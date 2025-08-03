// src/BusinessLogic/Services/IEmailService.cs
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public interface IEmailService
    {
        Task SendPasswordResetEmailAsync(string toEmail, string newPassword);
    }
}
