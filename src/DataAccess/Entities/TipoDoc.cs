using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class TipoDoc
    {
        [Key]
        public int IdTipoDoc { get; set; }
        [Required]
        [MaxLength(30)]
        public string Nombre { get; set; }
    }
}