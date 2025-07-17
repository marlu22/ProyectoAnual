using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Rol
    {
        public int IdRol { get; set; }
        public string Nombre { get; set; } = null!;
    }
}