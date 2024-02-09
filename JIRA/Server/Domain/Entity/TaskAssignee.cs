namespace JIRA.Server.Domain.Entity
{
    public class TaskAssignee
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public Guid DeveloperId { get; set; }
    }
}
