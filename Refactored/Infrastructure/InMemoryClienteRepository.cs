using Core;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure
{
    public class InMemoryClienteRepository : IClienteRepository
    {
        private static readonly List<Cliente> _clientes;

        static InMemoryClienteRepository()
        {
            _clientes = new List<Cliente>
            {
                new Cliente(1001, "Juan Pérez", "Caja de Ahorro", 30111222, 12500.75m),
                new Cliente(2002, "María López", "Cuenta Corriente", 30222333, 5300.00m),
                new Cliente(1003, "Carlos Gómez", "Caja de Ahorro", 30333444, 8500.30m),
                new Cliente(2004, "Ana Martínez", "Cuenta Corriente", 30444555, 450.50m),
                new Cliente(1005, "Luis Fernández", "Caja de Ahorro", 30555666, 15000.00m),
                new Cliente(1006, "Laura García", "Caja de Ahorro", 30666777, 9200.20m),
                new Cliente(2007, "Pedro Sánchez", "Cuenta Corriente", 30777888, 300.00m),
                new Cliente(1008, "Sofía Torres", "Caja de Ahorro", 30888999, 7200.10m),
                new Cliente(2009, "Diego Ramírez", "Cuenta Corriente", 30999000, 12000.90m),
                new Cliente(1010, "Valeria Castro", "Caja de Ahorro", 31000111, 5600.00m)
            };
        }

        public Cliente? GetByNroCuenta(int nroCuenta)
        {
            return _clientes.FirstOrDefault(c => c.NroCuenta == nroCuenta);
        }

        public void Update(Cliente cliente)
        {
            var existingCliente = GetByNroCuenta(cliente.NroCuenta);
            if (existingCliente != null)
            {
                // In a real DB, this would be an update operation.
                // For an in-memory list, we can remove the old and add the new.
                _clientes.Remove(existingCliente);
                _clientes.Add(cliente);
            }
        }
    }
}
