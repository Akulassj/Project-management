using System.ComponentModel.DataAnnotations;

namespace JIRA.Shared.Entity
{
    public class TaskAssignee
    {
        public Guid Id { get; set; }
        public Guid ProjectTaskId { get; set; }
        public Guid UserId { get; set; }
       

        public TaskAssignee() => Id = Guid.NewGuid();
    }
}
