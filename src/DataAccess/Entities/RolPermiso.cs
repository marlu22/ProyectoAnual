using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class RolPermiso
    {
        public int IdRol { get; set; }
        [ForeignKey("IdRol")]
        public Rol Rol { get; set; }

        public int IdPermiso { get; set; }
        [ForeignKey("IdPermiso")]
        public Permiso Permiso { get; set; }
    }
}