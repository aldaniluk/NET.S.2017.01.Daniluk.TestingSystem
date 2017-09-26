using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApplication.Models.Test
{
    public class PreviewTestViewModel
    {
        public int Id { get; set; }

        [DisplayName("Test name")]
        public string Name { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Minimal percentage")]
        public double MinPercentage { get; set; }

        [DisplayName("Questions")]
        public int QuestionQuantity { get; set; }

        [DisplayName("Users, passed test")]
        public int UsersQuantity { get; set; }

        [DisplayName("Average percentage")]
        public double AveragePercentage { get; set; }

        [DisplayName("Is test ready")]
        public bool IsReady { get; set; }
    }
}