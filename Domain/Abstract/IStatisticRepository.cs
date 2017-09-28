using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Abstract
{
    public interface IStatisticRepository : IRepository<Statistic>
    {
        bool IsUserPassedTest(int userId, int testId);
        Statistic GetStatistic(int userId, int testId);
        IEnumerable<Statistic> GetByUserId(int id);
        IEnumerable<Statistic> GetByTestId(int id);
        IEnumerable<Statistic> FilterStatistic(int? testId, StatisticSortType sortType);
    }
}
