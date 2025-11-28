namespace Personal_Blog.EndPoint.MVC.Models.Home
{
    public class PostListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? ImageUrl { get; set; }
        public string Summary { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
    }
}
