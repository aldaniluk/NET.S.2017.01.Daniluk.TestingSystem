using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Models.Answer;
using WebApplication.Models.Test;

namespace WebApplication.Infrastructure.Mappers
{
    public static class TestMapper
    {
        public static TestViewModel ToTestViewModel(this Test test)
        {
            if (test == null) return null;
            return new TestViewModel
            {
                Id = test.Id,
                Description = test.Description,
                Name = test.Name,
                MinPercentage = test.MinPercentage,
            };
        }

        public static PassTestViewModel ToPassTestViewModel(this Test test)
        {
            if (test == null) return null;
            return new PassTestViewModel
            {
                Id = test.Id,
                Description = test.Description,
                Name = test.Name,
                MinPercentage = test.MinPercentage,
                Questions = test.Questions.Select(q => q.ToQuestionViewModel()).ToList(),
                UserAnswers = new int[test.Questions.Count()],
                BeginDate = DateTime.Now
            };
        }

        public static PreviewTestViewModel ToPreviewTestViewModel(this Test test)
        {
            if (test == null) return null;
            IEnumerable<Statistic> statistics = test.Statistics.Where(s => s.TestId == test.Id);
            return new PreviewTestViewModel
            {
                Id = test.Id,
                Description = test.Description,
                Name = test.Name,
                MinPercentage = test.MinPercentage,
                AveragePercentage = statistics.Sum(s => s.Percentage) / statistics.Count(),
                QuestionQuantity = test.Questions.Count(),
                UsersQuantity = statistics.Count()
            };
        }
    }
}