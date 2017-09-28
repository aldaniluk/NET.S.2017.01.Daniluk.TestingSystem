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

        public IEnumerable<Test> GetAllReady()
        {
            return context.Set<Test>().Where(t => t.IsReady == true);
        }

        public void Create(Test entity)
        {
            context.Set<Test>().Add(entity);
            context.SaveChanges();
        }

        public void Update(Test entity)
        {
            Test testToUpdate = context.Set<Test>().FirstOrDefault(t => t.Id == entity.Id);
            context.Entry(testToUpdate).CurrentValues.SetValues(entity);
            context.SaveChanges();
        }

        public void Delete(Test entity)
        {
            Test test = context.Set<Test>().FirstOrDefault(t => t.Id == entity.Id);
            context.Set<Test>().Remove(test);
            context.SaveChanges();
        }

        public Test GetByName(string name)
        {
            return context.Set<Test>().Where(t => t.Name == name).FirstOrDefault();
        }

        public IEnumerable<Test> SearchAllTestsByKeyWord(string keyWord)
        {
            if (string.IsNullOrEmpty(keyWord))
                return GetAll();
            return context.Set<Test>().Where(t => t.Name.ToLower().Contains(keyWord.ToLower()) ||
                t.Description.ToLower().Contains(keyWord.ToLower()));
        }

        public IEnumerable<Test> SearchAllReadyTestsByKeyWord(string keyWord)
        {
            if (string.IsNullOrEmpty(keyWord))
                return GetAllReady();
            return context.Set<Test>().Where(t => t.IsReady == true)
                .Where(t => t.Name.ToLower().Contains(keyWord.ToLower()) || 
                t.Description.ToLower().Contains(keyWord.ToLower()));
        }

    }
}
