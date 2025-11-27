using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Personal_Blog.Domain.Core.Comment.Entities;

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
        }
    }
}
