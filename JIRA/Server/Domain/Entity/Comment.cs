namespace JIRA.Server.Domain.Entity
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid TaskId { get; set; }
        public Guid DeveloperId { get; set; }
    }
}
