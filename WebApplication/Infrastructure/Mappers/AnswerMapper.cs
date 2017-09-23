using Domain.Entities;
using WebApplication.Models.Answer;

namespace WebApplication.Infrastructure.Mappers
{
    public static class AnswerMapper
    {
        public static AnswerViewModel ToAnswerViewModel(this Answer answer)
        {
            return new AnswerViewModel
            {
                Id = answer.Id,
                Explanation = answer.Explanation,
                Text = answer.Text,
                Img = answer.Img,
                QuestionId = answer.QuestionId,
                Right = answer.Right
            };
        }

        public static Answer ToAnswer(this AnswerViewModel answer)
        {
            return new Answer
            {
                Id = answer.Id,
                Explanation = answer.Explanation,
                Text = answer.Text,
                Img = answer.Img,
                QuestionId = answer.QuestionId,
                Right = answer.Right
            };
        }
    }
}