using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class PoliticaSeguridad
    {
        [Key]
        public int IdPolitica { get; set; }
        public bool SinDatosPersonales { get; set; }
        public int MinCaracteres { get; set; }
        public int CantPreguntas { get; set; }
        // Agrega aquí los demás campos de la tabla si los hay
    }
}