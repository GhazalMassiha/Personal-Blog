using System.ComponentModel.DataAnnotations;

namespace Personal_Blog.EndPoint.MVC.Models.AuthorPanel
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "نام دسته بندی الزامی است.")]
        public string Name { get; set; }
    }
}
