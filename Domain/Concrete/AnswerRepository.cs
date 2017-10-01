using Domain.Abstract;
using Domain.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Domain.Concrete
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly DbContext context;

        public AnswerRepository(DbContext context)
        {
            this.context = context;
        }

        public Answer GetById(int id)
        {
            return context.Set<Answer>().FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Answer> GetAll()
        {
            return context.Set<Answer>();
        }

        public void Create(Answer entity)
        {
            context.Set<Answer>().Add(entity);
            context.SaveChanges();
        }

        public void Update(Answer entity)
        {
            Answer answerToUpdate = context.Set<Answer>().FirstOrDefault(a => a.Id == entity.Id);
            context.Entry(answerToUpdate).CurrentValues.SetValues(entity);
            context.SaveChanges();
        }

        public void Delete(Answer entity)
        {
            Answer answer = context.Set<Answer>().FirstOrDefault(a => a.Id == entity.Id);
            context.Set<Answer>().Remove(answer);
            context.SaveChanges();
        }
    }
}
