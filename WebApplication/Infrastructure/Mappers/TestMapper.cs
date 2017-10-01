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
            return new TestViewModel
            {
                Id = test.Id,
                Description = test.Description,
                Name = test.Name,
                MinPercentage = test.MinPercentage,
                IsReady = test.IsReady,
                Questions = test.Questions.Select(q => q.ToQuestionViewModel()).ToList()
            };
        }

        public static PassTestViewModel ToPassTestViewModel(this Test test)
        {
            return new PassTestViewModel
            {
                Id = test.Id,
                Description = test.Description,
                Name = test.Name,
                MinPercentage = test.MinPercentage,
                Questions = test.Questions.Select(q => q.ToQuestionViewModel()).ToList(),
                UserAnswers = new int[test.Questions.Count()],
                BeginDate = DateTime.Now,
                IsReady = test.IsReady
            };
        }

        public static PreviewTestViewModel ToPreviewTestViewModel(this Test test)
        {
            IEnumerable<Statistic> statistics = test.Statistics.Where(s => s.TestId == test.Id);
            double averagePerc = 0;
            if (statistics != null && statistics?.Count() != 0)
                averagePerc = Math.Round(statistics.Sum(s => s.Percentage) / statistics.Count(), 2);

            return new PreviewTestViewModel
            {
                Id = test.Id,
                Description = test.Description,
                Name = test.Name,
                MinPercentage = test.MinPercentage,
                AveragePercentage = averagePerc,
                QuestionQuantity = test.Questions.Count(),
                UsersQuantity = statistics.Count(),
                IsReady = test.IsReady
            };
        }

        public static Test ToTest(this TestViewModel test)
        {
            return new Test
            {
                Id = test.Id,
                Description = test.Description,
                Name = test.Name,
                MinPercentage = test.MinPercentage,
                IsReady = test.IsReady,
                Questions = test.Questions?.Select(q => q.ToQuestion()).ToList()
            };
        }

        public static PassTestModel ToPassTestModel(this PassTestViewModel test)
        {
            return new PassTestModel
            {
                TestId = test.Id,
                UserId = test.UserId,
                MinPercentage = test.MinPercentage,
                UserAnswers = test.UserAnswers,
                BeginDate = DateTime.Now
            };
        }
    }
}