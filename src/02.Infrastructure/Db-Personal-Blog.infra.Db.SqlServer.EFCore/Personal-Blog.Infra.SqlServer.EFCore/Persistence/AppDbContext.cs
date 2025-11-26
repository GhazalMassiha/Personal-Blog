using Microsoft.EntityFrameworkCore;
using Personal_Blog.Domain.Core.Author.Entities;
using Personal_Blog.Domain.Core.Category.Entities;
using Personal_Blog.Domain.Core.Post.Entities;
using Personal_Blog.Infra.SqlServer.EFCore.Configurations;

namespace Personal_Blog.Infra.SqlServer.EFCore.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
     : base(options)
        { }

        public DbSet<Author> Authors { get; set; }  
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
