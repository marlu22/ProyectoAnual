namespace Core
{
    public class ClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public Cliente? BuscarCliente(int nroCuenta)
        {
            return _clienteRepository.GetByNroCuenta(nroCuenta);
        }

        public void Retirar(int nroCuenta, decimal monto)
        {
            var cliente = _clienteRepository.GetByNroCuenta(nroCuenta);

            if (cliente == null)
            {
                throw new InvalidOperationException("La cuenta no existe.");
            }

            // The withdrawal logic is now inside the Cliente entity
            cliente.Retirar(monto);

            _clienteRepository.Update(cliente);
        }
    }
}
