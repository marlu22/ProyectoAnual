// Assuming this is in your Program.cs or wherever the MainForm is instantiated
using System;
using System.Windows.Forms;
using BusinessLogic.Services;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using DataAccess;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        // Lee la cadena de conexión desde appsettings.json si lo prefieres
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer("Server=local;Database=login;Trusted_Connection=True;");

        var context = new ApplicationDbContext(optionsBuilder.Options);
        IUserRepository userRepository = new UserRepository(context);
        IUserService userService = new UserService(userRepository);

        Application.Run(new MainForm(userService));
    }
}
