using Domain.Entities;
using WebApplication.Models.Statistic;

namespace WebApplication.Infrastructure.Mappers
{
    public static class StatisticMapper
    {
        public static StatisticViewModel ToStatisticViewModel(this Statistic statistic)
        {
            return new StatisticViewModel
            {
                UserId = statistic.UserId,
                TestId = statistic.TestId,
                TestName = statistic.Test.Name,
                UserName = statistic.User.Name,
                Date = statistic.Date,
                Time = statistic.Time,
                IsPassed = statistic.IsPassed,
                Percentage = statistic.Percentage
            };
        }

        public static Statistic ToStatistic(this StatisticViewModel statistic)
        {
            return new Statistic
            {
                UserId = statistic.UserId,
                TestId = statistic.TestId,
                Date = statistic.Date,
                Time = statistic.Time,
                IsPassed = statistic.IsPassed,
                Percentage = statistic.Percentage
            };
        }
    }
}