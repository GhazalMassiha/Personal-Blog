using Personal_Blog.Domain.Core.Author.Contracts.RepositoryContracts;
using Personal_Blog.Domain.Core.Category.Contracts.RepositoryContracts;
using Personal_Blog.Domain.Core.Category.Contracts.ServiceContracts;
using Personal_Blog.Domain.Core.Category.DTOs;

namespace Personal_Blog.Domain.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository) => _categoryRepository = categoryRepository;

        public bool Create(CategoryCreateDto dto)
        {
            return _categoryRepository.Create(dto);
        }

        public bool Delete(int categoryId)
        {
            return _categoryRepository.Delete(categoryId);
        }

        public List<CategoryDto> GetByAuthor(int authorId)
        {
            return _categoryRepository.GetByAuthor(authorId);
        }

        public CategoryDto? GetById(int categoryId)
        {
            return _categoryRepository.GetById(categoryId);
        }

        public bool Update(int categoryId, CategoryCreateDto dto)
        {
            return _categoryRepository.Update(categoryId, dto);
        }
    }
}
