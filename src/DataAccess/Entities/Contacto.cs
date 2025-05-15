using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class Contacto
    {
        [Key]
        public int IdContacto { get; set; }
        [Required]
        public int IdPersona { get; set; }
        [ForeignKey("IdPersona")]
        public Persona Persona { get; set; }
    }
}