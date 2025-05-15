using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Provincia
    {
        [Key]
        public int IdProvincia { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }
    }
}