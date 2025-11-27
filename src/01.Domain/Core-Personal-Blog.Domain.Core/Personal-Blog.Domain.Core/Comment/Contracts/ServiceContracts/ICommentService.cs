using Personal_Blog.Domain.Core.Comment.DTOs;

namespace Personal_Blog.Domain.Core.Comment.Contracts.ServiceContracts
{
    public interface ICommentService
    {
        public CommentDto? GetById(int commentId);
        public List<CommentDto> GetCommentByPostId(int postId);
        public List<CommentDto> GetAll();
        public bool Create(CommentCreateDto commentCreateDto);
        public bool ChangeStatusToConfirmed(int commentId);
        public bool ChangeStatusToRejected(int commentId);
        public bool Delete(int commentId);
    }
}
