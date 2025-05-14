using System.Collections.Generic;
using BusinessLogic.Models;

namespace BusinessLogic.Services
{
    public interface IUserService
    {
        IEnumerable<UserDto> GetAllUsers();
        UserDto CreateUser(UserRequest request);
        UserDto UpdateUser(int id, UserRequest request);
        void DeleteUser(int id);
    }
}
