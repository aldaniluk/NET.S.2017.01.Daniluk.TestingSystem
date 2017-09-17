using Domain.Entities;
using System.Linq;
using WebApplication.Models.User;

namespace WebApplication.Infrastructure.Mappers
{
    public static class UserMapper
    {
        public static UserProfileViewModel ToUserProfileViewModel(this User user)
        {
            return new UserProfileViewModel
            {
                Name = user.Name,
                Login = user.Login,
                Statistics = user.Statistics.Select(s => s.ToStatisticViewModel()).ToList()
            };
        }
    }
}