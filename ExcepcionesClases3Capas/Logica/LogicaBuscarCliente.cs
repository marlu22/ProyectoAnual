using Logica.ExepcionesPersonalizadas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica.ExepcionesPersonalizadas;
using ModeloDatos;

namespace Logica
{
    public class LogicaBuscarCliente
    {
        private string nombre;
        private string tipoCuenta;
        private int dni;
        private double importe;
        private double saldo;
        private int nroCuenta;

        public string NroCuenta
        {
            set
            {
                if (!int.TryParse(value, out int nroCuenta))
                    throw new ExceptionConversion(nameof(nroCuenta), "El número de cuenta debe ser un número entero.");
                if (value.Length > 4)
                    throw new ArgumentException("El número de cuenta es demasiado largo.", nameof(nroCuenta));
                nroCuenta = Convert.ToInt32(value);
                BuscarCuenta(nroCuenta);
            }
        }

        public string TipoCuenta
        {
            get { return tipoCuenta; }
        }

        public string Nombre
        {
            get { return nombre; }
        }

        public string DNI
        {
            get { return dni.ToString(); }
        }

        public string Saldo
        {
            get { return saldo.ToString("N2"); }
        }

        public void BuscarCuenta(int nro)
        {
            BuscarCliente Buscar = new BuscarCliente();
            var cliente = Buscar.Buscar(nro);
            nroCuenta = cliente.NroCuenta;
            tipoCuenta = cliente.TipoCuenta;
            saldo = cliente.Saldo;
            dni = cliente.DNI;
            nombre = cliente.Nombre;
        }

    }
}
