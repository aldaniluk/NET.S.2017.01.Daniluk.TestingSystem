using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Consrete
{
    public class StatisticRepository : IStatisticRepository
    {
        private readonly DbContext context;

        public StatisticRepository(DbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Statistic> GetByUserId(int id)
        {
            return context.Set<Statistic>().Where(s => s.UserId == id);
        }

        public IEnumerable<Statistic> GetByTestId(int id)
        {
            return context.Set<Statistic>().Where(s => s.TestId == id);
        }

        public IEnumerable<Statistic> GetAll()
        {
            return context.Set<Statistic>();
        }

        public void Create(Statistic entity)
        {
            context.Set<Statistic>().Add(entity);
            context.SaveChanges();
        }

        public void Update(Statistic entity)
        {
            if (entity == null) return;

            Statistic statisticToUpdate = context.Set<Statistic>().FirstOrDefault(s => s.UserId == entity.UserId && s.TestId == entity.TestId);
            context.Entry(statisticToUpdate).CurrentValues.SetValues(entity);
            context.SaveChanges();
        }

        public void Delete(Statistic entity)
        {
            Statistic statistic = context.Set<Statistic>().Single(s => s.UserId == entity.UserId && s.TestId == entity.TestId);
            context.Set<Statistic>().Remove(statistic);
            context.SaveChanges();
        }

        public bool IsUserPassedTest(int userId, int testId)
        {
            return (context.Set<Statistic>().FirstOrDefault(s => s.TestId == testId && s.UserId == userId) != null);
        }

        public IEnumerable<Statistic> FilterStatistic(int? testId, string sortType)
        {
            IEnumerable<Statistic> statictis;
            statictis = (testId != null && testId != 0) ? GetByTestId((int)testId) : GetAll();
            if (sortType == "Date")
            {
                statictis = statictis.OrderByDescending(s => s.Date);
            }
            else if (sortType == "Time")
            {
                statictis = statictis.OrderBy(s => s.Time);
            }
            else
            {
                statictis = statictis.OrderByDescending(s => s.Percentage);
            }
            return statictis;
        }
    }
}
