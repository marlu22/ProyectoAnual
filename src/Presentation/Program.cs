using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BusinessLogic;
using BusinessLogic.Exceptions;
using Presentation.Exceptions;

namespace Presentation
{
    internal static class Program
    {
        public static IServiceProvider? ServiceProvider { get; private set; }

        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            try
            {
                var host = CreateHostBuilder().Build();
                ServiceProvider = host.Services;

                Application.Run(ServiceProvider.GetRequiredService<LoginForm>());
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // Register services from BusinessLogic layer
                    services.AddInfrastructure(context.Configuration);

                    // Register Forms
                    services.AddTransient<LoginForm>();
                    services.AddTransient<AdminForm>();
                    services.AddTransient<UserForm>();
                    services.AddTransient<ProfileForm>();
                    services.AddTransient<CambioContrasenaForm>();
                    services.AddTransient<RecuperarContrasenaForm>();
                    services.AddTransient<PreguntasSeguridadForm>();
                    services.AddTransient<TwoFactorAuthForm>();
                    services.AddTransient<frmError>();
                    services.AddTransient<frmNotification>();
                });

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            HandleException(e.Exception);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception);
        }

        private static void HandleException(Exception? ex)
        {
            if (ex == null) return;

            LogException(ex);

            try
            {
                // Try to resolve the error form from DI container
                var errorForm = ServiceProvider?.GetService<frmError>();
                if (errorForm != null)
                {
                    errorForm.SetError(ex);
                    errorForm.ShowDialog();
                }
                else
                {
                    // Fallback if DI fails
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                // Fallback if even the error form fails
                MessageBox.Show($"A critical error occurred and the application cannot continue: {ex.Message}", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }


            if (!(ex is ValidationException) && !(ex is BusinessLogicException) && !(ex is UILayerException))
            {
                Environment.Exit(1);
            }
        }

        private static void LogException(Exception ex)
        {
            try
            {
                string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "error_log.txt");
                string errorMessage = $"[{DateTime.Now}] - {ex.GetType().FullName}:{Environment.NewLine}" +
                                      $"Message: {ex.Message}{Environment.NewLine}" +
                                      $"StackTrace:{Environment.NewLine}{ex.StackTrace}{Environment.NewLine}" +
                                      "--------------------------------------------------" +
                                      $"{Environment.NewLine}";
                File.AppendAllText(logPath, errorMessage);
            }
            catch
            {
                // If logging fails, there's not much else to do.
            }
        }
    }
}
