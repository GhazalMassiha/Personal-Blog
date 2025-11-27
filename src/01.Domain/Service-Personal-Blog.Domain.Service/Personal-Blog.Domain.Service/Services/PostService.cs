using Personal_Blog.Domain.Core.Comment.Contracts.RepositoryContracts;
using Personal_Blog.Domain.Core.Post.Contracts.RepositoryContracts;
using Personal_Blog.Domain.Core.Post.Contracts.ServiceContracts;
using Personal_Blog.Domain.Core.Post.DTOs;

namespace Personal_Blog.Domain.Service.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        public PostService(IPostRepository postRepository) => _postRepository = postRepository;

        public bool Create(PostCreateDto dto)
        {
            return _postRepository.Create(dto);
        }

        public bool Delete(int postId)
        {
            return _postRepository.Delete(postId);
        }

        public List<PostDto> GetAll()
        {
            return _postRepository.GetAll();
        }

        public List<PostDto> GetByAuthor(int authorId)
        {
            return _postRepository.GetByAuthor(authorId);
        }

        public List<PostDto> GetByCategory(int categoryId)
        {
            return _postRepository.GetByCategory(categoryId);
        }

        public PostDto? GetById(int postId)
        {
            return _postRepository.GetById(postId);
        }

        public bool Update(int postId, PostCreateDto dto)
        {
            return _postRepository.Update(postId, dto);
        }
    }
}
