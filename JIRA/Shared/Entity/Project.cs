using System;

namespace JIRA.Shared.Entity
{
    //public class Project
    //{
    //    public Guid Id { get; set; }
    //    public string Name { get; set; } = string.Empty;
    //    public string Description { get; set; } = string.Empty;
    //    public DateTime CreatedAt { get; set; }
    //}
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public ICollection<Job> Jobs { get; set; } // Навигационное свойство для связи с таблицей Job

        
        public Project()
        {
            CreatedAt = DateTime.UtcNow;
        }

    }
}
