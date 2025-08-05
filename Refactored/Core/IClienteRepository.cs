namespace Core
{
    public interface IClienteRepository
    {
        Cliente? GetByNroCuenta(int nroCuenta);
        void Update(Cliente cliente);
    }
}
