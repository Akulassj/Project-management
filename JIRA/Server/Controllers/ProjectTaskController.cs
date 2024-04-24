using JIRA.Server.Domain;
using Microsoft.AspNetCore.Mvc;

using JIRA.Shared;
using JIRA.Shared.Entity;
using Microsoft.Extensions.Logging;
namespace JIRA.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectTaskController : Controller
    {
        private readonly DataManager dataManager;

        public ProjectTaskController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        [HttpPost]
        public IActionResult AddProjectTask(TasksAssigneeViewModel tasksAssignees)
        {
            dataManager.ProjectTaskRepository.Add(tasksAssignees.ProjectTask);
            dataManager.TaskAssigneeRepository.Add(tasksAssignees.TaskAssignees);
            return Ok();
        }

        [HttpGet]
        public IActionResult SearchUsersInProject(Guid projectId, string username)
        {
            try
            {
                var users = dataManager.ProjectAsigneeRepository.SearchUsersByUsernameInProject(projectId, username);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetTaskAssigneesByUserId(Guid userId)
        {
            try
            {
                var taskAssignees = dataManager.TaskAssigneeRepository.GetTaskAssigneesByUserId(userId);
                return Ok(taskAssignees);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public IActionResult GetTaskAssigneeUsers(Guid projectTaskId)
        {
            try
            {
                var users = dataManager.TaskAssigneeRepository.GetTaskAssigneeUsers(projectTaskId);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public IActionResult UpdateProjectTaskStatus(ProjectTask projectTask)
        {
            try
            {
                
                var existingProjectTask = dataManager.ProjectTaskRepository.GetProjectTaskById(projectTask.Id);

                if (existingProjectTask == null)
                {
                    return NotFound();
                }

               
                existingProjectTask.Status = projectTask.Status;

                
                dataManager.ProjectTaskRepository.Update(existingProjectTask);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        
        public IActionResult GetUncompletedTasksForUserAndProject(Guid userId, Guid projectId)
        {
            var tasks = dataManager.ProjectTaskRepository.GetUncompletedTasksForUserAndProject(userId, projectId);
            return Ok(tasks);
        }

        [HttpPut]
        public IActionResult Update(ProjectTask projectTask)
        {
            dataManager.ProjectTaskRepository.Update(projectTask);
            return Ok();
        }
        //[HttpPut]
        //public IActionResult UpdateTaskAssignees(Guid projectId, List<ProjectTaskUsersModel> taskUsers)
        //{
        //    try
        //    {
        //        dataManager.TaskAssigneeRepository.UpdateTaskAssignees(projectId, taskUsers);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Ошибка при обновлении назначенных пользователей по задачам: {ex.Message}");
        //    }
        //}

    }
}
