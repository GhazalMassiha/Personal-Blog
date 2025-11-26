using Personal_Blog.Domain.Core._common;

namespace Personal_Blog.Domain.Core.Post.Entities
{
    public class Post : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; }

        public int AuthorId { get; set; }
        public Author.Entities.Author Author { get; set; }

        public int CategoryId { get; set; }
        public Category.Entities.Category Category { get; set; }
    }
}
