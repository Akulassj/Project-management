using ProjectManagement.Shared.Entity;
using System.Xml.Linq;

namespace ProjectManagement.Server.Domain.Repositories.Abstract
{
    public interface IProjectRepository
    {
        public List<Project> GetAllProjects();
        public Project GetProjectById(Guid id);
        
        public List<Project> GetProjectsByUserName(string userName);

        public List<User> GetAsigneeProjectUsers(Guid projectId);

        public void Add(Project project);

        public void DeleteProject(Guid projectId);

        public void Update(Project project);
    }
}
