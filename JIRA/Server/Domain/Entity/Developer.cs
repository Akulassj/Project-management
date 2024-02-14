namespace JIRA.Server.Domain.Entity
{
    //public class Developer
    //{
    //    public Guid Id { get; set; }
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public string Position { get; set; }
    //}
    public class Developer
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public Guid ProjectId { get; set; }

    }
}
