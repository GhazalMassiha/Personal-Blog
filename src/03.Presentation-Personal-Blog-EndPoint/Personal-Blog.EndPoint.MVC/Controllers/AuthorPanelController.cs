using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Personal_Blog.Domain.Core.Category.Contracts.AppServiceContracts;
using Personal_Blog.Domain.Core.Comment.Contracts.AppServiceContracts;
using Personal_Blog.Domain.Core.Post.Contracts.AppServiceContracts;
using Personal_Blog.Domain.Core.Post.DTOs;
using Personal_Blog.EndPoint.MVC.Models.AuthorPanel;
using Personal_Blog.EndPoint.MVC.Services;
using System.Security.Claims;

[Authorize]
public class AuthorPanelController : Controller
{
    private readonly IPostAppService _postApp;
    private readonly ICategoryAppService _catApp;
    private readonly ICommentAppService _commentApp;
    private readonly IImageService _imageService;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<AuthorPanelController> _logger;

    public AuthorPanelController(
        IPostAppService postApp,
        ICategoryAppService catApp,
        ICommentAppService commentApp,
        IImageService imageService,
        IWebHostEnvironment env,
        ILogger<AuthorPanelController> logger)
    {
        _postApp = postApp;
        _catApp = catApp;
        _commentApp = commentApp;
        _imageService = imageService;
        _env = env;
        _logger = logger;
    }

    public IActionResult Dashboard()
    {
        int authorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var posts = _postApp.GetByAuthor(authorId).Data;

        return View(posts);
    }

    [HttpGet]
    public IActionResult CreatePost()
    {
        int authorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var cats = _catApp.GetByAuthor(authorId).Data;
        var vm = new PostViewModel
        {
            Categories = cats.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
        };

        return View(vm);
    }

    [HttpPost]
    [RequestFormLimits(MultipartBodyLengthLimit = 2 * 1024 * 1024)]
    public IActionResult CreatePost(PostViewModel vm, IFormFile? ImageFile)
    {
        int authorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        string? imageUrl = null;

        if (ImageFile != null)
        {
            var imgResult = _imageService.SaveImage(
                ImageFile,
                uploadsFolderRelative: "uploads",
                maxFileSizeBytes: 2 * 1024 * 1024,
                allowedExtensions: new[] { ".jpg", ".jpeg", ".png" });

            if (!imgResult.Success)
            {
                ModelState.AddModelError("ImageFile", imgResult.ErrorMessage!);
            }
            else
            {
                imageUrl = imgResult.RelativePath;
            }
        }

        if (!ModelState.IsValid)
        {
            var cats = _catApp.GetByAuthor(authorId).Data;
            vm.Categories = cats.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });

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

        var result = _postApp.Create(dto);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError("", result.Message);

