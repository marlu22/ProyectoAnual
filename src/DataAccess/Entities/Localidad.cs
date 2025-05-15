using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class Localidad
    {
        [Key]
        public int IdLocalidad { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }
        [Required]
        public int IdPartido { get; set; }
        [ForeignKey("IdPartido")]
        public Partido Partido { get; set; }
    }
}