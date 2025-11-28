using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Personal_Blog.Domain.Core.Comment.Entities;
using Personal_Blog.Domain.Core.Comment.Enums;

namespace Personal_Blog.Infra.SqlServer.EFCore.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.Text)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(x => x.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(u => u.CreatedAt)
                .HasDefaultValueSql("GetDate()")
                .ValueGeneratedOnAdd();

            builder.HasOne(c => c.Post)
                .WithMany(a => a.Comments)
                .HasForeignKey(a => a.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
             // برای پست 1
             new Comment { Id = 1, FullName = "کاربر مهمان", Email = "guest1@example.com", Text = "خیلی ممنون، شروع خوبی برای پروژه بود!", Rating = 5, Status = StatusEnum.Confirmed, PostId = 1, CreatedAt = new DateTime(2025, 1, 13), UpdatedAt = new DateTime(2025, 1, 13) },
             new Comment { Id = 2, FullName = "مریم رضایی", Email = "maryam@example.com", Text = "مفید بود — لطفاً ادامه دهید.", Rating = 4, Status = StatusEnum.Pending, PostId = 1, CreatedAt = new DateTime(2025, 1, 13), UpdatedAt = new DateTime(2025, 1, 13) },

             // برای پست 2
             new Comment { Id = 3, FullName = "حسین کریمی", Email = "hossein@example.com", Text = "مقاله عالی، خیلی کمک کرد.", Rating = 5, Status = StatusEnum.Confirmed, PostId = 2, CreatedAt = new DateTime(2025, 1, 14), UpdatedAt = new DateTime(2025, 1, 14) },
             new Comment { Id = 4, FullName = "شکیبا نوروزی", Email = "shakiba@example.com", Text = "خوبه ولی انتظار داشتم جزئیات فنی بیشتری باشه.", Rating = 3, Status = StatusEnum.Rejected, PostId = 2, CreatedAt = new DateTime(2025, 1, 14), UpdatedAt = new DateTime(2025, 1, 14) },

             // برای پست 3
             new Comment { Id = 5, FullName = "سارا رضوانی", Email = "sara@example.com", Text = "چه خاطره خوبی!", Rating = 5, Status = StatusEnum.Confirmed, PostId = 3, CreatedAt = new DateTime(2025, 1, 15), UpdatedAt = new DateTime(2025, 1, 15) },
             new Comment { Id = 6, FullName = "رضا عباسی", Email = "reza.abassi@example.com", Text = "دوست دارم وقتی سفر میکنم اینها رو بخونم.", Rating = 4, Status = StatusEnum.Pending, PostId = 3, CreatedAt = new DateTime(2025, 1, 15), UpdatedAt = new DateTime(2025, 1, 15) },

             // برای پست 4
             new Comment { Id = 7, FullName = "علی حسینی", Email = "ali.h@example.com", Text = "مطالب زندگی سالم خیلی لازم بود، مرسی.", Rating = 5, Status = StatusEnum.Confirmed, PostId = 4, CreatedAt = new DateTime(2025, 1, 16), UpdatedAt = new DateTime(2025, 1, 16) },
             new Comment { Id = 8, FullName = "مینا کریمی", Email = "mina.k@example.com", Text = "لطفاً منابع هم معرفی کنید.", Rating = 4, Status = StatusEnum.Pending, PostId = 4, CreatedAt = new DateTime(2025, 1, 16), UpdatedAt = new DateTime(2025, 1, 16) },

             // برای پست 5
             new Comment { Id = 9, FullName = "کاربر عاشق کتاب", Email = "booklover@example.com", Text = "کتاب‌ها عالی هستند، لطفاً نقد هم بزنید.", Rating = 5, Status = StatusEnum.Confirmed, PostId = 5, CreatedAt = new DateTime(2025, 1, 17), UpdatedAt = new DateTime(2025, 1, 17) },
             new Comment { Id = 10, FullName = "شهرام رضائی", Email = "shahram@example.com", Text = "کتاب‌ها آشنا نبودند، لطفاً منابع انگلیسی هم معرفی کنید.", Rating = 3, Status = StatusEnum.Pending, PostId = 5, CreatedAt = new DateTime(2025, 1, 17), UpdatedAt = new DateTime(2025, 1, 17) },

             // برای پست 6
             new Comment { Id = 11, FullName = "مریم نعمتی", Email = "maryam.n@example.com", Text = "مفید بود، به نظرم ادامه این موضوع هم مهم است.", Rating = 4, Status = StatusEnum.Confirmed, PostId = 6, CreatedAt = new DateTime(2025, 1, 18), UpdatedAt = new DateTime(2025, 1, 18) },

             // برای پست 8
             new Comment { Id = 13, FullName = "مسافر مشتاق", Email = "traveller@example.com", Text = "مطلب خیلی مفید بود — امیدوارم به زودی سفر کنم!", Rating = 5, Status = StatusEnum.Confirmed, PostId = 8, CreatedAt = new DateTime(2025, 1, 20), UpdatedAt = new DateTime(2025, 1, 20) },

             // برای پست 9
             new Comment { Id = 15, FullName = "کاربر آرامش", Email = "peace@example.com", Text = "خیلی خوب — نیاز امروز بود.", Rating = 5, Status = StatusEnum.Confirmed, PostId = 9, CreatedAt = new DateTime(2025, 1, 21), UpdatedAt = new DateTime(2025, 1, 21) },

             // برای پست 10
             new Comment { Id = 16, FullName = "توسعه‌دهنده تازه‌کار", Email = "devnew@example.com", Text = "Blazor جالب است — آیا آموزش کامل دارید؟", Rating = 5, Status = StatusEnum.Pending, PostId = 10, CreatedAt = new DateTime(2025, 1, 22), UpdatedAt = new DateTime(2025, 1, 22) }
         );
        }
    }
}
