using System.Collections.Generic;
using WebApplication.Models.Statistic;

namespace WebApplication.Models.User
{
    public class UserProfileViewModel
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public List<StatisticViewModel> Statistics { get; set; }
    }
}