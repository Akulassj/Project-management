
namespace ProjectManagement.Shared.Entity
{

    public class ProjectTask
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public Guid ProjectId { get; set; }
        public Project? Project { get; set; }

        public ProjectTask()
        {
            CreatedAt = DateTime.UtcNow;
            CompletedAt = DateTime.UtcNow.AddDays(7);
            Id = Guid.NewGuid();
        }

    }
}
