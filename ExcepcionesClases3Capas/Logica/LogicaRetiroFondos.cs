using Logica.ExepcionesPersonalizadas;
using ModeloDatos;

namespace Logica
{
    public class LogicaRetiroFondos
    {
        private double importe = 0;
        private double saldo;
        private int nroCuenta = 0;

        public string NroCuenta
        {
            set
            {
                if (!int.TryParse(value, out int nroCuenta))
                    throw new ExceptionConversion(nameof(nroCuenta), "El número de cuenta debe ser un número entero.");
                if (value.Length > 4)
                    throw new ArgumentException("El número de cuenta es demasiado largo.", nameof(nroCuenta));
                this.nroCuenta = nroCuenta;
                LogicaBuscarCliente Buscar = new LogicaBuscarCliente();
                Buscar.BuscarCuenta(nroCuenta);
                this.saldo = Convert.ToDouble(Buscar.Saldo);
            }
        }

        public string Importe
        {
            set
            {
                if (!double.TryParse(value, out double valor))
                    throw new ExceptionConversion(nameof(importe), "El importe debe ser un número válido.");
                if ((valor % 100 != 0) || (valor < 100))
                    throw new ArgumentOutOfRangeException(nameof(importe), "El importe debe ser múltiplo de 100.");
                if (valor > saldo)
                    throw new ExceptionFondosInsuficientes(nameof(importe), "El importe no puede superar el saldo disponible.");
                importe = valor;
            }
        }

        public void Retirar()
        {
            if (nroCuenta == 0)
                throw new ArgumentException("No ingreso el número de cuenta de la cual retirar.");
            if (importe == 0)
                throw new ArgumentException("No ingreso el importe a retirar.");

            DescontarSaldoCliente.DescontarSaldo(nroCuenta, importe);
        }
    }
}
