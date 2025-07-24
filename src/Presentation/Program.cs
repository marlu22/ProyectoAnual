// Assuming this is in your Program.cs or wherever the MainForm is instantiated
using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using DataAccess.Repositories;
using BusinessLogic.Services;
using Presentation; // Add this if LoginForm is in Presentation.Forms namespace

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

        // Configuración y DI
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var connectionString = config.GetConnectionString("DefaultConnection");
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        var context = new ApplicationDbContext(optionsBuilder.Options);
        IUserRepository userRepository = new UserRepository(context);

        IUserService userService = new UserService(userRepository);

        Application.Run(new LoginForm(userService));
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
        MessageBox.Show(
            "Ocurrió un error inesperado. La aplicación se cerrará. Por favor, contacte al soporte técnico.",
            "Error Inesperado",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
        );

        // Optionally, exit the application
        Environment.Exit(1);
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
