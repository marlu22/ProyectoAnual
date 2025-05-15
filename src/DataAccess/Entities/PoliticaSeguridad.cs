using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class PoliticaSeguridad
    {
        [Key]
        public int IdPolitica { get; set; }
        public bool SinDatosPersonales { get; set; }
    }
}