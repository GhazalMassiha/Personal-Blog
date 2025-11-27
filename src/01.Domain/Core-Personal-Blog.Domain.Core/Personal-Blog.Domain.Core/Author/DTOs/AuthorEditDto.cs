namespace Personal_Blog.Domain.Core.Author.DTOs
{
    public class AuthorEditDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ImageUrl { get; set; }
    }
}
