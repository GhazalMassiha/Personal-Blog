using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Personal_Blog.Domain.Core.Author.Contracts.AppServiceContracts;
using Personal_Blog.Domain.Core.Author.DTOs;
using Personal_Blog.EndPoint.MVC.Models.Account;
using System.Security.Claims;
using System.Threading.Tasks;

public class AccountController : Controller
{
    private readonly IAuthorAppService _authorApp;

    public AccountController(IAuthorAppService authorApp)
    {
        _authorApp = authorApp;
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public IActionResult Register(RegisterViewModel vm)
    {
        var dto = new AuthorCreateDto
        {
            Username = vm.Username,
            Password = vm.Password,
            Email = vm.Email,
            FirstName = vm.FirstName,
            LastName = vm.LastName,
            ImageUrl = vm.ImageUrl
        };

        var res = _authorApp.Create(dto);

        if (!res.IsSuccess)
        {
            ModelState.AddModelError("", res.Message);
            return View(vm);
        }

        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult Login(string returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel vm, string? returnUrl = null)
    {
        var authorRes = _authorApp.GetByUsername(vm.Username);

        if (!authorRes.IsSuccess)
        {
            ModelState.AddModelError("", "نام کاربری یا رمز عبور اشتباه است.");

            return View(vm);
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, "1"),
            new Claim(ClaimTypes.Name, vm.Username),
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
               new ClaimsPrincipal(identity));

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        return RedirectToAction("Dashboard", "AuthorPanel");
    }


    [HttpGet]
    public IActionResult Profile()
    {
        var vm = new ProfileEditViewModel
        {
            Username = User.Identity.Name!,
        };
        return View(vm);
    }


    [HttpPost]
    public IActionResult Profile(ProfileEditViewModel vm)
    {
        int authorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var editDto = new AuthorEditDto
        {
            Username = vm.Username,
            Password = vm.Password,
            Email = vm.Email,
            FirstName = vm.FirstName,
            LastName = vm.LastName,
            ImageUrl = vm.ImageUrl
        };

        var res = _authorApp.Update(authorId, editDto);

        if (!res.IsSuccess)
        {
            ModelState.AddModelError("", res.Message);

            return View(vm);
        }

        ViewBag.Message = "پروفایل با موفقیت به‌روزرسانی شد.";
        return View(vm);
    }


    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Index", "Home");
    }
}