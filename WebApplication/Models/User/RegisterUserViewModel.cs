using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebApplication.Models.User
{
    public class RegisterUserViewModel
    {
        [Display(Name = "Enter your login")]
        [Required(ErrorMessage = "The field can not be empty!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Login must be greater than 3 characters and least than 50.")]
        [Remote("ValidLogIn", "Account", ErrorMessage = "This login is used. Please, select another.")]
        public string Login { get; set; }

        [Display(Name = "Enter your name")]
        [Required(ErrorMessage = "The field can not be empty!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be greater than 3 characters and least than 50.")]
        public string Name { get; set; }

        [Display(Name = "Enter your password")]
        [Required(ErrorMessage = "Enter your password")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be greater than 6 characters and least than 50.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm the password")]
        [Required(ErrorMessage = "Confirm the password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Passwords must match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}