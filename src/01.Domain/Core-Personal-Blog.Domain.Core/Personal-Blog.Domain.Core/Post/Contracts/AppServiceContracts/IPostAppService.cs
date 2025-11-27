using Personal_Blog.Domain.Core._common;
using Personal_Blog.Domain.Core.Post.DTOs;

namespace Personal_Blog.Domain.Core.Post.Contracts.AppServiceContracts
{
    public interface IPostAppService
    {
        Result<PostDto> GetById(int postId);
        Result<List<PostDto>> GetAll();
        Result<List<PostDto>> GetByAuthor(int authorId);
        Result<List<PostDto>> GetByCategory(int categoryId);
        Result<bool> Create(PostCreateDto dto);
        Result<bool> Update(int postId, PostCreateDto dto);
        Result<bool> Delete(int postId);
    }
}
