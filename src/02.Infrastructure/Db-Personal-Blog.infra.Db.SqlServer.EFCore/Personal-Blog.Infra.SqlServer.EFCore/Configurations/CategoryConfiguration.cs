using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Personal_Blog.Domain.Core.Category.Entities;

namespace Personal_Blog.Infra.SqlServer.EFCore.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(u => u.CreatedAt)
                .HasDefaultValueSql("GetDate()")
                .ValueGeneratedOnAdd();

            builder.HasMany(c => c.Posts)
                .WithOne(a => a.Category)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Author)
                .WithMany(a => a.Categories)
                .HasForeignKey(a => a.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
               new Category { Id = 1, Name = "تکنولوژی", AuthorId = 1 },
               new Category { Id = 2, Name = "سفر", AuthorId = 1 },
               new Category { Id = 3, Name = "سبک زندگی", AuthorId = 2 },
               new Category { Id = 4, Name = "برنامه‌نویسی .NET", AuthorId = 3},
               new Category { Id = 5, Name = "کتاب و مطالعه", AuthorId = 4},
               new Category { Id = 6, Name = "سلامت و ورزش", AuthorId = 5 }
           );
        }
    }
}
