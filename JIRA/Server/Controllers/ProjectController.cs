using JIRA.Client.Services;
using JIRA.Server.Domain;
using JIRA.Shared;
using JIRA.Shared.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace JIRA.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly DataManager dataManager;

        private readonly IHttpClientFactory _httpClientFactory;

        public ProjectController(DataManager dataManager, IHttpClientFactory httpClientFactory)
        {
            this.dataManager = dataManager;
            _httpClientFactory = httpClientFactory;
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
        public IActionResult GetProjectTasksByUserName(string userName)
        {
            var projectTasks = dataManager.ProjectTaskRepository.GetProjectTasksByUserName(userName);
            return Ok(projectTasks);
        }

        [HttpGet]
        public IActionResult GetProjectInfoViewModel(Guid projectId)
        {
            var project = dataManager.ProjectRepository.GetProjectById(projectId);
            var projectTasks = dataManager.ProjectTaskRepository.GetProjectTasksByProjectId(projectId);
            var assignedUsers = dataManager.ProjectRepository.GetAsigneeProjectUsers(projectId);
            var username = dataManager.ProjectAsigneeRepository.GetProjectCreator(projectId);
            var projectTaskUsers = new List<ProjectTaskUsersModel>();

            foreach (var task in projectTasks)
            {
                projectTaskUsers.Add(new ProjectTaskUsersModel()
                {
                    ProjectTask = task,
                    AssignedUser = dataManager.TaskAssigneeRepository.GetTaskAssigneeUsers(task.Id),
                    InActive = dataManager.TaskAssigneeRepository.GetActiveStatusByTaskId(task.Id)
                });
            }

            var projectInfoViewModel = new ProjectInfoViewModel()
            {
                Project = project,
                AssignedUsers = assignedUsers,
                ProjectTaskUsers = projectTaskUsers,
                Creatorname = username
            };

            return Ok(projectInfoViewModel);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProjectInfo(ProjectInfoViewModel projectInfo)
        {
            var notifications = new List<Notification>();
            var users = dataManager.ProjectRepository.GetAsigneeProjectUsers(projectInfo.Project.Id);
            foreach (var user in users)
            {
                if (!projectInfo.AssignedUsers.Contains(user))
                {
                    //отправить что он удален
                    notifications.Add(new Notification() { RecieverName = user.UserName, IsReaded = false, Message = $"Вас удалили с проекта {projectInfo.Project.Name}" });
                }
            }

            foreach (var user in projectInfo.AssignedUsers)
            {
                if (!users.Contains(user))
                {
                    //отправить что он Добавлен
                    notifications.Add(new Notification() { RecieverName = user.UserName, IsReaded = false, Message = $"Вас добавили на проект {projectInfo.Project.Name}" });
                }
            }

            dataManager.ProjectAsigneeRepository.UpdateProjectAsignees(projectInfo.Project.Id, projectInfo.AssignedUsers);
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
            //var resp = await httpClient.PostAsJsonAsync($"notification/AddNotifications", notifications);


        }
        [HttpGet]
       
        public IActionResult GetUserProjectsWithCreators(string userName)
        {
            try
            {
                // Находим пользователя по имени
                var user = dataManager.UserRepository.GetUserByName(userName);
                if (user == null)
                {
                    return NotFound($"Пользователь с именем {userName} не найден.");
                }

                // Получаем проекты пользователя
                var projects = dataManager.ProjectRepository.GetProjectsByUserName(userName);

                // Для каждого проекта находим его создателя
                var projectViewModels = new List<ProjectDeleteViewModel>();
                foreach (var project in projects)
                {
                    var creatorName = dataManager.ProjectAsigneeRepository.GetProjectCreator(project.Id);
                    projectViewModels.Add(new ProjectDeleteViewModel
                    {
                        Project = project,
                        CreatorName = creatorName
                   
                    });
                }

                return Ok(projectViewModels);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при получении проектов пользователя: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetUserProjectsWithCreatorsTasks(string userName)
        {
            try
            {
                // Находим пользователя по имени
                var user = dataManager.UserRepository.GetUserByName(userName);
                if (user == null)
                {
                    return NotFound($"Пользователь с именем {userName} не найден.");
                }

                // Получаем проекты пользователя
                var projects = dataManager.ProjectRepository.GetProjectsByUserName(userName);

                // Получаем задачи пользователя
                var tasks = dataManager.ProjectTaskRepository.GetProjectTasksByUserName(userName);

                // Для каждого проекта находим его создателя
                var projectViewModels = new List<ProjectDeleteViewModel>();
                foreach (var project in projects)
                {
                    var creatorName = dataManager.ProjectAsigneeRepository.GetProjectCreator(project.Id);
                    projectViewModels.Add(new ProjectDeleteViewModel
                    {
                        Project = project,
                        ProjectTask = tasks.FirstOrDefault(t => t.ProjectId == project.Id),
                        CreatorName = creatorName
                    });
                }

                return Ok(projectViewModels);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при получении проектов пользователя: {ex.Message}");
            }
        }
        [HttpGet]
        public IActionResult GetProject(Guid projectId)
        {
            var project = dataManager.ProjectRepository.GetProjectById(projectId);
            return Ok(project);
        }

        [HttpGet]
        public IActionResult GetProjectTaskCommentsById(Guid projectTaskId)
        {
            var projectTask = dataManager.ProjectTaskRepository.GetProjectTaskById(projectTaskId);
            var comments = dataManager.CommentRepository.GetCommentsByProjectTaskId(projectTaskId);

            var projectTaskComments = new ProjectTaskComments
            {
                ProjectTask = projectTask,
                Comments = comments
            };

            return Ok(projectTaskComments);
        }

        [HttpGet]
        public IActionResult GetAllProjectTasks()
        {
            var projectTasks = dataManager.ProjectTaskRepository.GetAllProjectTasks();
            return Ok(projectTasks);
        }


        //[HttpPost]
        //public IActionResult AddProject(ProjectsAsigneeViewModel projectAsignees)
        //{
        //    var user = dataManager.UserRepository.GetUserByName(projectAsignees.UserName);
        //    projectAsignees.ProjectAsignees.Add(new ProjectAsignee()
        //    {
        //        IsCreator = true,
        //        UserId = user.Id,
        //        ProjectId = projectAsignees.Project.Id
        //    });
        //    dataManager.ProjectRepository.Add(projectAsignees.Project);
        //    dataManager.ProjectAsigneeRepository.Add(projectAsignees.ProjectAsignees);
        //    return Ok();
        //}

        [HttpPost]
        public async Task<IActionResult> AddProject(ProjectsAsigneeViewModel projectAsignees)
        {
            var user = dataManager.UserRepository.GetUserByName(projectAsignees.UserName);
            projectAsignees.ProjectAsignees.Add(new ProjectAsignee()
            {
                IsCreator = true,
                UserId = user.Id,
                ProjectId = projectAsignees.Project.Id
            });
            dataManager.ProjectRepository.Add(projectAsignees.Project);
            dataManager.ProjectAsigneeRepository.Add(projectAsignees.ProjectAsignees);

            var notifications = new List<Notification>();

            foreach(var userId in projectAsignees.ProjectAsignees.Select(pa => pa.UserId))
            {
                var name = dataManager.UserRepository.GetUserById(userId).UserName;
                var notification = new Notification()
                {
                    RecieverName = name,
                    IsReaded = false,
                    Message = $"Вы были добавлены в проект {projectAsignees.Project.Name}"
                };
                notifications.Add(notification);
            }

         

            // Сериализация уведомления в JSON
            var jsonNotification = JsonConvert.SerializeObject(notifications);

            // Создание содержимого запроса
            var content = new StringContent(jsonNotification, Encoding.UTF8, "application/json");

            // Отправка запроса на сервер
            var httpClient = _httpClientFactory.CreateClient("InternalApi");
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

        [HttpDelete]
        public async Task<IActionResult> DeleteProject(Guid projectId)  
        {
            var notifications = new List<Notification>();
            var project = dataManager.ProjectRepository.GetProjectById(projectId);

            var projectAssignees = dataManager.ProjectAsigneeRepository.GetProjectAsigneesByProjectId(projectId);

            foreach(var userId in projectAssignees.Select(pa => pa.UserId))
            {
                var user = dataManager.UserRepository.GetUserById(userId);
                var notification = new Notification()
                {
                    RecieverName = user.UserName,
                    IsReaded = false,
                    Message = $"Был удален проект {project.Name}, где Вы были назначены"
                };
                notifications.Add(notification);
            }

            dataManager.ProjectRepository.DeleteProject(projectId);

            // Сериализация уведомления в JSON
            var jsonNotification = JsonConvert.SerializeObject(notifications);

            // Создание содержимого запроса
            var content = new StringContent(jsonNotification, Encoding.UTF8, "application/json");

            // Отправка запроса на сервер
            var httpClient = _httpClientFactory.CreateClient("InternalApi");
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
        public IActionResult GetProjectTasksByDate(Guid projectID, DateTime date)
        {
            try
            {
                // Приведение даты к началу дня
                date = date.Date;

                var projectTasks = dataManager.ProjectTaskRepository.GetProjectProjectTasksByDate(projectID, date);
                return Ok(projectTasks);
            }
            catch (Exception ex)
            {
                // Обработка ошибок, например, возврат статуса ошибки
                return StatusCode(500, $"Ошибка при получении заданий для даты: {ex.Message}");
            }
        }
        [HttpGet]
        public IActionResult GetProjectTaskStatuses()
        {
            var statuses = dataManager.ProjectTaskRepository.GetProjectTaskStatuses();
            return Ok(statuses);
        }
        [HttpGet]
        public IActionResult SearchUsers(string username)
        {
            try
            {
                // Вызываем метод репозитория для поиска пользователей по имени пользователя
                var users = dataManager.UserRepository.SearchUsersByUsername(username);

                // Возвращаем найденных пользователей в ответе
                return Ok(users);
            }
            catch (Exception ex)
            {
                // Обработка исключения при возникновении ошибки
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetProjectAsigneeUsers(Guid projectId)
        {
            var users = dataManager.ProjectRepository.GetAsigneeProjectUsers(projectId);
            return Ok(users);
        }

        [HttpPut]
        public IActionResult UpdateTaskAssignee(ProjectTaskUsersModel task)
        {
            dataManager.TaskAssigneeRepository.Update(task);
            return Ok();
        }
        [HttpDelete]
        public IActionResult DeleteProjectAsignees(Guid projectId)
        {
            dataManager.ProjectAsigneeRepository.DeleteProjectAsignees(projectId);
            return Ok();
        }
    }
}
