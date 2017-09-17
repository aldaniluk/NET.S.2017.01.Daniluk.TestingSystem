using System.Collections.Generic;
using WebApplication.Models.Answer;

namespace WebApplication.Models.Question
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public byte[] Img { get; set; }
        public int? TestId { get; set; }
        public virtual ICollection<AnswerViewModel> Answers { get; set; }
    }
}