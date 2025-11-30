using Personal_Blog.Domain.Core.Comment.Enums;

namespace Personal_Blog.Domain.Core.Comment.DTOs
{
    public class CommentCreateDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public StatusEnum Status { get; set; } = StatusEnum.Pending;
        public int PostId { get; set; }
    }
}
