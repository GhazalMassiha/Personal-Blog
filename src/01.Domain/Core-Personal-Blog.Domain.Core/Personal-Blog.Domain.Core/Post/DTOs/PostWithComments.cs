namespace Personal_Blog.Domain.Core.Post.DTOs
{
    public class PostWithComments
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public List<Comment.Entities.Comment>? Comments { get; set; }
    }
}
