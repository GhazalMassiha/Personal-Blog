using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Personal_Blog.Domain.Core.Post.Entities;

namespace Personal_Blog.Infra.SqlServer.EFCore.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Posts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.Content)
                .IsRequired()
                .HasMaxLength(4000);

            builder.Property(x => x.ImageUrl)
               .HasMaxLength(500);

            builder.Property(u => u.CreatedAt)
                .HasDefaultValueSql("GetDate()")
                .ValueGeneratedOnAdd();

            builder.HasOne(c => c.Author)
                .WithMany(a => a.Posts)
                .HasForeignKey(a => a.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Category)
                .WithMany(a => a.Posts)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
              new Post { Id = 1, Title = "شروع کار با ASP.NET Core MVC", Content = "در این مقاله قصد داریم در قدم اول به چگونگی ایجاد یک پروژه ساده ASP.NET Core MVC بپردازیم. ابتدا باید SDK دات‌نت نصب شود، سپس یک پروژه جدید با دستور dotnet new mvc ساخته شود. پس از آن تنظیمات اتصال به دیتابیس و پیکربندی EF Core را انجام می‌دهیم. ادامه‌ی مطلب … (متن کامل را خودتان اضافه کنید)", ImageUrl = null, AuthorId = 3, CategoryId = 4},

              new Post { Id = 2, Title = "معرفی EF Core و مایگریشن‌ها", Content = "Entity Framework Core به ما این امکان را می‌دهد که مدل‌های C# را به جداول دیتابیس تبدیل کنیم. با مایگریشن، هر تغییر در مدل به‌صورت خودکار به دیتابیس اعمال می‌شود. در این نوشته درباره مزایا، معایب و نکات مهم EF Core بحث خواهیم کرد. ادامه‌ی مطلب …", ImageUrl = null, AuthorId = 3, CategoryId = 4},

              new Post { Id = 3, Title = "سفر به شمال ایران: تجربه‌ای متفاوت", Content = "شمال ایران با طبیعت بکر و آب‌وهوا دلپذیر همواره مقصد بسیاری از مسافران بوده است. در این نوشته تجربه من از سفری چندروزه به استان گیلان و مازندران آورده شده است: صبح زود از تهران حرکت کردیم … ادامه مطلب …", ImageUrl = null, AuthorId = 1, CategoryId = 2},

              new Post { Id = 4, Title = "سبک زندگی سالم — عادات روزمره", Content = "داشتن سبک زندگی سالم نیازمند توجه به تغذیه، خواب، ورزش و آرامش ذهنی است. در این مقاله چند عادت ساده و عملی برای زندگی سالم پیشنهاد می‌شود: ۱) پیاده‌روی روزانه … ادامه مطلب …", ImageUrl = null, AuthorId = 2, CategoryId = 3 },

              new Post { Id = 5, Title = "۵ کتاب عالی برای توسعه‌دهندگان .NET", Content = "خواندن کتاب یکی از بهترین روش‌ها برای بهبود مهارت‌های برنامه‌نویسی است. در این پست ۵ کتاب در حوزه دات‌نت و معماری نرم‌افزار معرفی می‌شود: ۱) Pro ASP.NET Core MVC … ادامه مطلب …", ImageUrl = null, AuthorId = 4, CategoryId = 5},

              new Post { Id = 6, Title = "تغذیه سالم و ورزش روزانه", Content = "تغذیه مناسب و ورزش منظم به بدن انرژی می‌دهد و سلامت جسم و ذهن را تضمین می‌کند. در این مقاله نکاتی درباره رژیم غذایی، آب کافی و ورزش پیوسته گفته می‌شود … ادامه مطلب …", ImageUrl = null, AuthorId = 5, CategoryId = 6 },

              new Post { Id = 7, Title = "چگونه پروژه MVC را deploy کنیم؟", Content = "پس از توسعه پروژه وب، زمان deploy و استقرار آن می‌رسد. در این مطلب گام‌های لازم برای deploy یک پروژه ASP.NET Core MVC روی سرور یا هاست بررسی شده است … ادامه مطلب …", ImageUrl = null, AuthorId = 3, CategoryId = 4},

              new Post { Id = 8, Title = "راهنمای سفر به اروپا", Content = "اگر قصد سفر به اروپا دارید، این راهنما برای شماست: مسافرت اقتصادی، انتخاب کشورها، زمان مناسب و نکات مهم … ادامه مطلب …", ImageUrl = null, AuthorId = 1, CategoryId = 2 },

              new Post { Id = 9, Title = "Mindfulness و کاهش استرس", Content = "زندگی پرشتاب امروزی استرس زیادی به ما تحمیل می‌کند. در این مقاله با مفهوم Mindfulness و تکنیک‌های کاهش استرس آشنا می‌شویم … ادامه مطلب …", ImageUrl = null, AuthorId = 2, CategoryId = 3 },

              new Post { Id = 10, Title = "شروع یادگیری Blazor", Content = "Blazor چارچوب جدیدی برای ساخت رابط کاربری با C# و دات‌نت است. در این مطلب ابتدا با مفاهیم پایه آشنا می‌شویم … ادامه مطلب …", ImageUrl = null, AuthorId = 4, CategoryId = 4 }
          );
        }
    }
}
