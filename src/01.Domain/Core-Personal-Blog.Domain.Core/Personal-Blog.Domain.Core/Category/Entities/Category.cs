using Personal_Blog.Domain.Core._common;

namespace Personal_Blog.Domain.Core.Category.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public int AuthorId { get; set; }
        public Author.Entities.Author Author { get; set; }

        public List<Post.Entities.Post> Posts { get; set; } = new List<Post.Entities.Post>();
    }
}
