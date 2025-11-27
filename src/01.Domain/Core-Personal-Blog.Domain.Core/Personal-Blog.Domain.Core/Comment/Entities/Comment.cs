using Personal_Blog.Domain.Core._common;
using Personal_Blog.Domain.Core.Comment.Enums;

namespace Personal_Blog.Domain.Core.Comment.Entities
{
    public class Comment : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public StatusEnum Status { get; set; }

        public int PostId { get; set; }
        public Post.Entities.Post Post { get; set; }
    }
}
