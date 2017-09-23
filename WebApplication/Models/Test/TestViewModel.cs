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
        [StringLength(50, MinimumLength = 3)]
        [DisplayName("Test name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description cannot be empty.")]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Minimal percentage cannot be empty.")]
        [Range(0, 100, ErrorMessage = "Minimal percentage must be greater than 0 and less than 100.")]
        [DisplayName("Minimal percentage")]
        public double MinPercentage { get; set; }

        //[Remote("ValidateTest", "Test")]
        public bool IsReady { get; set; }

        //[Required]
        public List<QuestionViewModel> Questions { get; set; }

    }
}