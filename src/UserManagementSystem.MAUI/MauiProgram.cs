using Microsoft.AspNetCore.Components.WebView.Maui;
using UserManagementSystem.MAUI.Data;
using BusinessLogic.Services;
using DataAccess.Repositories;
using DataAccess;
using BusinessLogic.Configuration;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;

namespace UserManagementSystem.MAUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = "UserManagementSystem.MAUI.appsettings.json";

        using (Stream stream = assembly.GetManifestResourceStream(resourceName))
        {
            if (stream != null)
            {
                var config = new ConfigurationBuilder()
                    .AddJsonStream(stream)
                    .Build();
                builder.Configuration.AddConfiguration(config);
            }
        }

		builder.Services.AddMauiBlazorWebView();
#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif

		builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
		builder.Services.AddSingleton<IDbConnectionFactory>(new DatabaseConnectionFactory(builder.Configuration.GetConnectionString("DefaultConnection")));
		builder.Services.AddTransient<IUserRepository, SqlUserRepository>();
		builder.Services.AddTransient<IEmailService, EmailService>();
		builder.Services.AddTransient<IUserService, UserService>();


		builder.Services.AddSingleton<WeatherForecastService>();

		return builder.Build();
	}
}
