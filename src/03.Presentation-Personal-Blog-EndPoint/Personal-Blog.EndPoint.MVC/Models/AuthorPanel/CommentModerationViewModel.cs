using Personal_Blog.Domain.Core.Comment.Enums;

namespace Personal_Blog.EndPoint.MVC.Models.AuthorPanel
{
    public class CommentModerationViewModel
    {
        public int CommentId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public StatusEnum Status { get; set; }
        public int PostId { get; set; }
        public string PostTitle { get; set; }
    }
}
