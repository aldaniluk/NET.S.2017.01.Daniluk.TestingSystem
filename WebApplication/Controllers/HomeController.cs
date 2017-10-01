using Domain.Abstract;
using System.Web.Mvc;
using WebApplication.Models.User;
using WebApplication.Infrastructure.Mappers;
using System.Web;

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

        [Authorize]
        public ActionResult Profile()
        {
            UserProfileViewModel user = userRepository.GetByLogin(User.Identity.Name).ToUserProfileViewModel();
            return View(user);
        }
    }
}