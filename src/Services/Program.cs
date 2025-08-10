using DataAccess;
using DataAccess.Repositories;
using BusinessLogic.Services;
using BusinessLogic.Security;
using BusinessLogic.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Services.Middleware;
using Session;

var builder = WebApplication.CreateBuilder(args);

// Configurar autenticación JWT
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "ClaveSuperSecreta"))
        };
    });

builder.Services.AddSingleton<DatabaseConnectionFactory>();
builder.Services.AddScoped<IUserRepository, SqlUserRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
builder.Services.AddScoped<IUserManagementService, UserManagementService>();
builder.Services.AddScoped<IReferenceDataService, ReferenceDataService>();


builder.Services.AddControllers();

var app = builder.Build();

// Configurar middleware
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
