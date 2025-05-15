using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Rol
    {
        [Key]
        public int IdRol { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }
    }
}