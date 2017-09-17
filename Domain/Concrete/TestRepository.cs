using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;
using System.Linq.Expressions;
using Domain.Entities;
using Domain.Abstract;

namespace Domain.Consrete
{
    public class TestRepository : ITestRepository
    {
        private readonly DbContext context;

        public TestRepository(DbContext context)
        {
            this.context = context;
        }

        public Test GetById(int id)
        {
            return context.Set<Test>().FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Test> GetAll()
        {
            return context.Set<Test>();
        }

        public void Create(Test entity)
        {
            context.Set<Test>().Add(entity);
            context.SaveChanges();
        }

        public void Update(Test entity)
        {
            if (entity == null) return;

            Test testToUpdate = context.Set<Test>().FirstOrDefault(t => t.Id == entity.Id);
            Test ormTest = entity;
            context.Set<Test>().Attach(testToUpdate);
            testToUpdate.Name = ormTest.Name;
            testToUpdate.Description = ormTest.Description;
            testToUpdate.MinPercentage = ormTest.MinPercentage;
            testToUpdate.Questions = ormTest.Questions;
            testToUpdate.Statistics = ormTest.Statistics;
            context.Entry(testToUpdate).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(Test entity)
        {
            Test test = context.Set<Test>().Single(t => t.Id == entity.Id);
            context.Set<Test>().Remove(test);
            context.SaveChanges();
        }

    }
}
