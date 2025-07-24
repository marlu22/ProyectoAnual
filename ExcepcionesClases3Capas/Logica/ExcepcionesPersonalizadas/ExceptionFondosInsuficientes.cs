using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.ExepcionesPersonalizadas
{
    public class ExceptionFondosInsuficientes : Exception
    {
        public string? ParamName { get; }

        public ExceptionFondosInsuficientes() : base("Fondos insuficientes para realizar la operación.") { }

        public ExceptionFondosInsuficientes(string mensaje) : base(mensaje) { }

        public ExceptionFondosInsuficientes(string? mensaje, Exception? innerException) : base(mensaje) { }

        public ExceptionFondosInsuficientes(string? paramName,string? mensaje) : base(mensaje) { ParamName = paramName; }

        public ExceptionFondosInsuficientes(string? paramName, string? mensaje,  Exception? innerException) : base(mensaje) { ParamName = paramName; }
    }
}