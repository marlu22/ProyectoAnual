using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloDatos
{
    public static class DescontarSaldoCliente
    {
        public static void DescontarSaldo(int cuenta, double importe)
        {
            // Buscar la cuenta
            var cliente = ListaSaldosClientes.LSaldosClientes
                .FirstOrDefault(c => c.NroCuenta == cuenta);
            // Realizar el descuento
            cliente.Saldo -= importe;
        }
    }
    
}
