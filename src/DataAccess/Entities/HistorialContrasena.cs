using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class HistorialContrasena
    {
        [Key]
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaCambio { get; set; }
        public byte[] ContrasenaScript { get; set; }
    }
}