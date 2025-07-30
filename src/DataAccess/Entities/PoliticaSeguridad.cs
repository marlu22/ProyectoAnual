using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    [Table("politicas_seguridad")]
    public class PoliticaSeguridad
    {
        [Key]
        [Column("id_politica")]
        public int IdPolitica { get; set; }

        [Column("mayus_y_minus")]
        public bool MayusYMinus { get; set; }

        [Column("letras_y_numeros")]
        public bool LetrasYNumeros { get; set; }

        [Column("caracter_especial")]
        public bool CaracterEspecial { get; set; }

        [Column("autenticacion_2fa")]
        public bool Autenticacion2FA { get; set; }

        [Column("no_repetir_anteriores")]
        public bool NoRepetirAnteriores { get; set; }

        [Column("sin_datos_personales")]
        public bool SinDatosPersonales { get; set; }

        [Column("min_caracteres")]
        public int MinCaracteres { get; set; }

        [Column("cant_preguntas")]
        public int CantPreguntas { get; set; }
    }
}