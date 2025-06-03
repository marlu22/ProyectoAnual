// Assuming this is in your Program.cs or wherever the MainForm is instantiated
using System;
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
}
