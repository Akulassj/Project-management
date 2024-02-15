using Microsoft.AspNetCore.Identity;

namespace JIRA.Server.Domain.Entity
{
    //public class Developer
    //{
    //    public Guid Id { get; set; }
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public string Position { get; set; }
    //}
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Position { get; set; }
        public ICollection<Comment> Comments { get; set; } // Навигационное свойство для связи с таблицей Comment
    }
}
