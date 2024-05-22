using ProjectManagement.Server.Domain;
using Microsoft.AspNetCore.Mvc;

using ProjectManagement.Shared;
using ProjectManagement.Shared.Entity;
using Microsoft.Extensions.Logging;
using ProjectManagement.Client.Pages;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
namespace ProjectManagement.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectTaskController : Controller
    {
        private readonly DataManager dataManager;
        private readonly IHttpClientFactory _httpClientFactory;
        public ProjectTaskController(DataManager dataManager, IHttpClientFactory httpClientFactory)
        {
            this.dataManager = dataManager;
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task <IActionResult> AddProjectTask(TasksAssigneeViewModel tasksAssignees)
        { // Получаешь taskassignee и добавлять пользователю, что он назначен на задачу
            var user = dataManager.UserRepository.GetUserById(tasksAssignees.TaskAssignees.First().UserId);

            var notification = new Notification() { RecieverName = user.UserName, IsReaded = false, Message = $"Вас добавили на задачу {tasksAssignees.ProjectTask.Name}" };
            dataManager.ProjectTaskRepository.Add(tasksAssignees.ProjectTask);
            dataManager.TaskAssigneeRepository.Add(tasksAssignees.TaskAssignees);
            var httpClient = _httpClientFactory.CreateClient("InternalApi");

            // Сериализация списка уведомлений в JSON
            var jsonNotifications = JsonConvert.SerializeObject(notification);

            // Создание содержимого запроса
            var content = new StringContent(jsonNotifications, Encoding.UTF8, "application/json");

            // Отправка запроса на сервер
            var response = await httpClient.PostAsync("api/notification/AddNotification", content);

            // Обработка ответа
            if (response.IsSuccessStatusCode)
            {
                // Действия при успешном запросе
                return Ok();
            }
            else
            {
                // Обработка ошибки
                return StatusCode((int)response.StatusCode);
            }
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
        public async Task<IActionResult> UpdateProjectTask(ProjectTaskUsersModel selectedTask)
        {
            var oldUserId = dataManager.TaskAssigneeRepository.GetTaskAssigneeByTaskId(selectedTask.ProjectTask.Id).UserId;
            var oldUser = dataManager.UserRepository.GetUserById(oldUserId);

            var user = dataManager.UserRepository.GetUserById(selectedTask.AssignedUser.Id);

            var notifications = new List<Notification>()
            {
                new Notification() { RecieverName = user.UserName, IsReaded = false, Message = $"Вас добавили на задачу {selectedTask.ProjectTask.Name}" },
                 new Notification() { RecieverName = oldUser.UserName, IsReaded = false, Message = $"Вас удалили с задачи {selectedTask.ProjectTask.Name}" },
        };

           
            dataManager.TaskAssigneeRepository.Update(selectedTask);
            dataManager.ProjectTaskRepository.Update(selectedTask.ProjectTask);
            //dataManager.AttachmentRepository.Update(selectedTask);
            var httpClient = _httpClientFactory.CreateClient("InternalApi");

            // Сериализация списка уведомлений в JSON
            var jsonNotifications = JsonConvert.SerializeObject(notifications);

            // Создание содержимого запроса
            var content = new StringContent(jsonNotifications, Encoding.UTF8, "application/json");

            // Отправка запроса на сервер
            var response = await httpClient.PostAsync("api/notification/AddNotifications", content);

            // Обработка ответа
            if (response.IsSuccessStatusCode)
            {
                // Действия при успешном запросе
                return Ok();
            }
            else
            {
                // Обработка ошибки
                return StatusCode((int)response.StatusCode);
            }
        }
        [HttpGet]
        public IActionResult GetProjectTaskId(Guid id)
        {
            var projects = dataManager.ProjectTaskRepository.GetProjectTaskById(id);
            return Ok(projects);
        }

     
        [HttpDelete]
        public IActionResult DeleteTaskAssignees(Guid taskId)
        {
            dataManager.TaskAssigneeRepository.DeleteTaskAssignees(taskId);
            return Ok();
        }
        [HttpGet]

        public IActionResult GetProjectTasksByProjectId(Guid projectId)
        {
            var projectTasks = dataManager.ProjectTaskRepository.GetProjectTasksByProjectId(projectId);
            return Ok(projectTasks);
        }
        [HttpDelete]
        public IActionResult DeleteTask(Guid taskId)
        {
            dataManager.ProjectTaskRepository.DeleteTask(taskId);
            return Ok();
        }
        [HttpGet]
        public IActionResult GetTasksByTelegramChatId(string telegramChatId)
        {
            try
            {
                var tasks = dataManager.ProjectTaskRepository.GetProjectTasksByTelegramChatId(telegramChatId);
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public IActionResult GetTasksByTelegramChatIdWithProjects(string telegramChatId)
        {
            try
            {
                var tasks = dataManager.ProjectTaskRepository.GetProjectTasksByTelegramChatIdWithProjects(telegramChatId);
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        //[HttpGet]
        //public async Task<IActionResult> GetTasksCompletedToday()
        //{
        //    try
        //    {
        //        var tasks = await dataManager.ProjectTaskRepository.GetTasksCompletedToday();
        //        return Ok(tasks);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Failed to retrieve tasks completed today: {ex.Message}");
        //    }
        //}
        [HttpGet]
        public IActionResult GetTodayTasksByTelegramChatId(string telegramChatId)
        {
            try
            {
                var tasks = dataManager.ProjectTaskRepository.GetTodayProjectTasksByTelegramChatId(telegramChatId);
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
