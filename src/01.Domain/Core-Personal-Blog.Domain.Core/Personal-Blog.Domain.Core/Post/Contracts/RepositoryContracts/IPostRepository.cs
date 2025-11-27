using Personal_Blog.Domain.Core.Post.DTOs;

namespace Personal_Blog.Domain.Core.Post.Contracts.RepositoryContracts
{
    public interface IPostRepository
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
