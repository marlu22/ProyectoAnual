using System.Collections.Generic;
using System.Linq;
using DataAccess.Repositories;
using DataAccess.Entities;
using BusinessLogic.Models;

namespace BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            var usuarios = _userRepository.GetAll();
            return usuarios.Select(u => new UserDto
            {
                Id = u.IdUsuario,
                Username = u.UsuarioNombre,
                // Agrega aquí otros campos que tengas en UserDto
            });
        }

        public UserDto CreateUser(UserRequest request)
        {
            if (string.IsNullOrEmpty(request.Password))
                throw new ArgumentException("Password is required.", nameof(request.Password));

            var usuario = new Usuario
            {
                UsuarioNombre = request.Username,
                ContrasenaScript = System.Text.Encoding.UTF8.GetBytes(request.Password),
                // Asigna aquí otros campos requeridos, por ejemplo:
                // IdPersona = ...,
                // IdRol = ...,
            };

            _userRepository.Add(usuario);

            return new UserDto
            {
                Id = usuario.IdUsuario,
                Username = usuario.UsuarioNombre,
                // Otros campos
            };
        }

        public UserDto UpdateUser(int id, UserRequest request)
        {
            var usuario = _userRepository.GetById(id);
            if (usuario == null) throw new KeyNotFoundException("Usuario no encontrado.");

            usuario.UsuarioNombre = request.Username;
            // Actualiza otros campos si es necesario

            _userRepository.Update(usuario);

            return new UserDto
            {
                Id = usuario.IdUsuario,
                Username = usuario.UsuarioNombre,
                // Otros campos
            };
        }

        public void DeleteUser(int id)
        {
            var usuario = _userRepository.GetById(id);
            if (usuario == null) throw new KeyNotFoundException("Usuario no encontrado.");

            _userRepository.Delete(usuario);
        }
    }
}
