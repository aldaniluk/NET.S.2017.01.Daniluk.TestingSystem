using Domain.Entities;

namespace Domain.Abstract
{
    public interface IStatisticRepository : IRepository<Statistic>
    {
        bool IsUserPassedTest(int userId, int testId);
    }
}
