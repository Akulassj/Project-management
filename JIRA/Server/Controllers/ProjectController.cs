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
        public IActionResult GetAllJobs()
        {
            var jobs = dataManager.JobRepository.GetAllJobs();
            return Ok(jobs);
        }
        [HttpGet]
        public IActionResult GetJobsByDate(DateTime date)
        {
            try
            {
                // Приведение даты к началу дня
                date = date.Date;

                var jobs = dataManager.JobRepository.GetJobsByDate(date);
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
