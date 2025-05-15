using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class PermisoUsuario
    {
        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }

        public int IdPermiso { get; set; }
        [ForeignKey("IdPermiso")]
        public Permiso Permiso { get; set; }
    }
}