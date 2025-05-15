using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class Partido
    {
        [Key]
        public int IdPartido { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }
        [Required]
        public int IdProvincia { get; set; }
        [ForeignKey("IdProvincia")]
        public Provincia Provincia { get; set; }
    }
}