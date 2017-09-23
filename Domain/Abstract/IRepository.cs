using System.Collections.Generic;

namespace Domain.Abstract
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
