using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloDatos
{
    public static class ListaSaldosClientes
    {
        public static List<SaldosClientes> LSaldosClientes = new List<SaldosClientes>();

        public static void InicializarLista()
        {

            LSaldosClientes.AddRange(new List<SaldosClientes>
            {
                new SaldosClientes { NroCuenta = 1001, TipoCuenta = "Caja de Ahorro", Saldo = 12500.75, DNI = 30111222, Nombre = "Juan Pérez" },
                new SaldosClientes { NroCuenta = 2002, TipoCuenta = "Cuenta Corriente", Saldo = 5300.00, DNI = 30222333, Nombre = "María López" },
                new SaldosClientes { NroCuenta = 1003, TipoCuenta = "Caja de Ahorro", Saldo = 8500.30, DNI = 30333444, Nombre = "Carlos Gómez" },
                new SaldosClientes { NroCuenta = 2004, TipoCuenta = "Cuenta Corriente", Saldo = 450.50, DNI = 30444555, Nombre = "Ana Martínez" },
                new SaldosClientes { NroCuenta = 1005, TipoCuenta = "Caja de Ahorro", Saldo = 15000.00, DNI = 30555666, Nombre = "Luis Fernández" },
                new SaldosClientes { NroCuenta = 1006, TipoCuenta = "Caja de Ahorro", Saldo = 9200.20, DNI = 30666777, Nombre = "Laura García" },
                new SaldosClientes { NroCuenta = 2007, TipoCuenta = "Cuenta Corriente", Saldo = 300.00, DNI = 30777888, Nombre = "Pedro Sánchez" },
                new SaldosClientes { NroCuenta = 1008, TipoCuenta = "Caja de Ahorro", Saldo = 7200.10, DNI = 30888999, Nombre = "Sofía Torres" },
                new SaldosClientes { NroCuenta = 2009, TipoCuenta = "Cuenta Corriente", Saldo = 12000.90, DNI = 30999000, Nombre = "Diego Ramírez" },
                new SaldosClientes { NroCuenta = 1010, TipoCuenta = "Caja de Ahorro", Saldo = 5600.00, DNI = 31000111, Nombre = "Valeria Castro" }
            });
        }

    }
}
