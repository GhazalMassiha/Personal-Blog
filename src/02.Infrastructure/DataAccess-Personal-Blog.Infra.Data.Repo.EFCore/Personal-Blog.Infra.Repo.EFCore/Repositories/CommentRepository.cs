using Microsoft.EntityFrameworkCore;
using Personal_Blog.Domain.Core.Comment.Contracts.RepositoryContracts;
using Personal_Blog.Domain.Core.Comment.DTOs;
using Personal_Blog.Domain.Core.Comment.Entities;
using Personal_Blog.Domain.Core.Comment.Enums;
using Personal_Blog.Infra.SqlServer.EFCore.Persistence;

namespace Personal_Blog.Infra.Repo.EFCore.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;
        public CommentRepository(AppDbContext context) => _context = context;

        public CommentDto? GetById(int commentId)
        {
            return _context.Comments.Where(c => c.Id == commentId).Select(c => new CommentDto()
            {
                Id = c.Id,
                FullName = c.FullName,
                Text = c.Text,
                Rating = c.Rating,
                Status = c.Status,
                CreatedAt = c.CreatedAt,
                PostId = c.PostId,
            }).FirstOrDefault();
        }

        public List<CommentDto> GetCommentByPostId(int postId)
        {
            return _context.Comments.Where(c => c.PostId == postId).Select(c => new CommentDto()
            {
                Id = c.Id,
                FullName = c.FullName,
                Text = c.Text,
                Rating = c.Rating,
                Status = c.Status,
                CreatedAt = c.CreatedAt,
                PostId = c.PostId,
            }).ToList();
        }

        public List<CommentDto> GetAll()
        {
            return _context.Comments.Select(c => new CommentDto()
            {
                Id = c.Id,
                FullName = c.FullName,
                Text = c.Text,
                Rating = c.Rating,
                Status = c.Status,
                CreatedAt = c.CreatedAt,
                PostId = c.PostId,
            }).ToList();
        }

        public bool Create(CommentCreateDto commentCreateDto)
        {
            var comment = new Comment()
            {
                FullName = commentCreateDto.FullName,
                Email = commentCreateDto.Email,
                Text = commentCreateDto.Text,
                Rating = commentCreateDto.Rating,
                Status = StatusEnum.Pending,
                PostId = commentCreateDto.PostId,
            };

            _context.Comments.Add(comment);
            return _context.SaveChanges()>0;
        }

        public bool ChangeStatusToConfirmed(int commentId)
        {
            var result = _context.Comments
                .Where(c => c.Id == commentId)
                .ExecuteUpdate(setter => setter
                .SetProperty(c => c.Status, StatusEnum.Confirmed));

            return result > 0;
        }

        public bool ChangeStatusToRejected(int commentId)
        {
            var result = _context.Comments
                .Where(c => c.Id == commentId)
                .ExecuteUpdate(setter => setter
                .SetProperty(c => c.Status, StatusEnum.Rejected));

            return result > 0;
        }

        public bool Delete(int commentId)
        {
            var result = _context.Comments.Where(c => c.Id == commentId).ExecuteDelete();

            return result > 0;
        }
    }
}