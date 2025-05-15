using System.Collections.Generic;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<Usuario> GetAll();
        Usuario GetById(int id);
        void Add(Usuario usuario);
        void Update(Usuario usuario);
        void Delete(Usuario usuario);
    }
}
