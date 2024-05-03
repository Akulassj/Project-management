using JIRA.Shared;
using JIRA.Shared.Entity;
using Microsoft.EntityFrameworkCore;
namespace JIRA.Server.Domain.Repositories.Abstract
{
    public interface ITaskAssigneeRepository
    {
      public  TaskAssignee GetTaskAssigneeById(Guid id);
      public  List<TaskAssignee> GetTaskAssigneesByUserId(Guid userId);
       public void Add(List<TaskAssignee> taskAsignees);
        public User GetTaskAssigneeUsers(Guid projectTaskId);
        //public void UpdateTaskAssignees(Guid projectId, List<ProjectTaskUsersModel> taskUsers);
        public void Update(ProjectTaskUsersModel taskAsignee);

        public bool GetActiveStatusByTaskId(Guid taskId);
    }
}
