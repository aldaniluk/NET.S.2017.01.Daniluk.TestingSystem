using System;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class PassTestService : IPassTestService
    {
        private readonly IStatisticRepository statisticRepository;
        private readonly IAnswerRepository answerRepository;

        public PassTestService(IStatisticRepository statisticRepository, IAnswerRepository answerRepository)
        {
            this.statisticRepository = statisticRepository;
            this.answerRepository = answerRepository;
        }

        public Statistic PassTest(PassTestModel passTest)
        {
            double percentage = CountPercentageOfRightAnswers(passTest.UserAnswers);
            TimeSpan time = DateTime.Now - passTest.BeginDate;
            Statistic statistic = new Statistic
            {
                TestId = passTest.TestId,
                UserId = passTest.UserId,
                Date = DateTime.Now,
                Time = new TimeSpan(time.Hours, time.Minutes, time.Seconds),
                Percentage = percentage,
                IsPassed = (percentage >= passTest.MinPercentage)
            };
            if (statisticRepository.IsUserPassedTest(passTest.UserId, passTest.TestId))
                statisticRepository.Update(statistic);
            else
                statisticRepository.Create(statistic);
            Statistic st = statisticRepository.GetStatistic(passTest.UserId, passTest.TestId);
            return st;
        }

        private double CountPercentageOfRightAnswers(int[] userAnswers)
        {
            int rightAnswers = 0;
            int allAnswers = userAnswers.Length;
            for (int i = 0; i < allAnswers; i++)
            {
                if (answerRepository.GetById(userAnswers[i]).Right)
                    rightAnswers++;
            }
            return Math.Round((double)rightAnswers / allAnswers * 100, 2);
        }
    }
}
