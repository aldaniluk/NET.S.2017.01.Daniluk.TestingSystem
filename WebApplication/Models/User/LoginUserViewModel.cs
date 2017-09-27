using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebApplication.Models.User
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "The field can not be empty!")]
        [Display(Name = "Enter your login")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Login must be greater than 3 characters and least than 50.")]
        [Remote("ValidLogUp", "Account", ErrorMessage = "This login is not registered in our system.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "The field can not be empty!")]
        [DataType(DataType.Password)]
        [Display(Name = "Enter your password")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Password must be greater than 3 characters and least than 50.")]
        public string Password { get; set; }

        [Display(Name = "Remember password?")]
        public bool RememberMe { get; set; }
    }
}