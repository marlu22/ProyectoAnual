using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// src/DataAccess/Entities/Contacto.cs
// src/DataAccess/Entities/Contacto.cs
namespace DataAccess.Entities
{
    public class Contacto
    {
        public int IdPersona { get; set; }
        public string TipoContacto { get; set; } = null!;
        public string Valor { get; set; } = null!;
        public Persona? Persona { get; set; }
    }
}