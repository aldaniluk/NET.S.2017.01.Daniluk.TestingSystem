namespace WebApplication.Models.Answer
{
    public class AnswerViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public byte[] Img { get; set; }
        public bool Right { get; set; }
        public string Explanation { get; set; }
        public int QuestionId { get; set; }
    }
}