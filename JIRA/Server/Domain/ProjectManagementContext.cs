using JIRA.Server.Domain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JIRA.Shared.Domain
{
    public class ProjectManagementContext:IdentityDbContext
    {
        public ProjectManagementContext(DbContextOptions<ProjectManagementContext> options) : base(options) 
        { 

        } 

        public DbSet<Project> Projects { get; set; }
        public DbSet<Developer> Developers { get; set; }
      
        public DbSet<Job> Tasks { get; set; }
        public DbSet<TaskAssignee> TaskAssignees { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
    }
}
