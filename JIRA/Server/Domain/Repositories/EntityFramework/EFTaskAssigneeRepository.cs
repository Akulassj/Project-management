using JIRA.Server.Domain.Repositories.Abstract;
using JIRA.Shared;
using JIRA.Shared.Domain;
using JIRA.Shared.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
        public List<User> GetTaskAssigneeUsers(Guid projectTaskId)
        {
            return context.TaskAssignees
                          .Where(t => t.ProjectTaskId == projectTaskId)
                          .Select(t => t.UserId)
                          .SelectMany(u => context.Users.Where(user => user.Id == u))
                          .ToList();
        }
        //public void UpdateTaskAssignees(Guid projectId, List<ProjectTaskUsersModel> taskUsers)
        //{

        //    var projectTaskIds = taskUsers.Select(tu => tu.ProjectTask.Id).ToList();
        //    var taskAssignees = context.TaskAssignees.Where(ta => projectTaskIds.Contains(ta.ProjectTaskId)).ToList();
        //    context.TaskAssignees.RemoveRange(taskAssignees);


        //    foreach (var taskUser in taskUsers)
        //    {
        //        var taskAssigneeModels = taskUser.AssignedUsers.Select(user =>
        //            new TaskAssignee
        //            {
        //                ProjectTaskId = taskUser.ProjectTask.Id,
        //                UserId = user.Id
        //            }).ToList();

        //        context.TaskAssignees.AddRange(taskAssigneeModels);
        //    }

        //    context.SaveChanges();
        //}
        public void Update(ProjectTaskUsersModel task)
        {
            context.ProjectTasks.Entry(task.ProjectTask).State = EntityState.Modified;
            //ToDO : Change update method
            var t = context.TaskAssignees.Where(ta => ta.ProjectTaskId == task.ProjectTask.Id).FirstOrDefault();
            context.TaskAssignees.Remove(t);
            context.SaveChanges();
            t.UserId = task.AssignedUser.Id;
            context.Add(t);
            context.SaveChanges();
        }

    }
}
