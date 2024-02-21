namespace JIRA.Shared.Entity
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid JobId { get; set; }
        public Guid UserId { get; set; } // Изменено на тип Guid
        public Job Job { get; set; }
        public User User { get; set; }

        public Comment()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }

}
