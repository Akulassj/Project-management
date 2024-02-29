using JIRA.Shared.Entity;
using System.Xml.Linq;

namespace JIRA.Server.Domain.Repositories.Abstract
{
    public interface IProjectRepository
    {
        public List<Project> GetAllProjects();
        public Project GetProjectById(Guid id);
        
        public List<Project> GetProjectsByUserName(string userName);

        public void Add(Project project);
        public void Delete(Guid id);

        public void Update(Project project);
    }
}
