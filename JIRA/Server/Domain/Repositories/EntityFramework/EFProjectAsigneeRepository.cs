using JIRA.Server.Domain.Repositories.Abstract;
using JIRA.Shared.Domain;
using JIRA.Shared.Entity;
using Microsoft.EntityFrameworkCore;

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
        public List<User> SearchUsersByUsernameInProject(Guid projectId, string username)
        {
            return context.ProjectAsignees
                          .Where(pa => pa.ProjectId == projectId)
                          .Join(context.Users,
                                pa => pa.UserId,
                                u => u.Id,
                                (pa, u) => u)
                          .Where(u => u.UserName.ToLower().Contains(username.ToLower()))
                          .ToList();
        }
        public ProjectAsignee GetProjectAsigneeByProjectId(Guid projectId)
        {
            return context.ProjectAsignees.FirstOrDefault(pa => pa.ProjectId == projectId && pa.IsCreator);
        }
        public void AddProjectUser(Guid projectId, Guid userId)
        {
            
            if (!context.ProjectAsignees.Any(pa => pa.ProjectId == projectId && pa.UserId == userId))
            {
                context.ProjectAsignees.Add(new ProjectAsignee { ProjectId = projectId, UserId = userId });
                context.SaveChanges();
            }
        }

        public void RemoveProjectUser(Guid projectId, Guid userId)
        {
            var projectAsignee = context.ProjectAsignees
                .FirstOrDefault(pa => pa.ProjectId == projectId && pa.UserId == userId);

            if (projectAsignee != null)
            {
                context.ProjectAsignees.Remove(projectAsignee);
                context.SaveChanges();
            }
        }
        

    }
}
