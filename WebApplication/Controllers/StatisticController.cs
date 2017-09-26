using Domain.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using WebApplication.Infrastructure.Mappers;
using WebApplication.Models.Statistic;
using WebApplication.Models.Test;

namespace WebApplication.Controllers
{
    [AllowAnonymous]
    public class StatisticController : Controller
    {
        private readonly IStatisticRepository statisticRepository;
        private readonly ITestRepository testRepository;

        public StatisticController(IStatisticRepository statisticRepository, ITestRepository testRepository)
        {
            this.statisticRepository = statisticRepository;
            this.testRepository = testRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<TestViewModel> tests = testRepository.GetAll().Select(t => t.ToTestViewModel()).ToList();
            tests.Insert(0, new TestViewModel { Id = 0, Name = "All" });
            ViewBag.Tests = new SelectList(tests, "Id", "Name");

            //ViewBag.SortType = new SelectList(
            //new List<SelectListItem>
            //{
            //    new SelectListItem { Text = "Percentage", Value = typeof(TestViewModel).GetProperty("Percentage").ToString()},
            //    new SelectListItem { Text = "Date", Value = typeof(TestViewModel).GetProperty("Date").ToString()}
            //}, "Value", "Text");

            ViewBag.SortType = new SelectList(new List<string>()
            {
                "Percentage",
                "Date",
                "Time"
            });

            IEnumerable<StatisticViewModel> statistics = statisticRepository.GetAll().OrderByDescending(s => s.Percentage).Select(s => s.ToStatisticViewModel());
            return View(statistics);
        }

        public ActionResult FilterStatistic(int? testId, string sortType)
        {
            IEnumerable<StatisticViewModel> statictis = statisticRepository.FilterStatistic(testId, sortType)
                .Select(s => s.ToStatisticViewModel());
            return PartialView("_Statistics", statictis);
        }
    }
}