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

        [HttpGet]
        public IActionResult GetAllProjects()
        {
            var projects = dataManager.ProjectRepository.GetAllProjects();
            return Ok(projects);
        }

        [HttpGet]
        public IActionResult GetUserProjects(string userName)
        {
            var projects = dataManager.ProjectRepository.GetProjectsByUserName(userName);
            return Ok(projects);
        }

        [HttpGet]
        public IActionResult GetProject(Guid projectId)
        {
            var project = dataManager.ProjectRepository.GetProjectById(projectId);
            return Ok(project);
        }

        [HttpGet]
        public IActionResult GetProjectJobs(Guid projectId)
        {
            var projects = dataManager.JobRepository.GetJobsByProjectId(projectId);
            return Ok(projects);
        }

        [HttpGet]
        public IActionResult GetAllJobs()
        {
            var jobs = dataManager.JobRepository.GetAllJobs();
            return Ok(jobs);
        }

        [HttpGet]
        public IActionResult GetUserJobs(Guid userId)
        {
            var jobs = dataManager.JobRepository.GetAllJobs();
            return Ok(jobs);
        }

        [HttpGet]
        public IActionResult GetJobsByDate(Guid projectID, DateTime date)
        {
            try
            {
                // Приведение даты к началу дня
                date = date.Date;

                var jobs = dataManager.JobRepository.GetProjectJobsByDate(projectID, date);
                return Ok(jobs);
            }
            catch (Exception ex)
            {
                // Обработка ошибок, например, возврат статуса ошибки
                return StatusCode(500, $"Ошибка при получении заданий для даты: {ex.Message}");
            }
        }
        [HttpGet]
        public IActionResult GetJobStatuses()
        {
            var statuses = dataManager.JobRepository.GetJobStatuses();
            return Ok(statuses);
        }
    }
}
