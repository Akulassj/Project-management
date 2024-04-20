
namespace JIRA.Shared.Entity
{

    public class ProjectTask
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid ProjectId { get; set; }


        public ProjectTask()
        {
            CreatedAt = DateTime.UtcNow.ToLocalTime();
            Id = Guid.NewGuid();
        }

    }
}
