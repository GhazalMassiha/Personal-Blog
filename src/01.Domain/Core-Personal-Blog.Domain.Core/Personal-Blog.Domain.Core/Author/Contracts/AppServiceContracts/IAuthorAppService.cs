using Personal_Blog.Domain.Core._common;
using Personal_Blog.Domain.Core.Author.DTOs;

namespace Personal_Blog.Domain.Core.Author.Contracts.AppServiceContracts
{
    public interface IAuthorAppService
    {
        Result<AuthorDto> GetById(int authorId);
        Result<AuthorDto> GetByUsername(string username);
        Result<List<AuthorDto>> GetAll();
        Result<bool> VerifyPassword(string username, string password);
        Result<bool> Create(AuthorCreateDto dto);
        Result<bool> Update(int authorId, AuthorEditDto dto);
        Result<bool> Delete(int authorId);
    }
}
