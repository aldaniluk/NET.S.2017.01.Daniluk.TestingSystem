using Domain.Abstract;
using System.Web.Mvc;
using WebApplication.Infrastructure.Mappers;
using WebApplication.Models.Answer;

namespace WebApplication.Controllers
{
    [Authorize(Roles = "admin")]
    public class AnswerController : Controller
    {
        private readonly IAnswerRepository answerRepository;

        public AnswerController(IAnswerRepository answerRepository)
        {
            this.answerRepository = answerRepository;
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            AnswerViewModel answer = answerRepository.GetById(id).ToAnswerViewModel();
            return View("DetailsAnswer", answer);
        }

        [HttpGet]
        public ActionResult Create(int questionId)
        {
            ViewBag.QuestionId = questionId;
            return View("CreateAnswer");
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult Created(AnswerViewModel answer)
        {
            if (ModelState.IsValid)
            {
                answerRepository.Create(answer.ToAnswer());
                return RedirectToAction("Details", "Question", new { id = answer.QuestionId });
            }
            return View("CreateAnswer", answer);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            AnswerViewModel answer = answerRepository.GetById(id).ToAnswerViewModel();
            return View("EditAnswer", answer);
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edited(AnswerViewModel answer)
        {
            if (ModelState.IsValid)
            {
                answerRepository.Update(answer.ToAnswer());
                return RedirectToAction("Details", "Answer", new { id = answer.Id });
            }
            return View("EditAnswer");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            AnswerViewModel answer = answerRepository.GetById(id).ToAnswerViewModel();
            return View("DeleteAnswer", answer);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Deleted(AnswerViewModel answer)
        {
            int questionId = answerRepository.GetById(answer.Id).QuestionId;
            answerRepository.Delete(answer.ToAnswer());
            return RedirectToAction("Details", "Question", new { id = questionId });
        }

    }
}