using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.Answer
{
    public class AnswerViewModel
    {
        public int Id { get; set; }

        [DisplayName("Answer text")]
        [Required(ErrorMessage = "Text cannot be empty.")]
        public string Text { get; set; }

        [DisplayName("Answer image")]
        public byte[] Img { get; set; }

        [DisplayName("Is answer right")]
        public bool Right { get; set; }

        [DisplayName("Answer explantion")]
        [Required(ErrorMessage = "Explanation cannot be empty.")]
        public string Explanation { get; set; }

        public int QuestionId { get; set; }
    }
}