using System.Linq;
using System;
using System.Linq.Expressions;
using System.Data.Entity;
using Domain.Entities;
using System.Collections.Generic;
using Domain.Abstract;

namespace Domain.Consrete
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext context;

        public UserRepository(DbContext context)
        {
            this.context = context;
        }

        public User GetById(int id)
        {
            return context.Set<User>().FirstOrDefault(r => r.Id == id);
        }

        public User GetByLogin(string login)
        {
            return context.Set<User>().FirstOrDefault(r => r.Login == login);
        }

        public IEnumerable<User> GetAll()
        {
            return context.Set<User>();
        }

        public void Create(User entity)
        {
            context.Set<User>().Add(entity);
            context.SaveChanges();
        }

        public void Update(User entity)
        {
            if (entity == null) return;

            User userToUpdate = context.Set<User>().FirstOrDefault(u => u.Id == entity.Id);
            context.Entry(userToUpdate).CurrentValues.SetValues(entity);
            context.SaveChanges();
        }

        public void Delete(User entity)
        {
            User user = context.Set<User>().Single(u => u.Id == entity.Id);
            context.Set<User>().Remove(user);
            context.SaveChanges();
        }
    }
}
