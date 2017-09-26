using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.User
{
    public class RegisterUserViewModel
    {
        [Display(Name = "Enter your login")]
        [StringLength(50, ErrorMessage = "The field can not be empty!")]
        public string Login { get; set; }

        [Display(Name = "Enter your name")]
        [StringLength(50, ErrorMessage = "The field can not be empty!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        [StringLength(50, ErrorMessage = "The password must contain at least {2} characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Enter your password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm the password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm the password")]
        [Compare("Password", ErrorMessage = "Passwords must match")]
        public string ConfirmPassword { get; set; }

        //[Required]
        //[Display(Name = "Enter the code from the image")]
        //public string Captcha { get; set; }
    }
}