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
        [HttpGet("{projectId}/jobs")]
        public IActionResult GetJobsByProjectId(Guid projectId)
        {
            var jobs = dataManager.ProjectRepository.GetJobsByProjectId(projectId);
            return Ok(jobs);
        }

        [HttpGet("{jobId}/comments")]
        public IActionResult GetCommentsByJobID(Guid jobId)
        {
            var comments = dataManager.ProjectRepository.GetCommentsByJobID(jobId);
            return Ok(comments);
        }

        [HttpGet("{jobId}/attachments")]
        public IActionResult GetAttachmentsByJobID(Guid jobId)
        {
            var attachments = dataManager.ProjectRepository.GetAttachmentsByJobID(jobId);
            return Ok(attachments);
        }

        [HttpGet("{developerId}/assigned-tasks")]
        public IActionResult GetAssignedTasksByDeveloperId(Guid developerId)
        {
            var tasks = dataManager.ProjectRepository.GetAssignedTasksByDeveloperId(developerId);
            return Ok(tasks);
        }

        [HttpGet("{projectId}/assigned-tasks")]
        public IActionResult GetAssignedTasksByProjectId(Guid projectId)
        {
            var tasks = dataManager.ProjectRepository.GetAssignedTasksByProjectId(projectId);
            return Ok(tasks);
        }

    }
}
