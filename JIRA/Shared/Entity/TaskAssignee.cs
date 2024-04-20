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
        public Guid ProjectTaskId { get; set; }
        public Guid UserId { get; set; }
       

        public TaskAssignee() => Id = Guid.NewGuid();
    }
}
