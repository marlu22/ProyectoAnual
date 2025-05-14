using BusinessLogic.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

// Registrar servicios de negocio
builder.Services.AddScoped<IUserService, UserService>();


// Agregar controladores
builder.Services.AddControllers();

// Construir la aplicación
var app = builder.Build();

// Configurar middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
