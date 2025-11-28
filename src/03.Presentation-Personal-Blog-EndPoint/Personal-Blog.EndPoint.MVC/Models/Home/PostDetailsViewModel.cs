using Personal_Blog.Domain.Core.Comment.DTOs;
using Personal_Blog.Domain.Core.Post.DTOs;

namespace Personal_Blog.EndPoint.MVC.Models.Home
{
    public class PostDetailsViewModel
    {
        public PostDto Post { get; set; }
        public List<CommentDto> Comments { get; set; }
        public CommentCreateDto NewComment { get; set; }
    }
}
