using Personal_Blog.Domain.Core._common;
using Personal_Blog.Domain.Core.Comment.Contracts.AppServiceContracts;
using Personal_Blog.Domain.Core.Comment.Contracts.RepositoryContracts;
using Personal_Blog.Domain.Core.Comment.Contracts.ServiceContracts;
using Personal_Blog.Domain.Core.Comment.DTOs;
using Personal_Blog.Domain.Core.Comment.Enums;
using Personal_Blog.Domain.Core.Post.Contracts.RepositoryContracts;
using Personal_Blog.Domain.Core.Post.Contracts.ServiceContracts;
using System.Net.Mail;

namespace Personal_Blog.Domain.AppService.AppServices
{
    public class CommentAppService : ICommentAppService
    {
        private readonly ICommentService _commentService;
        private readonly IPostService _postService;

        public CommentAppService(ICommentService commentService, IPostService postService)
        { 
            _commentService = commentService;
            _postService = postService;
        }

        public Result<CommentDto> GetById(int commentId)
        {
            var dto = _commentService.GetById(commentId);
            if (dto == null)
                return Result<CommentDto>.Failure("کامنت یافت نشد.");

            return Result<CommentDto>.Success("کامنت دریافت شد.", dto);
        }

        public Result<List<CommentDto>> GetByPost(int postId)
        {
            var post = _postService.GetById(postId);
            if (post == null)
                return Result<List<CommentDto>>.Failure("پست موردنظر یافت نشد.");

            var list = _commentService.GetCommentByPostId(postId)
                         .Where(c => c.Status == StatusEnum.Confirmed)
                         .ToList();

            return Result<List<CommentDto>>.Success("کامنت‌ها دریافت شدند.", list);
        }

        public Result<List<CommentDto>> GetAll()
        {
            var list = _commentService.GetAll();

            return Result<List<CommentDto>>.Success("تمام کامنت‌ها دریافت شدند.", list);
        }

        public Result<bool> Create(CommentCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FullName))
                return Result<bool>.Failure("نام و نام خانوادگی الزامی است.");


            if (dto.FullName.Length < 3 || dto.FullName.Length > 200)
                return Result<bool>.Failure("نام و نام خانوادگی باید بین 3 تا ۲۰۰ کاراکتر باشد.");


            if (string.IsNullOrWhiteSpace(dto.Email))
                return Result<bool>.Failure("ایمیل الزامی است.");


            if (string.IsNullOrWhiteSpace(dto.Text))
                return Result<bool>.Failure("متن کامنت نمی‌تواند خالی باشد.");

            if (dto.Text.Length < 5)
                return Result<bool>.Failure("متن کامنت باید حداقل ۵ کاراکتر باشد.");


            if (dto.Rating < 1 || dto.Rating > 5)
                return Result<bool>.Failure("امتیاز باید عددی بین ۱ تا ۵ باشد.");


            var post = _postService.GetById(dto.PostId);
            if (post == null)
                return Result<bool>.Failure("پست موردنظر یافت نشد.");

            bool ok = _commentService.Create(dto);
            if (!ok)
                return Result<bool>.Failure("خطا در ثبت کامنت.");


            return Result<bool>.Success("کامنت با موفقیت ثبت شد و در انتظار تأیید است.", true);
        }

        public Result<bool> Confirm(int commentId)
        {
            var existing = _commentService.GetById(commentId);
            if (existing == null)
                return Result<bool>.Failure("کامنت یافت نشد.");

            bool ok = _commentService.ChangeStatusToConfirmed(commentId);
            if (!ok)
                return Result<bool>.Failure("خطا در تأیید کامنت.");

            return Result<bool>.Success("کامنت تأیید شد.", true);
        }

        public Result<bool> Reject(int commentId)
        {
            var existing = _commentService.GetById(commentId);
            if (existing == null)
                return Result<bool>.Failure("کامنت یافت نشد.");

            bool ok = _commentService.ChangeStatusToRejected(commentId);
            if (!ok)
                return Result<bool>.Failure("خطا در رد کردن کامنت.");

            return Result<bool>.Success("کامنت رد شد.", true);
        }

        public Result<bool> Delete(int commentId)
        {
            var existing = _commentService.GetById(commentId);
            if (existing == null)
                return Result<bool>.Failure("کامنت یافت نشد.");

            bool ok = _commentService.Delete(commentId);
            if (!ok)
                return Result<bool>.Failure("خطا در حذف کامنت.");

            return Result<bool>.Success("کامنت با موفقیت حذف شد.", true);
        }
    }
}
