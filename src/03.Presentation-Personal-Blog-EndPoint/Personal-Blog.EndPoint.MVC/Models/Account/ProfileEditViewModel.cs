using System.ComponentModel.DataAnnotations;

namespace Personal_Blog.EndPoint.MVC.Models.Account
{
    public class ProfileEditViewModel
    {
        [Required(ErrorMessage = "نام کاربری الزامی است.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "رمز عبور الرامی است.")]
        public string Password { get; set; }

        [Required(ErrorMessage = ".ایمیل الزامی است")]
        public string Email { get; set; }

        [Required(ErrorMessage = "نام الزامی است.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "نام خانوادگی الزامی است.")]
        public string LastName { get; set; }

        public string? ImageUrl { get; set; }
    }
}
