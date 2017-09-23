using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using WebApplication.Models.Answer;

namespace WebApplication.Models.Question
{
    public class QuestionViewModel
    {
        public int Id { get; set; }

        [DisplayName("Question text")]
        public string Text { get; set; }

        //public HttpPostedFileBase ImgFile { get; set; }

        [DisplayName("Question image")]
        public byte[] Img { get; set; }

        public int TestId { get; set; }
        public List<AnswerViewModel> Answers { get; set; }
    }
}