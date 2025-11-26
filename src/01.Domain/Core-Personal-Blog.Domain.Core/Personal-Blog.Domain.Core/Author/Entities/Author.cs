using Personal_Blog.Domain.Core._common;

namespace Personal_Blog.Domain.Core.Author.Entities
{
    public class Author : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ImageUrl { get; set; }

        public List<Category.Entities.Category> Categories { get; set; } = new List<Category.Entities.Category>();  
        public List<Post.Entities.Post> Posts { get; set; } = new List<Post.Entities.Post>();
    }
}
