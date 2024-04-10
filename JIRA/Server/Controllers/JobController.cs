using JIRA.Server.Domain;
using Microsoft.AspNetCore.Mvc;

using JIRA.Shared;
using JIRA.Shared.Entity;
using Microsoft.Extensions.Logging;
namespace JIRA.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class JobController : Controller
    {
        private readonly DataManager dataManager;

        public JobController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        [HttpPost]
        public IActionResult AddJob(TasksAssigneeViewModel tasksAssignees)
        {
            dataManager.JobRepository.Add(tasksAssignees.Job);
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
        public IActionResult GetTaskAssigneeUsers(Guid jobId)
        {
            try
            {
                var users = dataManager.TaskAssigneeRepository.GetTaskAssigneeUsers(jobId);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        public IActionResult UpdateJobStatus(Job job)
        {
            try
            {
                // Поиск задачи по идентификатору
                var existingJob = dataManager.JobRepository.GetJobById(job.Id);

                if (existingJob == null)
                {
                    return NotFound();
                }

                // Обновление статуса задачи
                existingJob.Status = job.Status;

                // Сохранение изменений
                dataManager.JobRepository.Update(existingJob);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



    }
}
