using Domain.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication.Infrastructure.Mappers;
using WebApplication.Models.Paging;
using WebApplication.Models.Test;

namespace WebApplication.Controllers
{
    public class TestController : Controller
    {
        private readonly ITestRepository testRepository;
        private readonly IStatisticRepository statisticRepository;
        private readonly IUserRepository userRepository;
        private readonly IPassTestService passTestService;
        public int PageSize = 3;

        public TestController(ITestRepository testRepository, IStatisticRepository statisticRepository, 
            IUserRepository userRepository, IPassTestService passTestService)
        {
            this.testRepository = testRepository;
            this.statisticRepository = statisticRepository;
            this.userRepository = userRepository;
            this.passTestService = passTestService;
        }

        public ActionResult Index()
        {
            TestPreviewPagingViewModel pagingViewModel = GetTestPreviewPagingViewModel(1, "");
            return View(pagingViewModel);
        }

        public ActionResult Search(int page = 1, string keyWord = "")
        {
            TestPreviewPagingViewModel pagingViewModel = GetTestPreviewPagingViewModel(page, keyWord);
            if (Request.IsAjaxRequest())
            {
                return Json(pagingViewModel, JsonRequestBehavior.AllowGet);
            }
            return View("Index", pagingViewModel); 
        }

        private TestPreviewPagingViewModel GetTestPreviewPagingViewModel(int page, string keyWord)
        {
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = User.IsInRole("admin") ?
                    testRepository.SearchAllTestsByKeyWord(keyWord).Count() :
                    testRepository.SearchAllReadyTestsByKeyWord(keyWord).Count()
            };
            List<PreviewTestViewModel> tests = User.IsInRole("admin") ?
                    testRepository.SearchAllTestsByKeyWord(keyWord).Skip((page - 1) * PageSize).Take(PageSize)
                    .Select(t => t.ToPreviewTestViewModel()).ToList() :
                    testRepository.SearchAllReadyTestsByKeyWord(keyWord).Skip((page - 1) * PageSize).Take(PageSize)
                    .Select(t => t.ToPreviewTestViewModel()).ToList();
            return new TestPreviewPagingViewModel { PagingInfo = pagingInfo, Tests = tests };
        }

        public ActionResult Preview(int id)
        {
            PreviewTestViewModel test = testRepository.GetById(id).ToPreviewTestViewModel();
            if (!test.IsReady)
            {
                return View("NotReadyTest");
            }
            string message = "";
            if (User.Identity.IsAuthenticated)
            {
                int userId = userRepository.GetByLogin(User.Identity.Name).Id;
                if (statisticRepository.IsUserPassedTest(userId, id))
                    message = "*Attention: you passed this test, but you can do this again. " +
                        "Your results in the statistics will change. Good luck!"; 
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
            {
                return View("NotReadyTest");
            }
            return View(test);
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        [ActionName("PassTest")]
        public ActionResult PassedTest(PassTestViewModel passTestModel)
        {
            int userId = userRepository.GetByLogin(User.Identity.Name).Id;
            passTestModel.UserId = userId;
            passTestService.PassTest(passTestModel.ToPassTestModel());
            return RedirectToAction("TestResult", "Statistic", new { userId = userId, testId = passTestModel.Id });
        }

        public ActionResult GetImage(int id)
        {
            byte[] image = testRepository.GetById(id).Image;
            if (image == null || image?.Length == 0)
            {
                return null;
            }
            return File(image, "image/jpg");
        }

        #region Methods for admin
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
                int testId = testRepository.GetByName(test.Name).Id;
                return RedirectToAction("Details", "Test", new { id = testId });
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
        #endregion
    }
}