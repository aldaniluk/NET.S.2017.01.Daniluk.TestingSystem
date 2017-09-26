using Domain.Entities;

namespace Domain.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        User GetById(int id);
        User GetByLogin(string login);
    }
}
