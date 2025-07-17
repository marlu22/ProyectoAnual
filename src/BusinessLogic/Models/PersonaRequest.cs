// src/BusinessLogic/Models/PersonaRequest.cs
namespace BusinessLogic.Models
{
    public class PersonaRequest
    {
        public int Legajo { get; set; } // Confirmed as int
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string? TipoDoc { get; set; }
        public string NumDoc { get; set; } = null!;
        public string Cuil { get; set; } = null!;
        public string Calle { get; set; } = null!;
        public string Altura { get; set; } = null!;
        public string? Localidad { get; set; }
        public string? Genero { get; set; }
        public string Correo { get; set; } = null!;
    }
}