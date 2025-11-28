using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Personal_Blog.Domain.Core.Category.Contracts.AppServiceContracts;
using Personal_Blog.Domain.Core.Post.Contracts.AppServiceContracts;
using Personal_Blog.Domain.Core.Post.DTOs;
using Personal_Blog.EndPoint.MVC.Models.AuthorPanel;
using System.Security.Claims;


public class AuthorPanelController : Controller
{
    private readonly IPostAppService _postApp;
    private readonly ICategoryAppService _catApp;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<AuthorPanelController> _logger;

    public AuthorPanelController(
        IPostAppService postApp,
        ICategoryAppService catApp,
        IWebHostEnvironment env,
        ILogger<AuthorPanelController> logger)
    {
        _postApp = postApp;
        _catApp = catApp;
        _env = env;
        _logger = logger;
    }

    public IActionResult Dashboard()
    {
        try
        {
            int authorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var posts = _postApp.GetByAuthor(authorId).Data;

            return View(posts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطا در دریافت فهرست پست‌ها برای نویسنده");

            TempData["ErrorMessage"] = "خطا در بارگذاری پست‌ها — لطفاً دوباره تلاش کنید.";

            return View(Enumerable.Empty<PostDto>());
        }
    }

    [HttpGet]
    public IActionResult CreatePost()
    {
        try
        {
            int authorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var vm = new PostViewModel
            {
                Categories = _catApp.GetByAuthor(authorId).Data
                   .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
            };

            return View(vm);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطا در نمایش فرم ایجاد پست");

            TempData["ErrorMessage"] = "خطا در باز کردن فرم — لطفاً بعداً تلاش کنید.";

            return RedirectToAction("Dashboard");
        }
    }

    [HttpPost]
    [RequestFormLimits(MultipartBodyLengthLimit = 2 * 1024 * 1024)] 
    public IActionResult CreatePost(PostViewModel vm, IFormFile? ImageFile)
    {
        try
        {
            int authorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);


            if (string.IsNullOrWhiteSpace(vm.Title))
                ModelState.AddModelError(nameof(vm.Title), "عنوان پست الزامی است.");

            if (string.IsNullOrWhiteSpace(vm.Content))
                ModelState.AddModelError(nameof(vm.Content), "متن پست الزامی است.");

            if (!_catApp.GetByAuthor(authorId).Data.Any(c => c.Id == vm.CategoryId))
                ModelState.AddModelError(nameof(vm.CategoryId), "دسته‌بندی انتخاب‌شده معتبر نیست.");

            string? imageUrl = null;

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var ext = Path.GetExtension(ImageFile.FileName).ToLowerInvariant();

                var allowed = new[] { ".jpg", ".jpeg", ".png" };

                if (!allowed.Contains(ext))
                    ModelState.AddModelError("ImageFile", "تصویر باید با فرمت JPG یا PNG باشد.");

                else if (ImageFile.Length > 2 * 1024 * 1024)
                    ModelState.AddModelError("ImageFile", "حجم تصویر نباید بیش از ۲ مگابایت باشد.");

                else
                {
                    var uploads = Path.Combine(_env.WebRootPath, "uploads");

                    if (!Directory.Exists(uploads))
                        Directory.CreateDirectory(uploads);

                    var fileName = $"{Guid.NewGuid()}{ext}";

                    var filePath = Path.Combine(uploads, fileName);

                    using (var fs = new FileStream(filePath, FileMode.Create))
                        ImageFile.CopyTo(fs);

                    imageUrl = "/uploads/" + fileName;
                }
            }

            if (!ModelState.IsValid)
            {
                vm.Categories = _catApp.GetByAuthor(authorId).Data
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });

