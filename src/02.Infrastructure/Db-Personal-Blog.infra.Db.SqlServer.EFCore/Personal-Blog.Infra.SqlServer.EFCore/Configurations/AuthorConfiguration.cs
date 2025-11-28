using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Personal_Blog.Domain.Core.Author.Entities;

namespace Personal_Blog.Infra.SqlServer.EFCore.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Authors");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Username).IsUnique();

            builder.Property(x => x.Username)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.ImageUrl)
                .HasMaxLength(500);

            builder.Property(u => u.CreatedAt)
                .HasDefaultValueSql("GetDate()")
                .ValueGeneratedOnAdd();

            builder.HasMany(c => c.Categories)
                .WithOne(a => a.Author)
                .HasForeignKey(a => a.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Posts)
                .WithOne(a => a.Author)
                .HasForeignKey(a => a.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(
               new Author { Id = 1, Username = "ghazal", Password = "passGhazal", Email = "ghazal@gmail.com", FirstName = "غزل", LastName = "مسیحا", ImageUrl = null },

               new Author { Id = 2, Username = "mina", Password = "passMina", Email = "mina@gmail.com", FirstName = "مینا", LastName = "محمدی", ImageUrl = null },

               new Author { Id = 3, Username = "reza", Password = "passReza", Email = "reza@gmail.com", FirstName = "رضا", LastName = "رضایی", ImageUrl = null },

               new Author {Id = 4, Username = "sara", Password = "passSara", Email = "sara@gmail.com", FirstName = "سارا", LastName = "کاظمی", ImageUrl = null },

               new Author {Id = 5, Username = "hossein", Password = "passHossein", Email = "hossein@gmail.com", FirstName = "حسین", LastName = "رضوانی", ImageUrl = null }
           );
        }
    }
}
