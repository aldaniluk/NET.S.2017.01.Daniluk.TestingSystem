using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class PassTestModel
    {
        public int TestId { get; set; }
        public int UserId { get; set; }
        public double MinPercentage { get; set; }
        public int[] UserAnswers { get; set; }
        public DateTime BeginDate { get; set; }
    }
}
