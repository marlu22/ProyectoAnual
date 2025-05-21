using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Permiso
    {
        [Key]
        public int IdPermiso { get; set; }

        [MaxLength(50)]
        public string Nombre { get; set; } // <-- Este campo representa 'permiso' en la tabla

        [MaxLength(200)]
        public string Descripcion { get; set; }
    }
}