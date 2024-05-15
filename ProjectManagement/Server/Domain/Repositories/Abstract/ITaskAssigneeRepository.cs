using ProjectManagement.Shared;
using ProjectManagement.Shared.Entity;
using Microsoft.EntityFrameworkCore;
namespace ProjectManagement.Server.Domain.Repositories.Abstract
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
        public void DeleteTaskAssignees(Guid taskId);
        public TaskAssignee GetTaskAssigneeByTaskId(Guid taskId);
    }
}
