using JIRA.Server.Domain.Entity;
using JIRA.Server.Domain.Repositories.Abstract;
using JIRA.Shared.Domain;
using Microsoft.EntityFrameworkCore;

namespace JIRA.Server.Domain.Repositories.EntityFramework
{
    public class EFProjectRepository: IProjectRepository
    {
        private readonly ProjectManagementContext context;

        public EFProjectRepository(ProjectManagementContext context)
        {
            this.context = context;
        }
        public List<Project> GetAllProjects()
        {
            return context.Projects.ToList();
        }

        public List<Developer> GetAssignedDevelopers(Guid projectId)
        {
            return context.Developers
                .Where(developer => developer.ProjectId == projectId)
                .ToList();
        }

        public List<Job> GetAssignedTasks(Guid developerId)
        {
            return context.TaskAssignees
                .Where(ta => ta.DeveloperId == developerId)
                .Select(ta => ta.TaskId) // Выбираем только идентификаторы задач
                .SelectMany(taskId => context.Tasks.Where(j => j.Id == taskId)) // Получаем соответствующие задачи
                .ToList();
        }

    }
}
