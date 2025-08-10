using BusinessLogic.Configuration;
using BusinessLogic.Security;
using BusinessLogic.Services;
using DataAccess;
using DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace BusinessLogic
{
    public static class ServiceFactory
    {
        private static void SetupDependencies(out IConfiguration config, out ILoggerFactory loggerFactory, out IUserRepository userRepository)
        {
            config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddConsole();
            });

            var connectionFactory = new DatabaseConnectionFactory(config);
            var sqlLogger = loggerFactory.CreateLogger<SqlUserRepository>();
            userRepository = new SqlUserRepository(connectionFactory, sqlLogger);
        }

        public static IUserAuthenticationService CreateUserAuthenticationService()
        {
            SetupDependencies(out var config, out var loggerFactory, out var userRepository);

            var smtpSettings = new SmtpSettings();
            config.GetSection("SmtpSettings").Bind(smtpSettings);
            var emailService = new EmailService(Options.Create(smtpSettings));
            var passwordHasher = new PasswordHasher();
            var logger = loggerFactory.CreateLogger<UserAuthenticationService>();

            return new UserAuthenticationService(userRepository, emailService, logger, passwordHasher);
        }

        public static IUserManagementService CreateUserManagementService()
        {
            SetupDependencies(out var config, out var loggerFactory, out var userRepository);

            var smtpSettings = new SmtpSettings();
            config.GetSection("SmtpSettings").Bind(smtpSettings);
            var emailService = new EmailService(Options.Create(smtpSettings));
            var passwordHasher = new PasswordHasher();
            var logger = loggerFactory.CreateLogger<UserManagementService>();

            return new UserManagementService(userRepository, emailService, logger, passwordHasher);
        }

        public static IReferenceDataService CreateReferenceDataService()
        {
            SetupDependencies(out _, out var loggerFactory, out var userRepository);

            var logger = loggerFactory.CreateLogger<ReferenceDataService>();

            return new ReferenceDataService(userRepository, logger);
        }
    }
}
