using Domain.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication.Models.Question;
using WebApplication.Infrastructure.Mappers;
using System.Web;
using System.IO;

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
        public ActionResult Create(int testId)
        {
            ViewBag.TestId = testId;
            return View("CreateQuestion");
        }

        [ActionName("Create")]
        [HttpPost]
        public ActionResult Created(QuestionViewModel question, HttpPostedFileBase file)
        {
            if (file != null && file?.ContentLength != 0)
            {
                using (var binaryReader = new BinaryReader(file.InputStream))
                {
                    question.Img = binaryReader.ReadBytes(file.ContentLength);
                }
            }
            if (ModelState.IsValid)
            {
                questionRepository.Create(question.ToQuestion());
                return RedirectToAction("Details", "Test", new { id = question.TestId });
            }
            return View("CreateQuestion");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            QuestionViewModel question = questionRepository.GetById(id).ToQuestionViewModel();
            return View("DetailsQuestion", question);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            QuestionViewModel question = questionRepository.GetById(id).ToQuestionViewModel();
            return View("EditQuestion", question);
        }

        [ActionName("Edit")]
        [HttpPost]
        public ActionResult Edited(QuestionViewModel question, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file?.ContentLength != 0)
                {
                    using (var binaryReader = new BinaryReader(file.InputStream))
                    {
                        question.Img = binaryReader.ReadBytes(file.ContentLength);
                    }
                }
                questionRepository.Update(question.ToQuestion());
                return RedirectToAction("Details", "Question", new { id = question.Id });
            }
            return View("EditQuestion", question);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            QuestionViewModel question = questionRepository.GetById(id).ToQuestionViewModel();
            return View("DeleteQuestion", question);
        }

        [ActionName("Delete")]
        [HttpPost]
        public ActionResult Deleted(QuestionViewModel question)
        {
            int testId = questionRepository.GetById(question.Id).TestId;
            questionRepository.Delete(question.ToQuestion());
            return RedirectToAction("Details", "Test", new { id = testId });
        }

        public ActionResult GetImage(int id)
        {
            byte[] imagedata = questionRepository.GetById(id).Img;
            if (imagedata == null || imagedata?.Length == 0)
            {
                return null;
            }
            return File(imagedata, "image/jpg");
        }
    }
}