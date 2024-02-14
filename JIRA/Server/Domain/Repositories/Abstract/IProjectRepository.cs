using JIRA.Server.Domain.Entity;

namespace JIRA.Server.Domain.Repositories.Abstract
{
    public interface IProjectRepository
    {
        public List<Project> GetAllProjects();
        public List<Developer> GetAssignedDevelopers(Guid projectId);
        public List<Job> GetAssignedTasks(Guid developerId);
    }
}
