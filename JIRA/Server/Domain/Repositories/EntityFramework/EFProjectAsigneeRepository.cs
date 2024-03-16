using JIRA.Server.Domain.Repositories.Abstract;
using JIRA.Shared.Domain;
using JIRA.Shared.Entity;

namespace JIRA.Server.Domain.Repositories.EntityFramework
{
    public class EFProjectAsigneeRepository:IProjectAsigneeRepository
    {
        private readonly ProjectManagementContext context;

        public EFProjectAsigneeRepository(ProjectManagementContext context)
        {
            this.context = context;
        }

        public void Add(List<ProjectAsignee> projectAsignees)
        {
            context.ProjectAsignees.AddRange(projectAsignees);
            context.SaveChanges();
        }
    }
}
