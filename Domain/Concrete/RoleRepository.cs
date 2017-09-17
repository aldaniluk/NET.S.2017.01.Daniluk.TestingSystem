using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Consrete
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DbContext context;

        public RoleRepository(DbContext context)
        {
            this.context = context;
        }

        public Role GetById(int id)
        {
            return context.Set<Role>().FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Role> GetAll()
        {
            return context.Set<Role>().AsEnumerable();
        }

        public void Create(Role entity)
        {
            context.Set<Role>().Add(entity);
            context.SaveChanges();
        }

        public void Update(Role entity)
        {
            if (entity == null) return;

            Role roleToUpdate = context.Set<Role>().FirstOrDefault(r => r.Id == entity.Id);
            Role ormRole = entity;
            context.Set<Role>().Attach(roleToUpdate);
            roleToUpdate.Users = ormRole.Users;
            roleToUpdate.Name = ormRole.Name;
            context.Entry(roleToUpdate).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(Role entity)
        {
            Role role = context.Set<Role>().Single(r => r.Id == entity.Id);
            context.Set<Role>().Remove(role);
            context.SaveChanges();
        }
    }
}
