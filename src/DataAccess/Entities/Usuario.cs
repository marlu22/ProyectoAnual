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

        [Column("Codigo2FA")]
        [StringLength(10)]
        public string? Codigo2FA { get; set; }

        [Column("Codigo2FAExpiracion")]
        public DateTime? Codigo2FAExpiracion { get; set; }

        [Column("FechaExpiracion")]
        public DateTime? FechaExpiracion { get; set; }

        [ForeignKey("IdPersona")]
        public virtual Persona Persona { get; set; } = null!;

        [ForeignKey("IdRol")]
        public virtual Rol Rol { get; set; } = null!;

        [ForeignKey("IdPolitica")]
        public virtual PoliticaSeguridad? PoliticaSeguridad { get; set; }

        public void Deshabilitar(string nombreUsuarioBloqueo)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuarioBloqueo))
            {
                throw new ArgumentException("El nombre del usuario que bloquea no puede ser nulo o vacío.", nameof(nombreUsuarioBloqueo));
            }
            FechaBloqueo = DateTime.Now;
            NombreUsuarioBloqueo = nombreUsuarioBloqueo;
        }

        public void Habilitar()
        {
            FechaBloqueo = new DateTime(9999, 12, 31);
            NombreUsuarioBloqueo = null;
        }

        public void Update(string username, int idRol, DateTime? fechaExpiracion, bool cambioContrasenaObligatorio, bool habilitado, string adminUsername)
        {
            UsuarioNombre = username;
            IdRol = idRol;
            FechaExpiracion = fechaExpiracion;
            CambioContrasenaObligatorio = cambioContrasenaObligatorio;

            if (habilitado)
            {
                Habilitar();
            }
            else
            {
                Deshabilitar(adminUsername);
            }
        }
    }
}