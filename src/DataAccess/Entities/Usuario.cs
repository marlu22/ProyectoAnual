using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    [Table("usuarios")]
    public class Usuario
    {
        [Key]
        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [Required]
        [Column("usuario")]
        [StringLength(30)]
        public string UsuarioNombre { get; set; } = null!;

        [Required]
        [Column("contrasena_script")]
        [MaxLength(512)]
        public byte[] ContrasenaScript { get; set; } = null!;

        [Column("id_persona")]
        public int IdPersona { get; set; }

        [Column("fecha_bloqueo")]
        public DateTime FechaBloqueo { get; set; }

        [Column("nombre_usuario_bloqueo")]
        [StringLength(30)]
        public string? NombreUsuarioBloqueo { get; set; }

        [Column("fecha_ultimo_cambio")]
        public DateTime FechaUltimoCambio { get; set; }

        [Column("id_rol")]
        public int IdRol { get; set; }

        [Column("id_politica")]
        public int? IdPolitica { get; set; }

        [Column("CambioContrasenaObligatorio")]
        public bool CambioContrasenaObligatorio { get; set; }

        [ForeignKey("IdPersona")]
        public virtual Persona Persona { get; set; } = null!;

        [ForeignKey("IdRol")]
        public virtual Rol Rol { get; set; } = null!;

        [ForeignKey("IdPolitica")]
        public virtual PoliticaSeguridad? PoliticaSeguridad { get; set; }
    }
}