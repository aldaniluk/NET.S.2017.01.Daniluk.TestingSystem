using System.Collections.Generic;

namespace Domain.Abstract
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        //T GetOneByPredicate(Expression<Func<T, bool>> func);
        //IEnumerable<T> GetAllByPredicate(Expression<Func<T, bool>> func);
    }
}
