namespace JIRA.Server.Domain.Entity
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
        public Guid AdministratorId { get; set; }

        //public Administrator Administrator { get; set; }

    }
}
