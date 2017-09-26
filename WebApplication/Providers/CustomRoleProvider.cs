using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace WebApplication.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public IUserRepository UserRepository
            => (IUserRepository)DependencyResolver.Current.GetService(typeof(IUserRepository));

        public IRoleRepository RoleRepository
            => (IRoleRepository)DependencyResolver.Current.GetService(typeof(IRoleRepository));

        public override bool IsUserInRole(string login, string roleName)
        {
            User user = UserRepository.GetAll().FirstOrDefault(u => u.Login == login);

            if (user == null) return false;

            List<Role> userRoles = new List<Role>();
            foreach(var r in user.Roles)
            {
                userRoles.Add(RoleRepository.GetById(r.Id));
            } 

            return (userRoles != null && userRoles.Where(r => r.Name.Contains(roleName)) != null);
        }

        public override string[] GetRolesForUser(string login)
        {
            using (var context = new Context())
            {
                var roles = new string[] { };
                var user = context.Users.FirstOrDefault(u => u.Login == login);

                if (user == null) return roles;

                var userRoles = user.Roles;

                if (userRoles != null)
                {
                    roles = userRoles.Select(r => r.Name).ToArray();
                }
                return roles;
            }
        }

        public override void CreateRole(string roleName)
        {
            var newRole = new Role() { Name = roleName };
            using (var context = new Context())
            {
                context.Roles.Add(newRole);
                context.SaveChanges();
            }
        }

        #region Stabs
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
        #endregion;
    }
}