﻿using JIRA.Server.Domain.Repositories.Abstract;
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

        public void UpdateProjectAsignees(Guid projectId, List<User> users)
        {
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
    }
}
