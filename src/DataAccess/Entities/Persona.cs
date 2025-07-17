// src/DataAccess/Entities/Persona.cs
namespace DataAccess.Entities
{
    public class Persona
    {
        public int IdPersona { get; set; }
        public string Legajo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string NombreCompleto => $"{Nombre} {Apellido}"; // Added for AdminForm.cs
        public int IdTipoDoc { get; set; }
        public string NumDoc { get; set; } = null!;
        public string Cuil { get; set; } = null!;
        public string Calle { get; set; } = null!;
        public string Altura { get; set; } = null!;
        public int IdLocalidad { get; set; }
        public int IdGenero { get; set; }
        public string Correo { get; set; } = null!;
        public DateTime FechaIngreso { get; set; }
        public TipoDoc? TipoDoc { get; set; }
        public Localidad? Localidad { get; set; }
        public Genero? Genero { get; set; }
    }
}