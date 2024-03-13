using JIRA.Shared;
using JIRA.Shared.Entity;

namespace JIRA.Server.Domain.Repositories.Abstract
{
    public interface IProjectAsigneeRepository
    {

        public void Add(List<ProjectAsignee> projectAsignees);
    }
}
