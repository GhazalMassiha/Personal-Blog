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
            var author = _context.Authors.FirstOrDefault(a => a.Id == authorId);
            if (author == null)
                return false;

            if (!string.IsNullOrWhiteSpace(dto.Username))
                author.Username = dto.Username.Trim();

            if (!string.IsNullOrWhiteSpace(dto.Password))
                author.Password = dto.Password;

            if (!string.IsNullOrWhiteSpace(dto.Email))
                author.Email = dto.Email.Trim();

            if (!string.IsNullOrWhiteSpace(dto.FirstName))
                author.FirstName = dto.FirstName.Trim();

            if (!string.IsNullOrWhiteSpace(dto.LastName))
                author.LastName = dto.LastName.Trim();

            if (dto.ImageUrl != null)
                author.ImageUrl = dto.ImageUrl.Trim();


            author.UpdatedAt = DateTime.Now;

            _context.Authors.Update(author);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(int authorId)
        {
            var result = _context.Authors.Where(a => a.Id == authorId).ExecuteDelete();

            return result > 0;
        }
    }
}
