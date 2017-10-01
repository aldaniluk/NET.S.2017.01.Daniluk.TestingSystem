using System;
using System.Collections.Generic;
using WebApplication.Models.Answer;
using WebApplication.Models.Question;

namespace WebApplication.Models.Test
{
    public class PassTestViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double MinPercentage { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
        public int[] UserAnswers { get; set; }
        public DateTime BeginDate { get; set; }
        public bool IsReady { get; set; }
    }
}