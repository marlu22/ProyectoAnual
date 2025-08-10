// src/DataAccess/Repositories/IUserRepository.cs
using System.Collections.Generic;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public interface IUserRepository
    {
        void AddUsuario(Usuario usuario);
        Usuario? GetUsuarioByNombreUsuario(string nombre);
        void UpdateUsuario(Usuario usuario);
        void Set2faCode(string username, string? code, System.DateTime? expiry);
        List<Usuario> GetAllUsers();
        List<HistorialContrasena> GetHistorialContrasenasByUsuarioId(int idUsuario);
        void AddHistorialContrasena(HistorialContrasena historial);
        void DeleteUsuario(int usuarioId);
    }
}