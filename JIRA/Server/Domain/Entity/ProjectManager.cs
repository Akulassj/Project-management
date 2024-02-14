namespace JIRA.Server.Domain.Entity
{
    public class ProjectManager
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty; 
       
        public Guid ProjectId { get; set; } // Внешний ключ для связи с проектом
        //public Project Project { get; set; } // Навигационное свойство к проекту менеджера
    }
}
