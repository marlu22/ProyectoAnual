using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class HistorialContrasena
    {
        public int IdHistorial { get; set; } // <--- Cambia de Id a IdHistorial
        public int IdUsuario { get; set; }
        public byte[] ContrasenaScript { get; set; }
    }
}