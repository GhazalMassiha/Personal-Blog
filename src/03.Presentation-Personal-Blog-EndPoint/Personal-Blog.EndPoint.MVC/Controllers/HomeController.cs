using Microsoft.AspNetCore.Mvc;
using Personal_Blog.Domain.Core.Comment.Contracts.AppServiceContracts;
using Personal_Blog.Domain.Core.Comment.DTOs;
using Personal_Blog.Domain.Core.Post.Contracts.AppServiceContracts;
using Personal_Blog.Domain.Core.Post.DTOs;
using Personal_Blog.EndPoint.MVC.Models.Home;

public class HomeController : Controller
{
    private readonly IPostAppService _postApp;
    private readonly ICommentAppService _commentApp;

    public HomeController(IPostAppService postApp, ICommentAppService commentApp)
    {
        _postApp = postApp;
        _commentApp = commentApp;
    }

    public IActionResult Index()
    {
        var postsRes = _postApp.GetAll();
        var list = (postsRes.IsSuccess ? postsRes.Data : new List<PostDto>())
            .Select(p => new PostListItemViewModel
            {
                Id = p.Id,
                Title = p.Title,
                ImageUrl = p.ImageUrl,
                Summary = p.Content.Length > 200 ? p.Content.Substring(0, 200) + "..." : p.Content,
                CreatedAt = p.CreatedAt,
                CategoryId = p.CategoryId,
                CategoryName = ""
            }).ToList();

        return View(list);
    }

    public IActionResult Details(int id)
    {
        if (id <= 0)
            return NotFound();

        var postRes = _postApp.GetById(id);

        if (!postRes.IsSuccess || postRes.Data == null)
            return NotFound();

        var commentRes = _commentApp.GetByPost(id);

        var vm = new PostDetailsViewModel
        {
            Post = postRes.Data,
            Comments = commentRes.IsSuccess && commentRes.Data != null
                       ? commentRes.Data
                       : new List<CommentDto>(),
            NewComment = new CommentCreateDto
            {
                PostId = id
            }
        };
        return View(vm);
    }
    [HttpPost]
    public IActionResult AddComment([Bind(Prefix = "NewComment")] CommentCreateDto dto)
    {
        var res = _commentApp.Create(dto);
        if (!res.IsSuccess)
        {
            TempData["ErrorMessage"] = res.Message;
        }
        else
        {
            TempData["message"] = "با موفقیت ذخیر شد";
        }

        return RedirectToAction("Details", "Home", new { id = dto.PostId });
    }
}
