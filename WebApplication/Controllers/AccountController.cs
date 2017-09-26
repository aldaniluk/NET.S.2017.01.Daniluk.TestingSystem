using Domain.Abstract;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication.Models.User;
using WebApplication.Providers;

namespace WebApplication.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserRepository userRepository;

        public AccountController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            //var type = HttpContext.User.GetType();
            //var iden = HttpContext.User.Identity.GetType();
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginUserViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(viewModel.Login, viewModel.Password))
                //Проверяет учетные данные пользователя и управляет параметрами пользователей
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Login, viewModel.RememberMe);
                    //Управляет службами проверки подлинности с помощью форм для веб-приложений
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login or password.");
                }
            }
            return View(viewModel);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterUserViewModel viewModel)
        {
            //if (viewModel.Captcha != (string)Session[CaptchaImage.CaptchaValueKey])
            //{
            //    ModelState.AddModelError("Captcha", "Incorrect input.");
            //    return View(viewModel);
            //}

            var anyUser = userRepository.GetAll().Any(u => u.Login.Contains(viewModel.Login));

            if (anyUser)
            {
                ModelState.AddModelError("", "User with this login already registered. Please, choose another.");
                return View(viewModel);
            }

            if (ModelState.IsValid)
            {
                var membershipUser = ((CustomMembershipProvider)Membership.Provider)
                    .CreateUser(viewModel.Login, viewModel.Name, viewModel.Password);

                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Login, false);
                    return RedirectToAction("Profile", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Error registration.");
                }
            }
            return View(viewModel);
        }

        //В сессии создаем случайное число от 1111 до 9999.
        //Создаем в ci объект CatchaImage
        //Очищаем поток вывода
        //Задаем header для mime-типа этого http-ответа будет "image/jpeg" т.е. картинка формата jpeg.
        //Сохраняем bitmap в выходной поток с форматом ImageFormat.Jpeg
        //Освобождаем ресурсы Bitmap
        //Возвращаем null, так как основная информация уже передана в поток вывод
        //[AllowAnonymous]
        //public ActionResult Captcha()
        //{
        //    Session[CaptchaImage.CaptchaValueKey] =
        //        new Random(DateTime.Now.Millisecond).Next(1111, 9999).ToString(CultureInfo.InvariantCulture);
        //    var ci = new CaptchaImage(Session[CaptchaImage.CaptchaValueKey].ToString(), 211, 50, "Helvetica");

        //    // Change the response headers to output a JPEG image.
        //    this.Response.Clear();
        //    this.Response.ContentType = "image/jpeg";

        //    // Write the image to the response stream in JPEG format.
        //    ci.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);

        //    // Dispose of the CAPTCHA image object.
        //    ci.Dispose();
        //    return null;
        //}

        //[ChildActionOnly]
        //public ActionResult LoginPartial()
        //{
        //    return PartialView("_LoginPartial");
        //}
    }
}