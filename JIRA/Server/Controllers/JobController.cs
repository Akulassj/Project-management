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

    }
}
