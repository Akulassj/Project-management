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
        public ProjectTask GetProjectTaskById(Guid projectId)
        {
            return context.ProjectTasks.FirstOrDefault(j => j.Id == projectId);
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
            var task = context.ProjectTasks.Where(j => j.Id == projectTask.Id).FirstOrDefault();
            if (task != null)
            {
                task.Description = projectTask.Description;
                task.Name = projectTask.Name;
                task.Status = projectTask.Status;

                context.ProjectTasks.Entry(task).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public List<ProjectTask> GetUncompletedTasksForUserAndProject(Guid userId, Guid projectId)
        {
            return context.ProjectTasks
                .Where(task => task.ProjectId == projectId && task.Status != "Completed")
                .ToList();
        }


        public void DeleteTask(Guid taskId)
        {
            var taskAssignees = context.TaskAssignees.Where(ta => ta.ProjectTaskId == taskId).ToList();
            context.TaskAssignees.RemoveRange(taskAssignees);

            var comments = context.Comments.Where(c => c.ProjectTaskId == taskId).ToList();
            context.Comments.RemoveRange(comments);

            var task = context.ProjectTasks.FirstOrDefault(t => t.Id == taskId);
            if (task != null)
            {
                context.ProjectTasks.Remove(task);
               
            }
            var attachments = context.Attachments.Where(a => a.ProjectTaskId == taskId).ToList();
            context.Attachments.RemoveRange(attachments);
            context.SaveChanges();
        }

    }
}