                return View(vm);
            }

            var dto = new PostCreateDto
            {
                Title = vm.Title,
                Content = vm.Content,
                ImageUrl = imageUrl,
                AuthorId = authorId,
                CategoryId = vm.CategoryId
            };

            var res = _postApp.Create(dto);

            if (!res.IsSuccess)
            {
                _logger.LogWarning("خطا در ذخیره پست: {Message}", res.Message);
                ModelState.AddModelError("", "خطا در ذخیره پست — لطفاً دوباره تلاش کنید.");

                vm.Categories = _catApp.GetByAuthor(authorId).Data
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });

                return View(vm);
            }

            return RedirectToAction("Dashboard");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطا در ایجاد پست جدید");

            ModelState.AddModelError("", "خطا در پردازش درخواست — لطفاً بعداً تلاش کنید.");

            int authorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            vm.Categories = _catApp.GetByAuthor(authorId).Data
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });

            return View(vm);
        }
    }

    [HttpGet]
    public IActionResult EditPost(int id)
    {
        try
        {
            int authorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var postRes = _postApp.GetById(id);

            if (!postRes.IsSuccess) return NotFound();

            var p = postRes.Data;

            if (p.AuthorId != authorId) return Forbid();

            var vm = new PostViewModel
            {
                Id = id,
                Title = p.Title,
                Content = p.Content,
                CategoryId = p.CategoryId,
                ImageUrl = p.ImageUrl,
                Categories = _catApp.GetByAuthor(authorId).Data
                   .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
            };

            return View(vm);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطا در نمایش فرم ویرایش پست {PostId}", id);

            TempData["ErrorMessage"] = "خطا در باز کردن فرم ویرایش — لطفاً بعداً تلاش کنید.";

            return RedirectToAction("Dashboard");
        }
    }

    [HttpPost]
    [RequestFormLimits(MultipartBodyLengthLimit = 2 * 1024 * 1024)]
    public IActionResult EditPost(int id, PostViewModel vm, IFormFile? ImageFile)
    {
        try
        {
            if (id != vm.Id) return BadRequest();

            int authorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var postRes = _postApp.GetById(id);

            if (!postRes.IsSuccess) return NotFound();

            var existing = postRes.Data;
            if (existing.AuthorId != authorId) return Forbid();


            if (string.IsNullOrWhiteSpace(vm.Title))
                ModelState.AddModelError(nameof(vm.Title), "عنوان پست الزامی است.");

            if (string.IsNullOrWhiteSpace(vm.Content))
                ModelState.AddModelError(nameof(vm.Content), "متن پست الزامی است.");

            if (!_catApp.GetByAuthor(authorId).Data.Any(c => c.Id == vm.CategoryId))
                ModelState.AddModelError(nameof(vm.CategoryId), "دسته‌بندی انتخاب‌شده معتبر نیست.");

            string? newImageUrl = existing.ImageUrl;

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var ext = Path.GetExtension(ImageFile.FileName).ToLowerInvariant();

                var allowed = new[] { ".jpg", ".jpeg", ".png" };

                if (!allowed.Contains(ext))
                    ModelState.AddModelError("ImageFile", "فرمت تصویر باید JPG یا PNG باشد.");

                else if (ImageFile.Length > 2 * 1024 * 1024)
                    ModelState.AddModelError("ImageFile", "حجم تصویر نباید بیشتر از ۲ مگابایت باشد.");

                else
                {
                    var uploads = Path.Combine(_env.WebRootPath, "uploads");

                    if (!Directory.Exists(uploads))
                        Directory.CreateDirectory(uploads);

                    var newFileName = $"{Guid.NewGuid()}{ext}";

                    var filePath = Path.Combine(uploads, newFileName);

                    using var fs = new FileStream(filePath, FileMode.Create);
                    ImageFile.CopyTo(fs);

                    newImageUrl = "/uploads/" + newFileName;

                    if (!string.IsNullOrEmpty(existing.ImageUrl))
                    {
                        var oldPhysical = Path.Combine(_env.WebRootPath,
                            existing.ImageUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));

                        if (System.IO.File.Exists(oldPhysical))
                        {
                            try { System.IO.File.Delete(oldPhysical); }

                            catch (Exception delEx)
                            {
                                _logger.LogWarning(delEx, "خطا در حذف تصویر قدیمی پس از به‌روزرسانی پست {PostId}", id);
                            }
                        }
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                vm.Categories = _catApp.GetByAuthor(authorId).Data
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });

                return View(vm);
            }

            var dto = new PostCreateDto
            {
                Title = vm.Title,
                Content = vm.Content,
                ImageUrl = newImageUrl,
                AuthorId = authorId,
                CategoryId = vm.CategoryId
            };

            var res = _postApp.Update(id, dto);

            if (!res.IsSuccess)
            {
                _logger.LogWarning("خطا در به‌روزرسانی پست {PostId}: {Message}", id, res.Message);

                ModelState.AddModelError("", "خطا در ذخیره تغییرات — لطفاً دوباره تلاش کنید.");

                vm.Categories = _catApp.GetByAuthor(authorId).Data
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });

                return View(vm);
            }

            return RedirectToAction("Dashboard");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطا در ویرایش پست {PostId}", id);

            ModelState.AddModelError("", "خطا در پردازش درخواست — لطفاً بعداً تلاش کنید.");

            int authorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            vm.Categories = _catApp.GetByAuthor(authorId).Data
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });

            return View(vm);
        }
    }

    [HttpPost]
    public IActionResult DeletePost(int id)
    {
        try
        {
            int authorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var postRes = _postApp.GetById(id);

            if (!postRes.IsSuccess) return NotFound();

            var p = postRes.Data;

            if (p.AuthorId != authorId) return Forbid();

            if (!string.IsNullOrEmpty(p.ImageUrl))
            {
                var oldPhysical = Path.Combine(_env.WebRootPath,
                    p.ImageUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));

                if (System.IO.File.Exists(oldPhysical))
                {
                    System.IO.File.Delete(oldPhysical);
                }
            }

            var res = _postApp.Delete(id);
            if (!res.IsSuccess)
            {
                _logger.LogWarning("خطا در حذف پست {PostId}", id);

                TempData["ErrorMessage"] = "حذف پست موفق نبود — لطفاً دوباره تلاش کنید.";
            }
            return RedirectToAction("Dashboard");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطا در حذف پست {PostId}", id);

            TempData["ErrorMessage"] = "خطا در حذف پست — لطفاً بعداً تلاش کنید.";

            return RedirectToAction("Dashboard");
        }
    }


}