// src/BusinessLogic/Models/PersonaRequest.cs
namespace BusinessLogic.Models
{
    public class PersonaRequest
    {
        public string Legajo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string TipoDoc { get; set; } = null!;
        public string NumDoc { get; set; } = null!;
        public string Cuil { get; set; } = null!;
        public string Calle { get; set; } = null!;
        public string Altura { get; set; } = null!;
        public string Localidad { get; set; } = null!;
        public string Genero { get; set; } = null!;
        public string Correo { get; set; } = null!;
    }
}