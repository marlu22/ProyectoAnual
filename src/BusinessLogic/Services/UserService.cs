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
            var users = _userRepository.GetAll();
            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email
            });
        }

        public UserDto CreateUser(UserRequest request)
        {
            if (string.IsNullOrEmpty(request.Password))
                throw new ArgumentException("Password is required.", nameof(request.Password));

            var user = new User
            {
                Username = request.Username,
                Password = request.Password,
                Email = request.Email
            };

            _userRepository.Add(user);

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };
        }

        public UserDto UpdateUser(int id, UserRequest request)
        {
            var user = _userRepository.GetById(id);
            if (user == null) throw new KeyNotFoundException("Usuario no encontrado.");

            user.Username = request.Username;
            user.Email = request.Email;

            _userRepository.Update(user);

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };
        }

        public void DeleteUser(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null) throw new KeyNotFoundException("Usuario no encontrado.");

            _userRepository.Delete(user);
        }
    }
}
