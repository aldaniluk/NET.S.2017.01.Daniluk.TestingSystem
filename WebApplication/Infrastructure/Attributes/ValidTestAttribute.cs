using System.ComponentModel.DataAnnotations;
using System.Web;
using WebApplication.Models.Test;

namespace WebApplication.Infrastructure.Attributes
{
    public class ValidTestAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            TestViewModel test = value as TestViewModel;

            if (test == null) return new ValidationResult("Test is incorrect.");
            if (test.IsReady == false) return ValidationResult.Success;
            
            if (test.Questions == null || test.Questions?.Count < 2) return new ValidationResult("Test must have at least 3 questions.");

            foreach (var q in test.Questions)
            {
                if (q.Answers == null || q.Answers?.Count < 2) return new ValidationResult("Each question must have at least 2 possible answers.");

                int countRightAnswers = 0;
                foreach (var a in q.Answers)
                {
                    if (a.Right) countRightAnswers++;
                }
                if (countRightAnswers != 1) return new ValidationResult("Only one of answers must be correct.");
            }
            return ValidationResult.Success;
        }
    }
}