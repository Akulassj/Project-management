namespace JIRA.Server.Domain.Entity
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
        public Guid TaskId { get; set; }
        
        public Guid DeveloperId { get; set; }

        //public Job Task { get; set; } // Навигационное свойство для связи с таблицей Task

        //public Developer Developer { get; set; } // Навигационное свойство для связи с таблицей Developer
    }
}
