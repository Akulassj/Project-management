using Microsoft.AspNetCore.Identity;

namespace JIRA.Shared.Entity
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Position { get; set; }
        public string? TelegramChatId { get; set; }
        public User() => Id = Guid.NewGuid();
    }
}
