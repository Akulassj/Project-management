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

        //[HttpPost]
        //public IActionResult AddJob([FromBody] dynamic requestData)
        //{
        //    try
        //    {
        //        //Извлекаем данные о задаче и назначенных пользователях из запроса
        //       var job = requestData.Job.ToObject<Job>();
        //        var taskAssignees = requestData.TaskAssignees.ToObject<List<TaskAssignee>>();

        //        //Добавляем задачу в репозиторий
        //        dataManager.JobRepository.Add(job);

        //       // Добавляем назначенных пользователей в репозиторий
        //        dataManager.TaskAssigneeRepository.Add(taskAssignees);

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //       // Обработка ошибок
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}



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
