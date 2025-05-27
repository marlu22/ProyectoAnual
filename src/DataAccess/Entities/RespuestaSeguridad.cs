using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class RespuestaSeguridad
    {
        public int IdUsuario { get; set; }
        public int IdPregunta { get; set; }
        public string Respuesta { get; set; }
    }
}