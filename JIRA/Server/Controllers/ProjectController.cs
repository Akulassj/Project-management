using JIRA.Server.Domain;
using Microsoft.AspNetCore.Mvc;

namespace JIRA.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly DataManager dataManager;

        public ProjectController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        [HttpGet("all")]
        public IActionResult GetAllProjects()
        {
            var projects = dataManager.ProjectRepository.GetAllProjects();
            return Ok(projects);
        }

        [HttpGet("{projectId}/developers")]
        public IActionResult GetAssignedDevelopers(Guid projectId)
        {
            var developers = dataManager.ProjectRepository.GetAssignedDevelopers(projectId);
            return Ok(developers);
        }

        [HttpGet("{developerId}/tasks")]
        public IActionResult GetAssignedTasks(Guid developerId)
        {
            var tasks = dataManager.ProjectRepository.GetAssignedTasks(developerId);
            return Ok(tasks);
        }
    }
}
