using JIRA.Server.Domain;
using Microsoft.AspNetCore.Mvc;

using JIRA.Shared;
using JIRA.Shared.Entity;
using Microsoft.Extensions.Logging;
using JIRA.Client.Pages;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
namespace JIRA.Server.Controllers
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
        public async Task <IActionResult> UpdateProjectTask(ProjectTaskUsersModel selectedTask)
        {
            var user = dataManager.UserRepository.GetUserById(selectedTask.AssignedUser.Id);

            var notification = new Notification() { RecieverName = user.UserName, IsReaded = false, Message = $"Вас добавили на задачу {selectedTask.ProjectTask.Name}" };
            dataManager.TaskAssigneeRepository.Update(selectedTask);
            dataManager.ProjectTaskRepository.Update(selectedTask.ProjectTask);
            //dataManager.AttachmentRepository.Update(selectedTask);
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
        public IActionResult GetProjectTaskId(Guid id)
        {
            var projects = dataManager.ProjectTaskRepository.GetProjectTaskById(id);
            return Ok(projects);
        }

        //[HttpGet]
        //public IActionResult GetFilePreview(Guid attachmentId)
        //{
        //    var filePreview = dataManager.AttachmentRepository.GetFilePreview(attachmentId);
        //    if (filePreview == null)
        //    {
        //        return NotFound();
        //    }

        //    return File(filePreview, "application/octet-stream"); // Отправляет массив байтов как файл клиенту
        //}
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
