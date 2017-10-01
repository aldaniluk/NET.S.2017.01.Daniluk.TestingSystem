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
            User user = UserRepository.GetByLogin(login);
            List<Role> userRoles = new List<Role>();
            foreach(var r in user.Roles)
            {
                userRoles.Add(RoleRepository.GetById(r.Id));
            } 

            return (userRoles != null && userRoles.Where(r => r.Name.Contains(roleName)) != null);
        }

        public override string[] GetRolesForUser(string login)
        {
            string[] roles = new string[] { };
            IEnumerable<Role> userRoles = UserRepository.GetByLogin(login).Roles;            
            if (userRoles != null)
            {
                roles = userRoles.Select(r => r.Name).ToArray();
            }
            return roles;
        }

        #region Stubs
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

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