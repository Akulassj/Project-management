using JIRA.Server.Domain.Repositories.Abstract;
using JIRA.Shared.Domain;
using JIRA.Shared.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JIRA.Server.Domain.Repositories.EntityFramework
{
    public class EFProjectAsigneeRepository : IProjectAsigneeRepository
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
        public List<ProjectAsignee> GetProjectAsigneesByProjectId(Guid projectId)
        {
            return context.ProjectAsignees.Where(pa => pa.ProjectId == projectId).ToList();
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

        public void UpdateProjectAsignees(Guid projectId, List<User> users)
        {
            var oldUsersIds = context.ProjectAsignees.Where(pa => pa.ProjectId == projectId).Select(pa => pa.UserId).SelectMany(id => context.Users.Where(user => user.Id == id)).Select(u=>u.Id).ToList();
            var tasks = context.ProjectTasks.Where(task => task.ProjectId == projectId).Select(task => task.Id).ToList();
            foreach (var user in oldUsersIds)
            {
                if (!users.Select(u => u.Id).Contains(user))
                {
                    var asignees = context.TaskAssignees.Where(ta => ta.UserId == user && tasks.Contains(ta.ProjectTaskId)).ToList();
                    foreach (var ta in asignees)
                    {
                        ta.InActive = true;
                        context.TaskAssignees.Entry(ta).State = EntityState.Modified;
                    }
                }
            }

            var projectAsignees = context.ProjectAsignees.Where(pa => pa.ProjectId == projectId).ToList();
            var creatorUserId = context.ProjectAsignees.Where(pa => pa.IsCreator && pa.ProjectId == projectId).FirstOrDefault().UserId;
            context.ProjectAsignees.RemoveRange(projectAsignees);
            context.ProjectAsignees.AddRange(users.Select(u => new ProjectAsignee() { ProjectId = projectId, UserId = u.Id, IsCreator = u.Id == creatorUserId }));
            context.SaveChanges();
        }

        public string GetProjectCreator(Guid projectId)
        {
            var creator = context.ProjectAsignees.FirstOrDefault(pa => pa.ProjectId == projectId && pa.IsCreator).UserId;
            var username = context.Users.Where(user => user.Id == creator).FirstOrDefault().UserName;
            return username;
        }
      
        public void DeleteProjectAsignees(Guid projectId)
        {
            var asignees = context.ProjectAsignees.Where(pa => pa.ProjectId == projectId).ToList();
            context.ProjectAsignees.RemoveRange(asignees);

            



            context.SaveChanges();
        }
    }
}
