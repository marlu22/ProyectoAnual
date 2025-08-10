// Assuming this is in your Program.cs or wherever the MainForm is instantiated
using System;
using System.IO;
using System.Windows.Forms;
using BusinessLogic;
using BusinessLogic.Services;
using Presentation; // Required for LoginForm, frmError, etc.
using UserManagementSystem.BusinessLogic.Exceptions;
using UserManagementSystem.DataAccess.Exceptions;
using UserManagementSystem.Presentation.Exceptions;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        // Global Exception Handling
        Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
        AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

        try
        {
            // La composición de dependencias ahora se realiza en la capa de lógica de negocio
            var authService = ServiceFactory.CreateUserAuthenticationService();
            var managementService = ServiceFactory.CreateUserManagementService();
            var referenceService = ServiceFactory.CreateReferenceDataService();

            Application.Run(new LoginForm(authService, managementService, referenceService));
        }
        catch (Exception ex)
        {
            // Captura cualquier excepción durante la inicialización
            HandleException(ex);
        }
    }

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

        // Log the exception
        LogException(ex);

        // Show a friendly message to the user
        using (var errorForm = new frmError(ex))
        {
            errorForm.ShowDialog();
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
