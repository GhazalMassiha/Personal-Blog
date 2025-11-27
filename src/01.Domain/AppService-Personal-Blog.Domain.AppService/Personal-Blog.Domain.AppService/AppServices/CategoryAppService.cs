using Personal_Blog.Domain.Core._common;
using Personal_Blog.Domain.Core.Category.Contracts.AppServiceContracts;
using Personal_Blog.Domain.Core.Category.Contracts.RepositoryContracts;
using Personal_Blog.Domain.Core.Category.Contracts.ServiceContracts;
using Personal_Blog.Domain.Core.Category.DTOs;

namespace Personal_Blog.Domain.AppService.AppServices
{
    public class CategoryAppService : ICategoryAppService
    {
        private readonly ICategoryService _categoryService;
        public CategoryAppService(ICategoryService categoryService) => _categoryService = categoryService;

        public Result<CategoryDto> GetById(int categoryId)
        {
            var cat = _categoryService.GetById(categoryId);
            if (cat == null)
                return Result<CategoryDto>.Failure("دسته‌بندی یافت نشد.");

            return Result<CategoryDto>.Success("دسته‌بندی دریافت شد.", cat);
        }

        public Result<List<CategoryDto>> GetByAuthor(int authorId)
        {
            var list = _categoryService.GetByAuthor(authorId);

            return Result<List<CategoryDto>>.Success("دسته‌بندی‌های نویسنده دریافت شدند.", list);
        }

        public Result<bool> Create(CategoryCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return Result<bool>.Failure("نام دسته‌بندی نمی‌تواند خالی باشد.");

            bool ok = _categoryService.Create(dto);
            if (!ok)
                return Result<bool>.Failure("خطا در ایجاد دسته‌بندی.");

            return Result<bool>.Success("دسته‌بندی با موفقیت ایجاد شد.", true);
        }

        public Result<bool> Update(int categoryId, CategoryCreateDto dto)
        {
            var existing = _categoryService.GetById(categoryId);
            if (existing == null)
                return Result<bool>.Failure("دسته‌بندی موردنظر یافت نشد.");

            bool ok = _categoryService.Update(categoryId, dto);
            if (!ok)
                return Result<bool>.Failure("خطا در ویرایش دسته‌بندی.");

            return Result<bool>.Success("دسته‌بندی با موفقیت بروزرسانی شد.", true);
        }

        public Result<bool> Delete(int categoryId)
        {
            var existing = _categoryService.GetById(categoryId);
            if (existing == null)
                return Result<bool>.Failure("دسته‌بندی یافت نشد.");

            bool ok = _categoryService.Delete(categoryId);
            if (!ok)
                return Result<bool>.Failure("خطا در حذف دسته‌بندی.");

            return Result<bool>.Success("دسته‌بندی با موفقیت حذف شد.", true);
        }
    }
}
