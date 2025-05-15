using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class PreguntaSeguridad
    {
        [Key]
        public int IdPregunta { get; set; }
        [Required]
        [MaxLength(255)]
        public string Pregunta { get; set; }
    }
}