using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    [Table("personas")]
    public class Persona
    {
        [Key]
        [Column("id_persona")]
        public int IdPersona { get; set; }

        [Required]
        [Column("legajo")]
        public int Legajo { get; set; }

        [Required]
        [Column("nombre")]
        [StringLength(30)]
        public string Nombre { get; set; } = null!;

        [Required]
        [Column("apellido")]
        [StringLength(30)]
        public string Apellido { get; set; } = null!;

        [NotMapped]
        public string NombreCompleto => $"{Nombre} {Apellido}";

        [Column("id_tipo_doc")]
        public int IdTipoDoc { get; set; }

        [Required]
        [Column("num_doc")]
        [StringLength(20)]
        public string NumDoc { get; set; } = null!;

        [Column("cuil")]
        [StringLength(15)]
        public string? Cuil { get; set; }

        [Column("calle")]
        [StringLength(50)]
        public string? Calle { get; set; }

        [Column("altura")]
        [StringLength(30)]
        public string? Altura { get; set; }

        [Column("id_localidad")]
        public int IdLocalidad { get; set; }

        [Column("id_genero")]
        public int IdGenero { get; set; }

        [Column("correo")]
        [StringLength(100)]
        public string? Correo { get; set; }

        [Column("fecha_ingreso")]
        public DateTime FechaIngreso { get; set; }

        [ForeignKey("IdTipoDoc")]
        public virtual TipoDoc? TipoDoc { get; set; }

        [ForeignKey("IdLocalidad")]
        public virtual Localidad? Localidad { get; set; }

        [ForeignKey("IdGenero")]
        public virtual Genero? Genero { get; set; }
    }
}