using Domain.Entities;
using System.Linq;
using WebApplication.Models.Question;

namespace WebApplication.Infrastructure.Mappers
{
    public static class QuestionMapper
    {
        public static QuestionViewModel ToQuestionViewModel(this Question question)
        {
            return new QuestionViewModel
            {
                Id = question.Id,
                Img = question.Img,
                Text = question.Text,
                TestId = question.TestId,
                Answers = question.Answers.Select(a => a.ToAnswerViewModel()).ToList()
            };
        }

        public static Question ToQuestion(this QuestionViewModel question)
        {
            return new Question
            {
                Id = question.Id,
                Img = question.Img,
                Text = question.Text,
                TestId = question.TestId,
                Answers = question.Answers?.Select(a => a.ToAnswer()).ToList()
            };
        }
    }
}