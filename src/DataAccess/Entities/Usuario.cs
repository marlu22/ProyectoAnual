using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required]
        [MaxLength(30)]
        public string UsuarioNombre { get; set; }

        [Required]
        public byte[] ContrasenaScript { get; set; }

        [Required]
        public int IdPersona { get; set; }

        public DateTime? FechaBloqueo { get; set; }

        [MaxLength(30)]
        public string? NombreUsuarioBloqueo { get; set; }

        public DateTime FechaUltimoCambio { get; set; }

        [Required]
        public int IdRol { get; set; }

        [ForeignKey("IdRol")]
        public Rol Rol { get; set; }

        public bool CambioContrasenaObligatorio { get; set; } = false;

        // Relaciones de navegación (opcional, si tienes las entidades Persona y Rol)
        // public Persona Persona { get; set; }
        // public Rol Rol { get; set; }
    }
}