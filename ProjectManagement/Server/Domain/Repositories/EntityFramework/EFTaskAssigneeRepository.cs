using ProjectManagement.Server.Domain.Repositories.Abstract;
using ProjectManagement.Shared;
using ProjectManagement.Shared.Domain;
using ProjectManagement.Shared.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace ProjectManagement.Server.Domain.Repositories.EntityFramework
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
        public User GetTaskAssigneeUsers(Guid projectTaskId)
        {
            return context.TaskAssignees
                          .Where(t => t.ProjectTaskId == projectTaskId)
                          .Select(t => t.UserId)
                          .SelectMany(u => context.Users.Where(user => user.Id == u))
                          .FirstOrDefault();
        }
        
            //public void Update(ProjectTaskUsersModel task)
            //{
            //    context.ProjectTasks.Entry(task.ProjectTask).State = EntityState.Modified;

            //    //ToDO : Change update method
            //    var t = context.TaskAssignees.Where(ta => ta.ProjectTaskId == task.ProjectTask.Id).FirstOrDefault();
            //    context.TaskAssignees.Remove(t);
            //    context.SaveChanges();
            //    t.UserId = task.AssignedUser.Id;
            //    context.Add(t);
            //    context.SaveChanges();
            //}
        public void Update(ProjectTaskUsersModel task)
        {
            // Обновление данных по задаче
            

            // Получение записи TaskAssignees для данной задачи
            var taskAssignee = context.TaskAssignees
                                    .FirstOrDefault(ta => ta.ProjectTaskId == task.ProjectTask.Id);

            if (taskAssignee != null)
            {
                // Изменение значений свойств объекта TaskAssignees
                taskAssignee.UserId = task.AssignedUser.Id;
                
                // Изменение состояния объекта TaskAssignees на EntityState.Modified
                context.Entry(taskAssignee).State = EntityState.Modified;
            }

            // Сохранение изменений в базе данных
            context.SaveChanges();
        }

        public bool GetActiveStatusByTaskId(Guid taskId)
        {
            return context.TaskAssignees.FirstOrDefault(ta => ta.ProjectTaskId == taskId).InActive;
        }

        public void DeleteTaskAssignees(Guid taskId)
        {
            var taskAssignees = context.TaskAssignees.Where(ta => ta.ProjectTaskId == taskId).ToList();
            context.TaskAssignees.RemoveRange(taskAssignees);
            context.SaveChanges();
        }

        public TaskAssignee GetTaskAssigneeByTaskId(Guid taskId)
        {
            return context.TaskAssignees.FirstOrDefault(ta => ta.ProjectTaskId == taskId);
        }
    }
}
