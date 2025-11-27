using Personal_Blog.Domain.Core._common;
using Personal_Blog.Domain.Core.Category.DTOs;

namespace Personal_Blog.Domain.Core.Category.Contracts.AppServiceContracts
{
    public interface ICategoryAppService
    {
        Result<CategoryDto> GetById(int categoryId);
        Result<List<CategoryDto>> GetByAuthor(int authorId);
        Result<bool> Create(CategoryCreateDto dto);
        Result<bool> Update(int categoryId, CategoryCreateDto dto);
        Result<bool> Delete(int categoryId);
    }
}