            var cats = _catApp.GetByAuthor(authorId).Data;
            vm.Categories = cats.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });

            return View(vm);
        }

        return RedirectToAction("Dashboard");
    }

    [HttpPost]
    public IActionResult DeletePost(int id)
    {
        int authorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var getRes = _postApp.GetById(id);
        if (!getRes.IsSuccess)
        {
            TempData["ErrorMessage"] = getRes.Message;
            return RedirectToAction("Dashboard");
        }

        var post = getRes.Data!;
        if (post.AuthorId != authorId)
            return Forbid();

        if (!string.IsNullOrEmpty(post.ImageUrl))
        {
            _imageService.DeleteImage(post.ImageUrl, _env.WebRootPath);
        }

        var delRes = _postApp.Delete(id);
        if (!delRes.IsSuccess)
            TempData["ErrorMessage"] = delRes.Message;

        return RedirectToAction("Dashboard");
    }

    [HttpPost]
    [RequestFormLimits(MultipartBodyLengthLimit = 2 * 1024 * 1024)]
    public IActionResult EditPost(int id, PostViewModel vm, IFormFile? ImageFile)
    {
        int authorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var getRes = _postApp.GetById(id);
        if (!getRes.IsSuccess) return NotFound();

        var existing = getRes.Data!;
        if (existing.AuthorId != authorId) return Forbid();

        string? newImageUrl = existing.ImageUrl;

        if (ImageFile != null)
        {
            var imgResult = _imageService.SaveImage(
                ImageFile,
                uploadsFolderRelative: "uploads",
                maxFileSizeBytes: 2 * 1024 * 1024,
                allowedExtensions: new[] { ".jpg", ".jpeg", ".png" });

            if (!imgResult.Success)
            {
                ModelState.AddModelError("ImageFile", imgResult.ErrorMessage!);
            }
            else
            {
                newImageUrl = imgResult.RelativePath;

                if (!string.IsNullOrEmpty(existing.ImageUrl))
                    _imageService.DeleteImage(existing.ImageUrl, _env.WebRootPath);
            }
        }

        if (!ModelState.IsValid)
        {
            var cats = _catApp.GetByAuthor(authorId).Data;
            vm.Categories = cats.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });

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

        var updRes = _postApp.Update(id, dto);
        if (!updRes.IsSuccess)
        {
            ModelState.AddModelError("", updRes.Message);
            var cats = _catApp.GetByAuthor(authorId).Data;
            vm.Categories = cats.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });

            return View(vm);
        }

        return RedirectToAction("Dashboard");
    }

    public IActionResult Categories()
    {
        int authorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var res = _catApp.GetByAuthor(authorId);
        if (!res.IsSuccess)
        {
            TempData["ErrorMessage"] = res.Message;

            return View(new List<CategoryViewModel>());
        }

        var vm = res.Data.Select(c => new CategoryViewModel
        {
            Id = c.Id,
            Name = c.Name
        }).ToList();

        return View(vm);
    }

    [HttpGet]
    public IActionResult CreateCategory()
    {
        return View(new CategoryViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateCategory(CategoryViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        int authorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var dto = new Personal_Blog.Domain.Core.Category.DTOs.CategoryCreateDto
        {
            Name = vm.Name,
            AuthorId = authorId
        };

        var res = _catApp.Create(dto);
        if (!res.IsSuccess)
        {
            ModelState.AddModelError("", res.Message);

            return View(vm);
        }

        return RedirectToAction(nameof(Categories));
    }

    [HttpGet]
    public IActionResult EditCategory(int id)
    {
        var getRes = _catApp.GetById(id);

        if (!getRes.IsSuccess)
            return NotFound();

        var dto = getRes.Data!;
        var vm = new CategoryViewModel { Id = dto.Id, Name = dto.Name };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EditCategory(int id, CategoryViewModel vm)
    {
        if (id != vm.Id)
            return BadRequest();

        if (!ModelState.IsValid)
            return View(vm);

        var dto = new Personal_Blog.Domain.Core.Category.DTOs.CategoryCreateDto
        {
            Name = vm.Name,
            AuthorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value)
        };

        var res = _catApp.Update(id, dto);
        if (!res.IsSuccess)
        {
            ModelState.AddModelError("", res.Message);

            return View(vm);
        }

        return RedirectToAction(nameof(Categories));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteCategory(int id)
    {
        var res = _catApp.Delete(id);
        if (!res.IsSuccess)
            TempData["ErrorMessage"] = res.Message;

        return RedirectToAction(nameof(Categories));
    }


    public IActionResult Comments()
    {
        int authorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var allRes = _commentApp.GetAll();
        if (!allRes.IsSuccess)
        {
            TempData["ErrorMessage"] = allRes.Message;
            return View(new List<CommentModerationViewModel>());
        }

        var list = new List<CommentModerationViewModel>();
        foreach (var c in allRes.Data!)
        {

            var postRes = _postApp.GetById(c.PostId);
            if (!postRes.IsSuccess) continue;

            var post = postRes.Data!;
            if (post.AuthorId != authorId) continue; 

            list.Add(new CommentModerationViewModel
            {
                Id = c.Id,             
                FullName = c.FullName,
                Email = string.Empty,
                Text = c.Text,
                Rating = c.Rating,
                Status = c.Status,
                PostId = c.PostId,
                PostTitle = post.Title,
                CreatedAt = c.CreatedAt
            });
        }

        return View(list);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ConfirmComment(int commentId)
    {
        var res = _commentApp.Confirm(commentId);
        if (!res.IsSuccess)
            TempData["ErrorMessage"] = res.Message;

        return RedirectToAction(nameof(Comments));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult RejectComment(int commentId)
    {
        var res = _commentApp.Reject(commentId);
        if (!res.IsSuccess)
            TempData["ErrorMessage"] = res.Message;

        return RedirectToAction(nameof(Comments));
    }
}
