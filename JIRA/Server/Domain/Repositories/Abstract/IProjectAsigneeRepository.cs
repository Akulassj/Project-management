using JIRA.Shared;
using JIRA.Shared.Entity;

namespace JIRA.Server.Domain.Repositories.Abstract
{
    public interface IProjectAsigneeRepository
    {
        public List<User> SearchUsersByUsernameInProject(Guid projectId, string username);
        public void Add(List<ProjectAsignee> projectAsignees);
        public ProjectAsignee GetProjectAsigneeByProjectId(Guid projectId);
        public void UpdateProjectAsignees(Guid projectId, List<User> users);
        public void AddProjectUser(Guid projectId, Guid userId);
        public void RemoveProjectUser(Guid projectId, Guid userId);
        public string GetProjectCreator(Guid projectId);
    }
}
