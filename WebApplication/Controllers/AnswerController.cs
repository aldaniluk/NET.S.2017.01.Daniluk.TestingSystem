using Domain.Abstract;
using System.Web.Mvc;
using WebApplication.Infrastructure.Mappers;
using WebApplication.Models.Answer;

namespace WebApplication.Controllers
{
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

        [ActionName("Create")]
        [HttpPost]
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

        [ActionName("Edit")]
        [HttpPost]
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

        [ActionName("Delete")]
        [HttpPost]
        public ActionResult Deleted(AnswerViewModel answer)
        {
            int questionId = answerRepository.GetById(answer.Id).QuestionId;
            answerRepository.Delete(answer.ToAnswer());
            return RedirectToAction("Details", "Question", new { id = questionId });
        }

    }
}