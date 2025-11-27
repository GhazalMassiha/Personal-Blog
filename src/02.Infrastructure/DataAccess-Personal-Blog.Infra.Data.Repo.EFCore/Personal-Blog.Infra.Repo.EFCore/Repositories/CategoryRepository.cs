using Microsoft.EntityFrameworkCore;
using Personal_Blog.Domain.Core.Category.Contracts.RepositoryContracts;
using Personal_Blog.Domain.Core.Category.DTOs;
using Personal_Blog.Domain.Core.Category.Entities;
using Personal_Blog.Infra.SqlServer.EFCore.Persistence;

namespace Personal_Blog.Infra.Repo.EFCore.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context) => _context = context;

        public CategoryDto? GetById(int categoryId)
        {
            return _context.Categories
                .Where(c => c.Id == categoryId)
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .FirstOrDefault();
        }

        public List<CategoryDto> GetByAuthor(int authorId)
        {
            return _context.Categories
                .Where(c => c.AuthorId == authorId)
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();
        }

        public bool Create(CategoryCreateDto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                AuthorId = dto.AuthorId
            };

            _context.Categories.Add(category);
            return _context.SaveChanges() > 0;
        }

        public bool Update(int categoryId, CategoryCreateDto dto)
        {
            var affected = _context.Categories
                .Where(c => c.Id == categoryId)
                .ExecuteUpdate(b => b
                    .SetProperty(c => c.Name, dto.Name)
                );

            return affected > 0;
        }

        public bool Delete(int categoryId)
        {
            var result = _context.Categories.Where(c => c.Id == categoryId).ExecuteDelete();

            return result > 0;
        }
    }
}
