using Personal_Blog.Domain.Core.Comment.Enums;
using System.ComponentModel.DataAnnotations;

namespace Personal_Blog.EndPoint.MVC.Models.AuthorPanel
{
    public class CommentModerationViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "نام الزامی است.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "ایمیل الزامی است.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "متن نظر نمیتواند خالی باشد.")]
        public string Text { get; set; }

        [Required(ErrorMessage = "امتیاز نمیتواند خالی باشد.")]
        public int Rating { get; set; }

        public StatusEnum Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public int PostId { get; set; }
        public string PostTitle { get; set; }
    }
}
