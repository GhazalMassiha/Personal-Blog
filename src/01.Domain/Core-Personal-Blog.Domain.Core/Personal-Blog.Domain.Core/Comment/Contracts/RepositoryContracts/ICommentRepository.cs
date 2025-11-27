using Personal_Blog.Domain.Core.Comment.DTOs;
using Personal_Blog.Domain.Core.Comment.Enums;

namespace Personal_Blog.Domain.Core.Comment.Contracts.RepositoryContracts
{
    public interface ICommentRepository
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
