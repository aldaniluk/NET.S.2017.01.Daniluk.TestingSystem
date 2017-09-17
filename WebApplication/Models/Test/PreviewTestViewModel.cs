using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models.Test
{
    public class PreviewTestViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double MinPercentage { get; set; }
        public int QuestionQuantity { get; set; }
        public int UsersQuantity { get; set; }
        public double AveragePercentage { get; set; }
    }
}