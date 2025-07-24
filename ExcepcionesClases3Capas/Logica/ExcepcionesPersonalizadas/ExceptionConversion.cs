using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.ExepcionesPersonalizadas
{
    public class ExceptionConversion : Exception
    {
        public string? ParamName { get; }

        public ExceptionConversion() : base("Error en la conversion de datos.") { }

        public ExceptionConversion(string mensaje) : base(mensaje) { }  

        public ExceptionConversion(string? mensaje, Exception? innerException) : base(mensaje) { }

        public ExceptionConversion(string? paramName, string? mensaje) : base(mensaje) { ParamName = paramName;}

        public ExceptionConversion(string? paramName, string? mensaje,  Exception? innerException) : base(mensaje) { ParamName = paramName;}
    }
}
