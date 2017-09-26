using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.User
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "The field can not be empty!")]
        [Display(Name = "Enter your login")]
        [StringLength(50)]
        public string Login { get; set; }

        [Required(ErrorMessage = "The field can not be empty!")]
        [DataType(DataType.Password)]
        [Display(Name = "Enter your password")]
        [StringLength(50)]
        public string Password { get; set; }

        [Display(Name = "Remember password?")]
        public bool RememberMe { get; set; }
    }
}