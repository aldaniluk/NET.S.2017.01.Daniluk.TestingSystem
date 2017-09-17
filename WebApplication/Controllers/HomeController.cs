using Domain.Abstract;
using System.Web.Mvc;
using WebApplication.Models.User;
using WebApplication.Infrastructure.Mappers;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepository userRepository;

        public HomeController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        //[Authorize]
        public ActionResult MyProfile()
        {
            UserProfileViewModel user = userRepository.GetById(2).ToUserProfileViewModel();
            return View(user);
        }
    }
}