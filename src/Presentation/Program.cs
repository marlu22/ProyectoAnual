// Assuming this is in your Program.cs or wherever the MainForm is instantiated
using System;
using System.Windows.Forms;
using BusinessLogic.Services;
using DataAccess.Repositories; // Add this namespace if needed

internal static class Program
{
    [STAThread]
    static void Main()
    {
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        // Create an instance of IUserRepository and pass it to UserService
        IUserRepository userRepository = new UserRepository(); // Replace with your actual implementation
        IUserService userService = new UserService(userRepository);
        Application.Run(new MainForm(userService));
    }
}
