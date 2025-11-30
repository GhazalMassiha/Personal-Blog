using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Personal_Blog.Domain.Core.Author.Contracts.AppServiceContracts;
using Personal_Blog.Domain.Core.Author.DTOs;
using Personal_Blog.EndPoint.MVC.Models.Account;
using System.Security.Claims;

public class AccountController(IAuthorAppService authorApp) : Controller
{
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

        var res = authorApp.Create(dto);

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
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        // 1. ابتدا بررسی نام کاربری
        var authorRes = authorApp.GetByUsername(vm.Username);
        if (!authorRes.IsSuccess || authorRes.Data == null)
        {
            ModelState.AddModelError("", "نام کاربری یا رمز عبور اشتباه است.");
            return View(vm);
        }

        // 2. سپس اعتبارسنجی پسورد
        var verifyRes = authorApp.VerifyPassword(vm.Username, vm.Password);
        if (!verifyRes.IsSuccess)
        {
            ModelState.AddModelError("", verifyRes.Message);
            return View(vm);
        }

        var user = authorRes.Data;

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        var properties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTime.UtcNow.AddDays(5)
        };

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);

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

        var res = authorApp.Update(authorId, editDto);

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