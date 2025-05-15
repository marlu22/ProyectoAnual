using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class Persona
    {
        [Key]
        public int IdPersona { get; set; }
        [Required]
        public int Legajo { get; set; }
        [Required]
        [MaxLength(30)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(30)]
        public string Apellido { get; set; }
        [Required]
        public int IdTipoDoc { get; set; }
        [ForeignKey("IdTipoDoc")]
        public TipoDoc TipoDoc { get; set; }
        [Required]
        [MaxLength(20)]
        public string NumDoc { get; set; }
        [MaxLength(10)]
        public string Cuil { get; set; }
        [MaxLength(50)]
        public string Calle { get; set; }
        [MaxLength(30)]
        public string Altura { get; set; }
        [Required]
        public int IdLocalidad { get; set; }
        [ForeignKey("IdLocalidad")]
        public Localidad Localidad { get; set; }
        [Required]
        public int IdGenero { get; set; }
        [ForeignKey("IdGenero")]
        public Genero Genero { get; set; }
        [MaxLength(100)]
        public string Correo { get; set; }
        public DateTime FechaIngreso { get; set; }
    }
}