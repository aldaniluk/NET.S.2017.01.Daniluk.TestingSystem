using System.Web.Mvc;

namespace WebApplication.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        public ActionResult Oops()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }
    }
}