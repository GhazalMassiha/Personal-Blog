using Personal_Blog.Domain.Core._common;
using Personal_Blog.Domain.Core.Author.Contracts.AppServiceContracts;
using Personal_Blog.Domain.Core.Author.Contracts.RepositoryContracts;
using Personal_Blog.Domain.Core.Author.Contracts.ServiceContracts;
using Personal_Blog.Domain.Core.Author.DTOs;

namespace Personal_Blog.Domain.AppService.AppServices
{
    public class AuthorAppService : IAuthorAppService
    {
        private readonly IAuthorService _authorService;
        public AuthorAppService(IAuthorService authorService) => _authorService = authorService;

        public Result<AuthorDto> GetById(int authorId)
        {
            var author = _authorService.GetById(authorId);
            if (author == null)
                return Result<AuthorDto>.Failure("نویسنده یافت نشد.");

            return Result<AuthorDto>.Success("نویسنده با موفقیت بازیابی شد.", author);
        }

        public Result<AuthorDto> GetByUsername(string username)
        {
            var author = _authorService.GetByUsername(username);
            if (author == null)
                return Result<AuthorDto>.Failure("نویسنده با این نام کاربری یافت نشد.");

            return Result<AuthorDto>.Success("نویسنده با موفقیت پیدا شد.", author);
        }

        public Result<List<AuthorDto>> GetAll()
        {
            var list = _authorService.GetAll();

            return Result<List<AuthorDto>>.Success("لیست نویسندگان دریافت شد.", list);
        }

        public Result<bool> Create(AuthorCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Password))
                return Result<bool>.Failure("نام کاربری و کلمه عبور الزامی است.");

            if (_authorService.GetByUsername(dto.Username) != null)
                return Result<bool>.Failure("این نام کاربری قبلاً ثبت شده است.");

            bool ok = _authorService.Create(dto);
            if (!ok)
                return Result<bool>.Failure("خطا در ثبت نویسنده.");

            return Result<bool>.Success("نویسنده با موفقیت ثبت شد.", true);
        }

        public Result<bool> Update(int authorId, AuthorEditDto dto)
        {
            var existing = _authorService.GetById(authorId);
            if (existing == null)
                return Result<bool>.Failure("نویسنده یافت نشد.");

            var other = _authorService.GetByUsername(dto.Username);

            if (other != null && other.Username != existing.Username)
                return Result<bool>.Failure("این نام کاربری قبلاً ثبت شده است.");

            bool ok = _authorService.Update(authorId, dto);
            if (!ok)
                return Result<bool>.Failure("خطا در بروزرسانی اطلاعات نویسنده.");

            return Result<bool>.Success("اطلاعات نویسنده با موفقیت بروزرسانی شد.", true);
        }

        public Result<bool> Delete(int authorId)
        {
            var existing = _authorService.GetById(authorId);
            if (existing == null)
                return Result<bool>.Failure("نویسنده یافت نشد.");

            bool ok = _authorService.Delete(authorId);
            if (!ok)
                return Result<bool>.Failure("خطا در حذف نویسنده.");

            return Result<bool>.Success("نویسنده با موفقیت حذف شد.", true);
        }
    }
}
