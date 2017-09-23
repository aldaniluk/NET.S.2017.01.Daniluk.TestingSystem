using Domain.Entities;

namespace Domain.Abstract
{
    public interface IQuestionRepository : IRepository<Question>
    {
        Question GetById(int id);
    }
}
