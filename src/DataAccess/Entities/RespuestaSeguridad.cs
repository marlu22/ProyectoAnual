using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class RespuestaSeguridad
    {
        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }

        public int IdPregunta { get; set; }
        [ForeignKey("IdPregunta")]
        public PreguntaSeguridad PreguntaSeguridad { get; set; }
    }
}