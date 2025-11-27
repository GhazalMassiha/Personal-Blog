using Personal_Blog.Domain.Core._common;
using Personal_Blog.Domain.Core.Comment.DTOs;

namespace Personal_Blog.Domain.Core.Comment.Contracts.AppServiceContracts
{
    public interface ICommentAppService
    {
        Result<CommentDto> GetById(int commentId);
        Result<List<CommentDto>> GetByPost(int postId);
        Result<List<CommentDto>> GetAll();
        Result<bool> Create(CommentCreateDto dto);
        Result<bool> Confirm(int commentId);
        Result<bool> Reject(int commentId);
        Result<bool> Delete(int commentId);
    }
}
