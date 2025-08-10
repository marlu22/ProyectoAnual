using System.Collections.Generic;
using BusinessLogic.Models;

namespace BusinessLogic.Services
{
    public interface IUserService
    {
        void CrearUsuario(UserRequest request);
        void UpdateUser(UserDto user);
        void DeleteUser(int userId);
        List<UserDto> GetAllUsers();
        UserDto? GetUserByUsername(string username);
    }
}
