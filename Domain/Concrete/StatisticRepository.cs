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

        public Statistic GetById(int id)
        {
            throw new NotImplementedException();
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
            Statistic ormStatistic = entity;
            context.Set<Statistic>().Attach(statisticToUpdate);
            statisticToUpdate.TestId = ormStatistic.TestId;
            statisticToUpdate.UserId = ormStatistic.UserId;
            statisticToUpdate.Date = ormStatistic.Date;
            statisticToUpdate.Time = ormStatistic.Time;
            statisticToUpdate.Percentage = ormStatistic.Percentage;
            statisticToUpdate.IsPassed = ormStatistic.IsPassed;
            context.Entry(statisticToUpdate).State = EntityState.Modified;
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
            return (context.Set<Statistic>().FirstOrDefault(s => s.TestId == testId && s.UserId == userId) == null) ? false : true;
        }


    }
}
