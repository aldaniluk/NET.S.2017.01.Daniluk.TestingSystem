using Domain.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication.Infrastructure.Mappers;
using WebApplication.Models.Statistic;

namespace WebApplication.Controllers
{
    public class StatisticController : Controller
    {
        private readonly IStatisticRepository statisticRepository;

        public StatisticController(IStatisticRepository statisticRepository)
        {
            this.statisticRepository = statisticRepository;
        }

        [HttpGet]
        public ActionResult AllStatistics()
        {
            IEnumerable<StatisticViewModel> statistics = statisticRepository.GetAll().OrderByDescending(s => s.Percentage).Select(s => s.ToStatisticViewModel());
            return View(statistics);
        }
    }
}