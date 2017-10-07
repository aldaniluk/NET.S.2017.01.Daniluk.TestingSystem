using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WebApplication.Infrastructure.Attributes;
using WebApplication.Models.Question;

namespace WebApplication.Models.Test
{
    [ValidTest]
    public class TestViewModel 
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name cannot be empty.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Test name must be greater than 3 characters and less than 50.")]
        [DisplayName("Test name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description cannot be empty.")]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Minimal percentage cannot be empty.")]
        [Range(0, 100, ErrorMessage = "Minimal percentage must be greater than 0 and less than 100.")]
        [DisplayName("Minimal percentage")]
        public double MinPercentage { get; set; }

        [DisplayName("Is this test ready?")]
        public bool IsReady { get; set; }

        [DisplayName("Image of the test")]
        public byte[] Img { get; set; }

        public byte[] ImgSmall { get; set; }

        public List<QuestionViewModel> Questions { get; set; }

    }
}