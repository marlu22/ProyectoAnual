var builder = WebApplication.CreateBuilder(args);

// Configurar servicios aquí si querés (más adelante)

// Configurar el pipeline HTTP
var app = builder.Build();

app.MapGet("/", () => "¡API funcionando correctamente!");

app.Run();
