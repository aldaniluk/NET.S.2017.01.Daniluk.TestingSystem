using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Infrastructure.Mappers;
using WebApplication.Models.Statistic;
using WebApplication.Models.Test;

namespace WebApplication.Controllers
{
    public class TestController : Controller
    {
        private readonly ITestRepository testRepository;
        private readonly IQuestionRepository questionRepository;
        private readonly IAnswerRepository answerRepository;
        private readonly IStatisticRepository statisticRepository;
        private readonly IUserRepository userRepository;

        public TestController(ITestRepository testRepository, IQuestionRepository questionRepository, IAnswerRepository answerRepository, IStatisticRepository statisticRepository, IUserRepository userRepository)
        {
            this.testRepository = testRepository;
            this.questionRepository = questionRepository;
            this.answerRepository = answerRepository;
            this.statisticRepository = statisticRepository;
            this.userRepository = userRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            IEnumerable<PreviewTestViewModel> tests;
            if (User.IsInRole("admin"))
            {
                tests = testRepository.GetAll().Select(t => t.ToPreviewTestViewModel());
            }
            else
            {
                tests = testRepository.GetAllReady().Select(t => t.ToPreviewTestViewModel());
            }
            return View(tests);
        }

        [AllowAnonymous]
        public ActionResult Preview(int id)
        {
            PreviewTestViewModel test = testRepository.GetById(id).ToPreviewTestViewModel();
            if (!test.IsReady)
                return View("NotReadyTest");
            string message = "";
            if (User.Identity.IsAuthenticated)
            {
                int userId = userRepository.GetByLogin(User.Identity.Name).Id;
                if (statisticRepository.IsUserPassedTest(userId, id))
                    message = "*Attention: you passed this test, but you can do this again. Your results in the statistics will change. Good luck!"; 
            }
            ViewBag.Message = message;
            return View(test);
        }

        [HttpGet]
        [Authorize(Roles = "user")]
        public ActionResult PassTest(int id)
        {
            PassTestViewModel test = testRepository.GetById(id).ToPassTestViewModel();
            if (!test.IsReady)
                return View("NotReadyTest");
            return View(test);
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        [ActionName("PassTest")]
        public ActionResult PassedTest(PassTestViewModel passTestModel)
        {
            int userId = userRepository.GetByLogin(User.Identity.Name).Id;
            double percentage = CountPercentageOfRightAnswers(passTestModel.UserAnswers);
            TimeSpan time = DateTime.Now - passTestModel.BeginDate;
            StatisticViewModel statistic = new StatisticViewModel
            {
                TestId = passTestModel.Id,
                UserId = userId,
                Date = DateTime.Now,
                Time = new TimeSpan(time.Hours, time.Minutes, time.Seconds),
                Percentage = percentage,
                IsPassed = (percentage >= passTestModel.MinPercentage)
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

        public ActionResult SearchTest(string keyWord)
        {
            IEnumerable<PreviewTestViewModel> tests;
            if (User.IsInRole("admin"))
            {
                tests = testRepository.SearchAllTestsByKeyWord(keyWord).Select(t => t.ToPreviewTestViewModel());
            }
            else
            {
                tests = testRepository.SearchAllReadyTestsByKeyWord(keyWord).Select(t => t.ToPreviewTestViewModel());
            }
            
            return PartialView("_Tests", tests);
        }

        //methods for admin
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View("CreateTest");
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ActionName("Create")]
        public ActionResult Created(TestViewModel test)
        {
            if (ModelState.IsValid)
            {
                testRepository.Create(test.ToTest());
                TestViewModel testViewModel = testRepository.GetByName(test.Name).ToTestViewModel();
                return View("DetailsTest", testViewModel);
            }
            return View("Create");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            TestViewModel test = testRepository.GetById(id).ToTestViewModel();
            return View("EditTest", test);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ActionName("Edit")]
        public ActionResult Edited(TestViewModel test)
        {
            if (ModelState.IsValid)
            {
                testRepository.Update(test.ToTest());
                return RedirectToAction("Details", "Test", new { id = test.Id });
            }
            return View("EditTest", test);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            TestViewModel test = testRepository.GetById(id).ToTestViewModel();
            return View("DeleteTest", test);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ActionName("Delete")]
        public ActionResult Deleted(TestViewModel test)
        {
            testRepository.Delete(test.ToTest());
            return RedirectToAction("Index", "Test");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Details(int id)
        {
            TestViewModel test = testRepository.GetById(id).ToTestViewModel();
            return View("DetailsTest", test);
        }

    }
}