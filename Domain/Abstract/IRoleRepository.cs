using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Abstract
{
    public interface IRoleRepository : IRepository<Role>
    {
        Role GetById(int id);
        Role GetByName(string name);
    }
}
