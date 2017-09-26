using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace WebApplication.Providers
{
    public class CustomMembershipProvider : MembershipProvider
    {
        public IUserRepository UserRepository
            => (IUserRepository)DependencyResolver.Current.GetService(typeof(IUserRepository));

        public IRoleRepository RoleRepository
            => (IRoleRepository)DependencyResolver.Current.GetService(typeof(IRoleRepository));

        public MembershipUser CreateUser(string login, string name, string password)
        {
            MembershipUser membershipUser = GetUser(login, false);

            if (membershipUser != null) return null;

            var user = new User
            {
                Login = login,
                Name = name,
                Password = Crypto.HashPassword(password)
            };

            IEnumerable<Role> roles = RoleRepository.GetByName("user");
            if (roles != null)
            {
                user.Roles = roles.ToList();
            }

            UserRepository.Create(user);
            membershipUser = GetUser(login, false);
            return membershipUser;
        }

        public override bool ValidateUser(string login, string password)
        {
            var user = UserRepository.GetByLogin(login);
            return (user != null && Crypto.VerifyHashedPassword(user.Password, password));
        }

        public override MembershipUser GetUser(string login, bool userIsOnline)
        {
            var user = UserRepository.GetByLogin(login);

            if (user == null) return null;

            var memberUser = new MembershipUser("CustomMembershipProvider", user.Login,
                null, null, null, null,
                false, false, DateTime.Now,
                DateTime.MinValue, DateTime.MinValue,
                DateTime.MinValue, DateTime.MinValue);

            return memberUser;
        }

        #region Stabs
        public override MembershipUser CreateUser(string username, string password, string login,
            string passwordQuestion,
            string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion,
            string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override string ApplicationName { get; set; }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }
        #endregion     
    }
}