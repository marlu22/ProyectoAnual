using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class PermisoUsuario
    {
        public int IdUsuario { get; set; }
        public int IdPermiso { get; set; }
        public DateTime FechaVencimiento { get; set; }
    }
}