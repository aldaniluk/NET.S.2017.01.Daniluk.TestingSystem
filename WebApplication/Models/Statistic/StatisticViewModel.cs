using System;

namespace WebApplication.Models.Statistic
{
    public class StatisticViewModel
    {
        public int UserId { get; set; }
        public int TestId { get; set; }
        public string TestName { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public double Percentage { get; set; }
        public bool IsPassed { get; set; }
    }
}