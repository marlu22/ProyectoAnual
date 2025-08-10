using BusinessLogic.Configuration;
using BusinessLogic.Security;
using BusinessLogic.Services;
using DataAccess;
using DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BusinessLogic
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register configuration
            services.AddSingleton(configuration);

            // Configure SmtpSettings using the options pattern
            services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));

            // Register logging
            services.AddSingleton<ILoggerFactory>(serviceProvider =>
            {
                return LoggerFactory.Create(builder =>
                {
                    builder
                        .AddFilter("Microsoft", LogLevel.Warning)
                        .AddFilter("System", LogLevel.Warning)
                        .AddConsole();
                });
            });

            services.AddLogging();

            // Register DataAccess components
            services.AddSingleton<DatabaseConnectionFactory>();
            services.AddScoped<IUserRepository, SqlUserRepository>();

            // Register BusinessLogic services
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();

            services.AddTransient<IUserAuthenticationService, UserAuthenticationService>();
            services.AddTransient<IUserManagementService, UserManagementService>();
            services.AddTransient<IReferenceDataService, ReferenceDataService>();

            return services;
        }
    }
}
