namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Answer
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public byte[] Img { get; set; }

        public bool Right { get; set; }

        public string Explanation { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}
