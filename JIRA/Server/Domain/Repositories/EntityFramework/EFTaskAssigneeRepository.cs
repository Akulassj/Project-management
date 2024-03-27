using JIRA.Server.Domain.Repositories.Abstract;
using JIRA.Shared.Domain;
using JIRA.Shared.Entity;
using Microsoft.EntityFrameworkCore;
namespace JIRA.Server.Domain.Repositories.EntityFramework
{
    public class EFTaskAssigneeRepository : ITaskAssigneeRepository
    {

        private readonly ProjectManagementContext context;

        public EFTaskAssigneeRepository(ProjectManagementContext context)
        {
            this.context = context;
        }

        public TaskAssignee GetTaskAssigneeById(Guid id)
        {
            return context.TaskAssignees.FirstOrDefault(t => t.Id == id);
        }

        public List<TaskAssignee> GetTaskAssigneesByUserId(Guid userId)
        {
            return context.TaskAssignees.Where(t => t.UserId == userId).ToList();
        }
        public void Add(List<TaskAssignee> taskAsignees)
        {
            context.TaskAssignees.AddRange(taskAsignees);
            context.SaveChanges();
        }
    }
}
