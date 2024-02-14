namespace JIRA.Server.Domain.Entity
{
    public class Administrator
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; } 
        public string? LastName { get; set; }
       

        public List<Project> Projects { get; set; }
    }
}
