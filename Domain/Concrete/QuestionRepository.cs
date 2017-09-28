using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;
using System.Linq.Expressions;
using Domain.Entities;
using Domain.Abstract;

namespace Domain.Consrete
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly DbContext context;

        public QuestionRepository(DbContext context)
        {
            this.context = context;
        }

        public Question GetById(int id)
        {
            return context.Set<Question>().FirstOrDefault(q => q.Id == id);
        }

        public IEnumerable<Question> GetAll()
        {
            return context.Set<Question>();
        }

        public void Create(Question entity)
        {
            context.Set<Question>().Add(entity);
            context.SaveChanges();
        }

        public void Update(Question entity)
        {
            Question questionToUpdate = context.Set<Question>().FirstOrDefault(q => q.Id == entity.Id);
            context.Entry(questionToUpdate).CurrentValues.SetValues(entity);
            context.SaveChanges();
        }

        public void Delete(Question entity)
        {
            Question question = context.Set<Question>().FirstOrDefault(q => q.Id == entity.Id);
            context.Set<Question>().Remove(question);
            context.SaveChanges();
        }

    }
}
