using Domain.Abstract;
using System.Web.Mvc;
using WebApplication.Models.Question;
using WebApplication.Infrastructure.Mappers;
using System.Web;

namespace WebApplication.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionRepository questionRepository;

        public QuestionController(IQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Details(int id)
        {
            QuestionViewModel question = questionRepository.GetById(id).ToQuestionViewModel();
            return View("DetailsQuestion", question);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Create(int testId)
        {
            ViewBag.TestId = testId;
            return View("CreateQuestion");
        }

        [HttpPost]
        [ActionName("Create")]
        [Authorize(Roles = "admin")]
        public ActionResult Created(QuestionViewModel question, HttpPostedFileBase file)
        {
            if (file != null && file?.ContentLength != 0)
            {
                question.Img = new byte[file.ContentLength];
                file.InputStream.Read(question.Img, 0, file.ContentLength);
            }
            if (ModelState.IsValid)
            {
                questionRepository.Create(question.ToQuestion());
                return RedirectToAction("Details", "Test", new { id = question.TestId });
            }
            return View("CreateQuestion");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            QuestionViewModel question = questionRepository.GetById(id).ToQuestionViewModel();
            return View("EditQuestion", question);
        }

        [HttpPost]
        [ActionName("Edit")]
        [Authorize(Roles = "admin")]
        public ActionResult Edited(QuestionViewModel question, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file?.ContentLength != 0)
                {
                    question.Img = new byte[file.ContentLength];
                    file.InputStream.Read(question.Img, 0, file.ContentLength);
                }
                questionRepository.Update(question.ToQuestion());
                return RedirectToAction("Details", "Question", new { id = question.Id });
            }
            return View("EditQuestion", question);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            QuestionViewModel question = questionRepository.GetById(id).ToQuestionViewModel();
            return View("DeleteQuestion", question);
        }

        [HttpPost]
        [ActionName("Delete")]
        [Authorize(Roles = "admin")]
        public ActionResult Deleted(QuestionViewModel question)
        {
            int testId = questionRepository.GetById(question.Id).TestId;
            questionRepository.Delete(question.ToQuestion());
            return RedirectToAction("Details", "Test", new { id = testId });
        }

        [Authorize]
        public ActionResult GetImage(int id)
        {
            byte[] image = questionRepository.GetById(id).Img;
            if (image == null || image?.Length == 0)
            {
                return null;
            }
            return File(image, "image/jpg");
        }
    }
}