using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Cliente
    {
        public int NroCuenta { get; private set; }
        public string Nombre { get; private set; }
        public string TipoCuenta { get; private set; }
        public int DNI { get; private set; }
        public decimal Saldo { get; private set; }

        public Cliente(int nroCuenta, string nombre, string tipoCuenta, int dni, decimal saldo)
        {
            NroCuenta = nroCuenta;
            Nombre = nombre;
            TipoCuenta = tipoCuenta;
            DNI = dni;
            Saldo = saldo;
        }

        public void Retirar(decimal monto)
        {
            if (monto <= 0)
            {
                throw new ArgumentException("El monto a retirar debe ser positivo.", nameof(monto));
            }

            if (monto > Saldo)
            {
                throw new InvalidOperationException("Fondos insuficientes.");
            }

            Saldo -= monto;
        }
    }
}
