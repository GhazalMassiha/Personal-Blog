using Personal_Blog.Domain.Core.Post.DTOs;

namespace Personal_Blog.Domain.Core.Post.Contracts.ServiceContracts
{
    public interface IPostService
    {
        PostDto? GetById(int postId);
        List<PostDto> GetAll();
        List<PostDto> GetByAuthor(int authorId);
        List<PostDto> GetByCategory(int categoryId);
        bool Create(PostCreateDto dto);
        bool Update(int postId, PostCreateDto dto);
        bool Delete(int postId);
    }
}
