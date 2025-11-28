using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Personal_Blog.Domain.AppService.AppServices;
using Personal_Blog.Domain.Core.Author.Contracts.AppServiceContracts;
using Personal_Blog.Domain.Core.Author.Contracts.RepositoryContracts;
using Personal_Blog.Domain.Core.Author.Contracts.ServiceContracts;
using Personal_Blog.Domain.Core.Category.Contracts.AppServiceContracts;
using Personal_Blog.Domain.Core.Category.Contracts.RepositoryContracts;
using Personal_Blog.Domain.Core.Category.Contracts.ServiceContracts;
using Personal_Blog.Domain.Core.Comment.Contracts.AppServiceContracts;
using Personal_Blog.Domain.Core.Comment.Contracts.RepositoryContracts;
using Personal_Blog.Domain.Core.Comment.Contracts.ServiceContracts;
using Personal_Blog.Domain.Core.Post.Contracts.AppServiceContracts;
using Personal_Blog.Domain.Core.Post.Contracts.RepositoryContracts;
using Personal_Blog.Domain.Core.Post.Contracts.ServiceContracts;
using Personal_Blog.Domain.Service.Services;
using Personal_Blog.Infra.Repo.EFCore.Repositories;
using Personal_Blog.Infra.SqlServer.EFCore.Persistence;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);


builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();


builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICommentService, CommentService>();


builder.Services.AddScoped<IAuthorAppService, AuthorAppService>();
builder.Services.AddScoped<ICategoryAppService, CategoryAppService>();
builder.Services.AddScoped<IPostAppService, PostAppService>();
builder.Services.AddScoped<ICommentAppService, CommentAppService>();


builder.Services.AddControllersWithViews();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.SlidingExpiration = true;
    });

var app = builder.Build();



if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();