using System.ComponentModel.DataAnnotations;

namespace Personal_Blog.EndPoint.MVC.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "نام کاربری الزامی است.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "رمز عبور الرامی است.")]
        public string Password { get; set; }
    }
}
