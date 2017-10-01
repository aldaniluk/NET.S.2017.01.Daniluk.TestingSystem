using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using WebApplication.Models.Answer;

namespace WebApplication.Models.Question
{
    public class QuestionViewModel
    {
        public int Id { get; set; }

        [DisplayName("Question text")]
        [Required(ErrorMessage = "Text cannot be empty.")]
        public string Text { get; set; }

        [DisplayName("Question image")]
        public byte[] Img { get; set; }

        public int TestId { get; set; }

        public List<AnswerViewModel> Answers { get; set; }
    }
}