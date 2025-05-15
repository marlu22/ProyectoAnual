using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Genero
    {
        [Key]
        public int IdGenero { get; set; }
        [Required]
        [MaxLength(25)]
        public string Nombre { get; set; }
    }
}