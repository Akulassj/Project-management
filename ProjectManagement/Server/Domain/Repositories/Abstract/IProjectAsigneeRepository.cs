using ProjectManagement.Shared;
using ProjectManagement.Shared.Entity;

namespace ProjectManagement.Server.Domain.Repositories.Abstract
{
    public interface IProjectAsigneeRepository
    {
        public List<User> SearchUsersByUsernameInProject(Guid projectId, string username);
        public void Add(List<ProjectAsignee> projectAsignees);
        public List<ProjectAsignee> GetProjectAsigneesByProjectId(Guid projectId);
        public void UpdateProjectAsignees(Guid projectId, List<User> users);
        public void AddProjectUser(Guid projectId, Guid userId);
        public void RemoveProjectUser(Guid projectId, Guid userId);
        public string GetProjectCreator(Guid projectId);
        public void DeleteProjectAsignees(Guid projectId);
       
    }
}
