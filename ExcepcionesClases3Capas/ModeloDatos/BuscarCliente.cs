using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloDatos
{
    public class BuscarCliente
    {
        public ClienteDTO Buscar(int cuenta)
        {
            var cliente = ListaSaldosClientes.LSaldosClientes
                .FirstOrDefault(c => c.NroCuenta == cuenta);

            if (cliente == null)
                throw new FileNotFoundException("Número de cuenta inexistente.");


            return new ClienteDTO
            {
                NroCuenta = cliente.NroCuenta,
                TipoCuenta = cliente.TipoCuenta,
                Saldo = cliente.Saldo,
                DNI = cliente.DNI,
                Nombre = cliente.Nombre
            };
        }
    }
}
