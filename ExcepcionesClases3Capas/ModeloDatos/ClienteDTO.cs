using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloDatos
{
    public class ClienteDTO
    {
        public int NroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public double Saldo { get; set; }
        public int DNI { get; set; }
        public string Nombre { get; set; }
    }
}
