using System;
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

        [Column("fecha_nacimiento")]
        public DateTime? FechaNacimiento { get; set; }

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

        [Column("celular")]
        [StringLength(30)]
        public string? Celular { get; set; }

        [Column("fecha_ingreso")]
        public DateTime FechaIngreso { get; set; }

        [ForeignKey("IdTipoDoc")]
        public virtual TipoDoc? TipoDoc { get; set; }

        [ForeignKey("IdLocalidad")]
        public virtual Localidad? Localidad { get; set; }

        [ForeignKey("IdGenero")]
        public virtual Genero? Genero { get; set; }

        public void Update(int legajo, string nombre, string apellido, int idTipoDoc, string numDoc, DateTime? fechaNacimiento, string? cuil, string? calle, string? altura, int idLocalidad, int idGenero, string? correo, string? celular, DateTime fechaIngreso)
        {
            Legajo = legajo;
            Nombre = nombre;
            Apellido = apellido;
            IdTipoDoc = idTipoDoc;
            NumDoc = numDoc;
            FechaNacimiento = fechaNacimiento;
            Cuil = cuil;
            Calle = calle;
            Altura = altura;
            IdLocalidad = idLocalidad;
            IdGenero = idGenero;
            Correo = correo;
            Celular = celular;
            FechaIngreso = fechaIngreso;
        }
    }
}