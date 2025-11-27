using Personal_Blog.Domain.Core.Comment.Enums;

namespace Personal_Blog.Domain.Core.Comment.DTOs
{
    public class CommentDto
    {
        public string FullName { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public int PostId { get; set; }

    }
}
