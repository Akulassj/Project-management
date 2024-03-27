﻿using JIRA.Server.Domain.Repositories.Abstract;
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
        public List<User> SearchUsersByUsernameInProject(Guid projectId, string username)
        {
            return context.ProjectAsignees
                          .Where(pa => pa.ProjectId == projectId)
                          .Join(context.Users,
                                pa => pa.UserId,
                                u => u.Id,
                                (pa, u) => u)
                          .Where(u => u.UserName.Contains(username))
                          .ToList();
        }
    }
}
