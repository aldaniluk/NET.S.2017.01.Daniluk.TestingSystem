using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication.Infrastructure.Mappers;
using WebApplication.Models.Statistic;
using WebApplication.Models.Test;

namespace WebApplication.Controllers
{
    public class TestController : Controller
    {
        private readonly ITestRepository testRepository;
        private readonly IAnswerRepository answerRepository;
        private readonly IStatisticRepository statisticRepository;

        public TestController(ITestRepository testRepository, IAnswerRepository answerRepository, IStatisticRepository statisticRepository)
        {
            this.testRepository = testRepository;
            this.answerRepository = answerRepository;
            this.statisticRepository = statisticRepository;
        }

        [HttpGet]
        public ActionResult AllTests()
        {
            IEnumerable<PreviewTestViewModel> tests = testRepository.GetAll().Select(t => t.ToPreviewTestViewModel());
            return View(tests);
        }

        public ActionResult TestPreview(int id)
        {
            PreviewTestViewModel test = testRepository.GetById(id).ToPreviewTestViewModel();
            string message = "";
            if (statisticRepository.IsUserPassedTest(2, id))
                message = "You passed this test. If you will repass, your percentage will update.";
            ViewBag.Message = message;
            return View(test);
        }

        [HttpGet]
        public ActionResult PassTest(int id)
        {
            PassTestViewModel test = testRepository.GetById(id).ToPassTestViewModel();
            return View(test);
        }

        [ActionName("PassTest")]
        [HttpPost]
        public ActionResult PassedTest(PassTestViewModel passTestModel)
        {
            int userId = 2; //
            double percentage = CountPercentageOfRightAnswers(passTestModel.UserAnswers);
            TimeSpan time = DateTime.Now - passTestModel.BeginDate;
            StatisticViewModel statistic = new StatisticViewModel
            {
                TestId = passTestModel.Id,
                UserId = userId,
                Date = DateTime.Now,
                Time = new TimeSpan(time.Hours, time.Minutes, time.Seconds),
                Percentage = percentage,
                IsPassed = percentage >= passTestModel.MinPercentage ? true : false
            };
            if (statisticRepository.IsUserPassedTest(userId, passTestModel.Id))
                statisticRepository.Update(statistic.ToStatistic());
            else
                statisticRepository.Create(statistic.ToStatistic());
            string message = statistic.IsPassed ? 
                $"You passed successfully the test {passTestModel.Name}!" :
                $"You didn't pass the test {passTestModel.Name}. But don't give up, try again!";
            ViewBag.Message = message;
            return View("PassedTest", statistic);
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