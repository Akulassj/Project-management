using JIRA.Server.Domain.Repositories.Abstract;
using JIRA.Shared.Domain;
using JIRA.Shared.Entity;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace JIRA.Server.Domain.Repositories.EntityFramework
{
    public class EFProjectTaskRepository : IProjectTaskRepository
    {
        private readonly ProjectManagementContext context;
        public EFProjectTaskRepository(ProjectManagementContext context)
        {
            this.context = context;
        }
        public ProjectTask GetProjectTaskById(Guid id)
        {
            return context.ProjectTasks.FirstOrDefault(j => j.Id == id);
        }

        public List<ProjectTask> GetProjectTasksByProjectId(Guid projectId)
        {
            return context.ProjectTasks.Where(j => j.ProjectId == projectId).ToList();
        }
        public List<ProjectTask> GetAllProjectTasks()
        {
            return context.ProjectTasks.ToList();
        }

        public List<ProjectTask> GetProjectProjectTasksByDate(Guid projectID, DateTime date)
        {
            DateTime startDate = date.Date.ToUniversalTime(); 
            DateTime endDate = startDate.AddDays(1).AddSeconds(-1); 

            return context.ProjectTasks
                .Where(j => j.CreatedAt >= startDate && j.CreatedAt <= endDate && j.ProjectId == projectID)
                .ToList();
        }
        public List<ProjectTaskStatus> GetProjectTaskStatuses()
        {
            return context.ProjectTasks.Select(j => Enum.Parse<ProjectTaskStatus>(j.Status)).Distinct().ToList();
        }
        public List<ProjectTask> GetProjectTasksByUserName(string userName)
        {
            var userId = context.Users.FirstOrDefault(u => u.UserName == userName).Id;
            if (userId == null)
            {
                throw new Exception($"Пользователь с именем {userName} не найден.");
            }

            return context.TaskAssignees
                .Where(ta => ta.UserId == userId)
                .Select(ta => ta.ProjectTaskId)
                .Distinct()
                .SelectMany(projectTaskId => context.ProjectTasks.Where(j => j.Id == projectTaskId))
                .Distinct()
                .ToList();
        }
        public void Add(ProjectTask projectTask)
        {
            context.ProjectTasks.Add(projectTask);
            context.SaveChanges();
        }

        public void Update(ProjectTask projectTask)
        {
            context.ProjectTasks.Entry(projectTask).State = EntityState.Modified;
            context.SaveChanges();
        }
        public List<ProjectTask> GetUncompletedTasksForUserAndProject(Guid userId, Guid projectId)
        {
            return context.ProjectTasks
                .Where(task => task.ProjectId == projectId && task.Status != "Completed")
                .ToList();
        }



    }
}
