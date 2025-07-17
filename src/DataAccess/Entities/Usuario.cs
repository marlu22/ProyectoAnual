using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string UsuarioNombre { get; set; } = null!;
        public byte[] ContrasenaScript { get; set; } = null!;
        public int IdPersona { get; set; }
        public DateTime FechaBloqueo { get; set; }
        public string? NombreUsuarioBloqueo { get; set; }
        public DateTime FechaUltimoCambio { get; set; }
        public int IdRol { get; set; }
        public int? IdPolitica { get; set; }
        public bool CambioContrasenaObligatorio { get; set; }
        public Persona Persona { get; set; } = null!;
        public Rol Rol { get; set; } = null!;
        public PoliticaSeguridad? PoliticaSeguridad { get; set; }
    }
}