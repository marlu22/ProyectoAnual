// src/BusinessLogic/Services/EmailService.cs
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using BusinessLogic.Configuration;
using Microsoft.Extensions.Options;

namespace BusinessLogic.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value ?? throw new ArgumentNullException(nameof(smtpSettings));
        }

        public async Task SendPasswordResetEmailAsync(string toEmail, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(toEmail))
            {
                // No lanzar excepción, solo registrar o ignorar si no hay correo.
                // En un sistema real, aquí habría un logging.
                Console.WriteLine("No se proporcionó una dirección de correo para la recuperación de contraseña.");
                return;
            }

            try
            {
                using var client = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port)
                {
                    Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                    EnableSsl = _smtpSettings.UseSsl
                };

                var from = new MailAddress(_smtpSettings.SenderEmail, _smtpSettings.SenderName);
                var to = new MailAddress(toEmail);

                using var mailMessage = new MailMessage(from, to)
                {
                    Subject = "Recuperación de Contraseña",
                    IsBodyHtml = true,
                    Body = GetEmailBody(newPassword)
                };

                await client.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // En una aplicación real, esto debería ser logueado a un sistema de logging.
                // Por ahora, lanzaremos una excepción para que el desarrollador vea el problema de configuración.
                throw new InvalidOperationException($"Error al enviar el correo. Verifique la configuración SMTP en appsettings.json. Detalles: {ex.Message}", ex);
            }
        }

        private static string GetEmailBody(string newPassword)
        {
            return $@"
                <div style='font-family: Arial, sans-serif; color: #333;'>
                    <h1 style='color: #0056b3;'>Recuperación de Contraseña</h1>
                    <p>Se ha solicitado un reinicio de su contraseña.</p>
                    <p>Su nueva contraseña temporal es:</p>
                    <h2 style='color: #darkorange; border: 1px solid #ddd; padding: 10px; display: inline-block;'>{newPassword}</h2>
                    <p>Por su seguridad, se le pedirá que cambie esta contraseña la próxima vez que inicie sesión.</p>
                    <hr>
                    <p style='font-size: 0.8em; color: #777;'>Si usted no solicitó este cambio, por favor ignore este correo electrónico.</p>
                </div>";
        }
    }
}
