using BusinessLogic.Configuration;
using BusinessLogic.Services;
using DataAccess;
using DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;

namespace BusinessLogic
{
    public static class ServiceFactory
    {
        public static IUserService CreateUserService()
        {
            // 1. Set up configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // 2. Set up logging
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddConsole();
            });

            // 3. Create DataAccess components
            var connectionFactory = new DatabaseConnectionFactory(config);
            ILogger<SqlUserRepository> sqlLogger = loggerFactory.CreateLogger<SqlUserRepository>();
            IUserRepository userRepository = new SqlUserRepository(connectionFactory, sqlLogger);

            // 4. Create BusinessLogic components
            var smtpSettings = new SmtpSettings();
            config.GetSection("SmtpSettings").Bind(smtpSettings);
            IEmailService emailService = new EmailService(Options.Create(smtpSettings));

            ILogger<UserService> userLogger = loggerFactory.CreateLogger<UserService>();
            IUserService userService = new UserService(userRepository, emailService, userLogger);

            return userService;
        }
    }
}
