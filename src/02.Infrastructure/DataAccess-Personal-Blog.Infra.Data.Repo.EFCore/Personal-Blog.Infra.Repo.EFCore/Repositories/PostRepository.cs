using Microsoft.EntityFrameworkCore;
using Personal_Blog.Domain.Core.Post.Contracts.RepositoryContracts;
using Personal_Blog.Domain.Core.Post.DTOs;
using Personal_Blog.Domain.Core.Post.Entities;
using Personal_Blog.Infra.SqlServer.EFCore.Persistence;

namespace Personal_Blog.Infra.Repo.EFCore.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _context;
        public PostRepository(AppDbContext context) => _context = context;

        public PostDto? GetById(int postId)
        {
            var p = _context.Posts.Find(postId);
            if (p == null) return null;
            return new PostDto
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                ImageUrl = p.ImageUrl,
                CreatedAt = p.CreatedAt,
                AuthorId = p.AuthorId,
                CategoryId = p.CategoryId
            };
        }

        public List<PostDto> GetAll()
        {
            return _context.Posts
                .Select(p => new PostDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                    ImageUrl = p.ImageUrl,
                    CreatedAt = p.CreatedAt,
                    AuthorId = p.AuthorId,
                    CategoryId = p.CategoryId
                })
                .ToList();
        }

        public List<PostDto> GetByAuthor(int authorId)
        {
            return _context.Posts
                .Where(p => p.AuthorId == authorId)
                .Select(p => new PostDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                    ImageUrl = p.ImageUrl,
                    CreatedAt = p.CreatedAt,
                    AuthorId = p.AuthorId,
                    CategoryId = p.CategoryId
                })
                .ToList();
        }

        public List<PostDto> GetByCategory(int categoryId)
        {
            return _context.Posts
                .Where(p => p.CategoryId == categoryId)
                .Select(p => new PostDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                    ImageUrl = p.ImageUrl,
                    CreatedAt = p.CreatedAt,
                    AuthorId = p.AuthorId,
                    CategoryId = p.CategoryId
                })
                .ToList();
        }

        public bool Create(PostCreateDto dto)
        {
            var post = new Post
            {
                Title = dto.Title,
                Content = dto.Content,
                ImageUrl = dto.ImageUrl,
                AuthorId = dto.AuthorId,
                CategoryId = dto.CategoryId
            };

            _context.Posts.Add(post);
            return _context.SaveChanges() > 0;
        }

        public bool Update(int postId, PostCreateDto dto)
        {
            var affectedRows = _context.Posts
                .Where(p => p.Id == postId)
                .ExecuteUpdate(setter => setter
                     .SetProperty(p => p.Title, dto.Title)
                     .SetProperty(p => p.Content, dto.Content)
                     .SetProperty(p => p.ImageUrl, dto.ImageUrl)
                     .SetProperty(p => p.CategoryId, dto.CategoryId)
                );

            return affectedRows > 0;
        }

        public bool Delete(int postId)
        {
            var result = _context.Posts.Where(c => c.Id == postId).ExecuteDelete();

            return result > 0;
        }
    }
}
