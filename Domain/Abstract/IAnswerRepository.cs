using Domain.Entities;

namespace Domain.Abstract
{
    public interface IAnswerRepository : IRepository<Answer>
    {
        Answer GetById(int id);
    }
}
