using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public interface IUserRepository
    {
        Task AddUsuarioAsync(Usuario usuario);
        Task<Usuario?> GetUsuarioByNombreUsuarioAsync(string nombre);
        Task UpdateUsuarioAsync(Usuario usuario);
        Task Set2faCodeAsync(string username, string? code, System.DateTime? expiry);
        Task<List<Usuario>> GetAllUsersAsync();
        Task<List<HistorialContrasena>> GetHistorialContrasenasByUsuarioIdAsync(int idUsuario);
        Task AddHistorialContrasenaAsync(HistorialContrasena historial);
        Task DeleteUsuarioAsync(int usuarioId);
    }
}