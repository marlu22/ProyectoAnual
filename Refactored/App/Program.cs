using Core;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();

            var clienteService = serviceProvider.GetService<ClienteService>();

            Console.WriteLine("Bienvenido al Sistema Bancario");
            Console.WriteLine("---------------------------------");

            while (true)
            {
                Console.Write("Ingrese el número de cuenta (o 'salir' para terminar): ");
                var nroCuentaInput = Console.ReadLine();

                if (nroCuentaInput?.ToLower() == "salir")
                {
                    break;
                }

                if (!int.TryParse(nroCuentaInput, out int nroCuenta))
                {
                    Console.WriteLine("Número de cuenta inválido. Intente de nuevo.");
                    continue;
                }

                var cliente = clienteService.BuscarCliente(nroCuenta);

                if (cliente == null)
                {
                    Console.WriteLine("Cliente no encontrado.");
                    continue;
                }

                Console.WriteLine($"Cliente encontrado: {cliente.Nombre}");
                Console.WriteLine($"Tipo de Cuenta: {cliente.TipoCuenta}");
                Console.WriteLine($"Saldo Actual: {cliente.Saldo:C}");

                Console.Write("Ingrese el monto a retirar (o presione Enter para buscar otra cuenta): ");
                var montoInput = Console.ReadLine();

                if (string.IsNullOrEmpty(montoInput))
                {
                    continue;
                }

                if (!decimal.TryParse(montoInput, out decimal monto))
                {
                    Console.WriteLine("Monto inválido.");
                    continue;
                }

                try
                {
                    clienteService.Retirar(nroCuenta, monto);
                    Console.WriteLine("Retiro exitoso.");
                    var clienteActualizado = clienteService.BuscarCliente(nroCuenta);
                    Console.WriteLine($"Nuevo Saldo: {clienteActualizado.Saldo:C}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                Console.WriteLine("---------------------------------");
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IClienteRepository, InMemoryClienteRepository>();
            services.AddTransient<ClienteService>();
        }
    }
}
