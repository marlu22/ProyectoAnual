using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Permiso
    {
        [Key]
        public int IdPermiso { get; set; }
        [MaxLength(200)]
        public string Descripcion { get; set; }
    }
}