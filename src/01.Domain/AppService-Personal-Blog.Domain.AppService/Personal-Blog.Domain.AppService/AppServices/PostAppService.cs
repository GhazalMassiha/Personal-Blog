using Personal_Blog.Domain.Core._common;
using Personal_Blog.Domain.Core.Author.Contracts.RepositoryContracts;
using Personal_Blog.Domain.Core.Author.Contracts.ServiceContracts;
using Personal_Blog.Domain.Core.Category.Contracts.RepositoryContracts;
using Personal_Blog.Domain.Core.Category.Contracts.ServiceContracts;
using Personal_Blog.Domain.Core.Post.Contracts.AppServiceContracts;
using Personal_Blog.Domain.Core.Post.Contracts.RepositoryContracts;
using Personal_Blog.Domain.Core.Post.Contracts.ServiceContracts;
using Personal_Blog.Domain.Core.Post.DTOs;

namespace Personal_Blog.Domain.AppService.AppServices
{
    public class PostAppService : IPostAppService
    {
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        private readonly IAuthorService _authorService;

        public PostAppService(IPostService postService,
                           ICategoryService categoryService,
                           IAuthorService authorService)
        {
            _postService = postService;
            _categoryService = categoryService;
            _authorService = authorService;
        }

        public Result<PostDto> GetById(int postId)
        {
            var dto = _postService.GetById(postId);
            if (dto == null)
                return Result<PostDto>.Failure("پست یافت نشد.");

            return Result<PostDto>.Success("پست دریافت شد.", dto);
        }

        public Result<List<PostDto>> GetAll()
        {
            var list = _postService.GetAll();

            return Result<List<PostDto>>.Success("تمام پست‌ها دریافت شدند.", list);
        }

        public Result<List<PostDto>> GetByAuthor(int authorId)
        {
            var list = _postService.GetByAuthor(authorId);

            return Result<List<PostDto>>.Success("پست‌های نویسنده دریافت شدند.", list);
        }

        public Result<List<PostDto>> GetByCategory(int categoryId)
        {
            var list = _postService.GetByCategory(categoryId);

            return Result<List<PostDto>>.Success("پست‌های دسته‌بندی دریافت شدند.", list);
        }

        public Result<bool> Create(PostCreateDto dto)
        {
            if (_authorService.GetById(dto.AuthorId) == null)
                return Result<bool>.Failure("نویسنده وجود ندارد.");

            if (_categoryService.GetById(dto.CategoryId) == null)
                return Result<bool>.Failure("دسته‌بندی انتخاب شده معتبر نیست.");

            bool ok = _postService.Create(dto);
            if (!ok)
                return Result<bool>.Failure("خطا در ایجاد پست.");

            return Result<bool>.Success("پست با موفقیت ایجاد شد.", true);
        }

        public Result<bool> Update(int postId, PostCreateDto dto)
        {
            var existing = _postService.GetById(postId);
            if (existing == null)
                return Result<bool>.Failure("پست موردنظر یافت نشد.");

            if (_categoryService.GetById(dto.CategoryId) == null)
                return Result<bool>.Failure("دسته‌بندی انتخاب‌شده معتبر نیست.");

            bool ok = _postService.Update(postId, dto);
            if (!ok)
                return Result<bool>.Failure("خطا در بروزرسانی پست.");

            return Result<bool>.Success("پست با موفقیت بروزرسانی شد.", true);
        }

        public Result<bool> Delete(int postId)
        {
            var existing = _postService.GetById(postId);
            if (existing == null)
                return Result<bool>.Failure("پست موردنظر یافت نشد.");

            bool ok = _postService.Delete(postId);
            if (!ok)
                return Result<bool>.Failure("خطا در حذف پست.");

            return Result<bool>.Success("پست با موفقیت حذف شد.", true);
        }
    }
}
