namespace JIRA.Shared.Entity
{
    //public class TaskAssignee
    //{
    //    public Guid Id { get; set; }
    //    public Guid TaskId { get; set; }
    //    public Guid DeveloperId { get; set; }
    //}
    public class TaskAssignee
    {
        public Guid Id { get; set; }
        public Guid JobId { get; set; }
        public Guid UserId { get; set; }
        public Job Job { get; set; } // Навигационное свойство для связи с таблицей Task
        public User User { get; set; } // Навигационное свойство для связи с таблицей User

        public TaskAssignee() => Id = Guid.NewGuid();
    }
}
