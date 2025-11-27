namespace Personal_Blog.Domain.Core.Post.DTOs
{
    public class PostDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
    }
}
