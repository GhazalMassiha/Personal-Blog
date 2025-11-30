using Personal_Blog.Domain.Core.Author.DTOs;

namespace Personal_Blog.Domain.Core.Author.Contracts.RepositoryContracts
{
    public interface IAuthorRepository
    {
        AuthorDto? GetById(int authorId);
        AuthorDto? GetByUsername(string username);
        List<AuthorDto> GetAll();
        public AuthorLoginDto? GetByUsernameToVerifyPassword(string username);
        bool Create(AuthorCreateDto dto);
        bool Update(int authorId, AuthorEditDto dto);
        bool Delete(int authorId);
    }
}
