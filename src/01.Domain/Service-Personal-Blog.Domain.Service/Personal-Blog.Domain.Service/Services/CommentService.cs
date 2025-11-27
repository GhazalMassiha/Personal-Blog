using Personal_Blog.Domain.Core.Category.Contracts.RepositoryContracts;
using Personal_Blog.Domain.Core.Comment.Contracts.RepositoryContracts;
using Personal_Blog.Domain.Core.Comment.Contracts.ServiceContracts;
using Personal_Blog.Domain.Core.Comment.DTOs;

namespace Personal_Blog.Domain.Service.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        public CommentService(ICommentRepository commentRepository) => _commentRepository = commentRepository;

        public bool ChangeStatusToConfirmed(int commentId)
        {
            return _commentRepository.ChangeStatusToConfirmed(commentId);
        }

        public bool ChangeStatusToRejected(int commentId)
        {
            return _commentRepository.ChangeStatusToRejected(commentId);
        }

        public bool Create(CommentCreateDto commentCreateDto)
        {
            return _commentRepository.Create(commentCreateDto);
        }

        public bool Delete(int commentId)
        {
            return _commentRepository.Delete(commentId);
        }

        public List<CommentDto> GetAll()
        {
            return _commentRepository.GetAll();
        }

        public CommentDto? GetById(int commentId)
        {
            return _commentRepository.GetById(commentId);
        }

        public List<CommentDto> GetCommentByPostId(int postId)
        {
            return _commentRepository.GetCommentByPostId(postId);
        }
    }
}
