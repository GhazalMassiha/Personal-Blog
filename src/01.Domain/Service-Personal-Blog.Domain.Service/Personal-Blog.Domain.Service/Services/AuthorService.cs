using Personal_Blog.Domain.Core.Author.Contracts.RepositoryContracts;
using Personal_Blog.Domain.Core.Author.Contracts.ServiceContracts;
using Personal_Blog.Domain.Core.Author.DTOs;

namespace Personal_Blog.Domain.Service.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorService(IAuthorRepository authorRepository) => _authorRepository = authorRepository;

        public bool Create(AuthorCreateDto dto)
        {
            return _authorRepository.Create(dto);
        }

        public bool Delete(int authorId)
        {
            return _authorRepository.Delete(authorId);
        }

        public List<AuthorDto> GetAll()
        {
            return _authorRepository.GetAll();
        }

        public AuthorDto? GetById(int authorId)
        {
            return _authorRepository.GetById(authorId);
        }

        public AuthorDto? GetByUsername(string username)
        {
            return _authorRepository.GetByUsername(username);
        }

        public bool Update(int authorId, AuthorEditDto dto)
        {
            return _authorRepository.Update(authorId, dto);
        }
    }
}
