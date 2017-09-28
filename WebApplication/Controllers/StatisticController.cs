using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public ActionResult Index()
        {
            List<TestViewModel> tests = testRepository.GetAllReady().Select(t => t.ToTestViewModel()).ToList();
            tests.Insert(0, new TestViewModel { Id = 0, Name = "All" });
            ViewBag.Tests = new SelectList(tests, "Id", "Name");

            ViewBag.SortType = new SelectList((StatisticSortType[])Enum.GetValues(typeof(StatisticSortType)));

            IEnumerable<StatisticViewModel> statistics = statisticRepository.FilterStatistic(null, StatisticSortType.Percentage)
                .Select(s => s.ToStatisticViewModel());
            return View(statistics);
        }

        public ActionResult FilterStatistic(int? testId, StatisticSortType sortType)
        {
            IEnumerable<StatisticViewModel> statictics = statisticRepository.FilterStatistic(testId, sortType)
                .Select(s => s.ToStatisticViewModel());
            return PartialView("_Statistics", statictics);
        }
    }
}