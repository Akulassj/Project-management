namespace JIRA.Server.Domain.Entity
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid TaskId { get; set; }
        public Guid DeveloperId { get; set; }
        public Task Task { get; set; } // Навигационное свойство для связи с таблицей Task
        public User Developer { get; set; } // Навигационное свойство для связи с таблицей User

        public Comment() 
        { 
            CreatedAt = DateTime.Now;
        }
    }
}
