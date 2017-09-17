using Domain.Abstract;
using Domain.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Domain.Consrete
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
            if (entity == null) return;

            Answer answerToUpdate = context.Set<Answer>().FirstOrDefault(a => a.Id == entity.Id);
            Answer ormAnswer = entity;
            context.Set<Answer>().Attach(answerToUpdate);
            answerToUpdate.Text = ormAnswer.Text;
            answerToUpdate.Img = ormAnswer.Img;
            answerToUpdate.Explanation = ormAnswer.Explanation;
            answerToUpdate.Right = ormAnswer.Right;
            answerToUpdate.QuestionId = ormAnswer.QuestionId;
            context.Entry(answerToUpdate).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(Answer entity)
        {
            Answer answer = context.Set<Answer>().Single(a => a.Id == entity.Id);
            context.Set<Answer>().Remove(answer);
            context.SaveChanges();
        }

        //public DalAnswer GetOneByPredicate(Expression<Func<DalAnswer, bool>> func)
        //{
        //    //?????????
        //    return context.Set<Answer>().Select(a => a.ToDalAnswer()).FirstOrDefault(func);
        //}

        //public IEnumerable<DalAnswer> GetAllByPredicate(Expression<Func<DalAnswer, bool>> func)
        //{
        //    return context.Set<Answer>().Select(a => a.ToDalAnswer()).Where(func);
        //}
    }
}
