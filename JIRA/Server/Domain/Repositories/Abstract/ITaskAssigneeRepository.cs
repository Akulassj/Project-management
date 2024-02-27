using JIRA.Shared.Entity;
using Microsoft.EntityFrameworkCore;
namespace JIRA.Server.Domain.Repositories.Abstract
{
    public interface ITaskAssigneeRepository
    {
      public  TaskAssignee GetTaskAssigneeById(Guid id);
      public  List<TaskAssignee> GetTaskAssigneesByUserId(Guid userId);
    }
}
