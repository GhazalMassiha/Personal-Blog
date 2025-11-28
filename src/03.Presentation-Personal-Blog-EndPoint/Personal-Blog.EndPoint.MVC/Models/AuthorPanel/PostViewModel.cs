using Microsoft.AspNetCore.Mvc.Rendering;

namespace Personal_Blog.EndPoint.MVC.Models.AuthorPanel
{
    public class PostViewModel
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
