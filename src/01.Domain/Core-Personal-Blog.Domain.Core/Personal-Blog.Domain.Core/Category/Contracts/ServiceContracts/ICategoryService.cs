using Personal_Blog.Domain.Core.Category.DTOs;

namespace Personal_Blog.Domain.Core.Category.Contracts.ServiceContracts
{
    public interface ICategoryService 
    {
        CategoryDto? GetById(int categoryId);
        List<CategoryDto> GetByAuthor(int authorId);
        bool Create(CategoryCreateDto dto);
        bool Update(int categoryId, CategoryCreateDto dto);
        bool Delete(int categoryId);
    }
}
