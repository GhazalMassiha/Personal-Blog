using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Personal_Blog.EndPoint.MVC.Models.AuthorPanel
{
    public class PostViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "عنوان پست الزامی است.")] 
        public string Title { get; set; }

        [Required(ErrorMessage = "متن پست الزامی است.")]
        public string Content { get; set; }

        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "دسته‌بندی انتخاب‌شده معتبر نیست.")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem>? Categories { get; set; }
    }
}
