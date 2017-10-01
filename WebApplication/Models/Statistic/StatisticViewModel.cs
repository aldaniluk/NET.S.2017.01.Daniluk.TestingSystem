using System;
using System.ComponentModel;

namespace WebApplication.Models.Statistic
{
    public class StatisticViewModel
    {
        public int UserId { get; set; }

        public int TestId { get; set; }

        [DisplayName("Test name")]
        public string TestName { get; set; }

        [DisplayName("User name")]
        public string UserName { get; set; }

        [DisplayName("Date of passing test")]
        public DateTime Date { get; set; }

        [DisplayName("Time")]
        public TimeSpan Time { get; set; }

        [DisplayName("Percentage")]
        public double Percentage { get; set; }

        [DisplayName("Test passed")]
        public bool IsPassed { get; set; }
    }
}