using Microsoft.EntityFrameworkCore;
using Personal_Blog.Domain.Core.Author.Contracts.RepositoryContracts;
using Personal_Blog.Domain.Core.Author.DTOs;
using Personal_Blog.Domain.Core.Author.Entities;
using Personal_Blog.Infra.SqlServer.EFCore.Persistence;

namespace Personal_Blog.Infra.Repo.EFCore.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _context;
        public AuthorRepository(AppDbContext context) => _context = context;

        public AuthorDto? GetById(int authorId)
        {
            return _context.Authors
                .Where(a => a.Id == authorId)
                .Select(a => new AuthorDto
                {
                    Username = a.Username,
                    Email = a.Email,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    ImageUrl = a.ImageUrl
                })
                .FirstOrDefault();
        }

        public AuthorDto? GetByUsername(string username)
        {
            return _context.Authors
                .Where(a => a.Username == username)
                .Select(a => new AuthorDto
                {
                    Username = a.Username,
                    Email = a.Email,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    ImageUrl = a.ImageUrl
                })
                .FirstOrDefault();
        }

        public List<AuthorDto> GetAll()
        {
            return _context.Authors
                .Select(a => new AuthorDto
                {
                    Username = a.Username,
                    Email = a.Email,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    ImageUrl = a.ImageUrl
                })
                .ToList();
        }

        public bool Create(AuthorCreateDto dto)
        {
            var author = new Author
            {
                Username = dto.Username,
                Password = dto.Password,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                ImageUrl = dto.ImageUrl
            };

            _context.Authors.Add(author);
            return _context.SaveChanges() > 0;
        }

        public bool Update(int authorId, AuthorEditDto dto)
        {
            var affected = _context.Authors
                .Where(a => a.Id == authorId)
                .ExecuteUpdate(b => b
                    .SetProperty(a => a.Username, dto.Username)
                    .SetProperty(a => a.Password, dto.Password)
                    .SetProperty(a => a.Email, dto.Email)
                    .SetProperty(a => a.FirstName, dto.FirstName)
                    .SetProperty(a => a.LastName, dto.LastName)
                    .SetProperty(a => a.ImageUrl, dto.ImageUrl)
                );

            return affected > 0;
        }

        public bool Delete(int authorId)
        {
            var result = _context.Authors.Where(a => a.Id == authorId).ExecuteDelete();

            return result > 0;
        }
    }
}
