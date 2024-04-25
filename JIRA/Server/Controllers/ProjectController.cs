using JIRA.Server.Domain;
using JIRA.Shared;
using JIRA.Shared.Entity;
using Microsoft.AspNetCore.Identity;
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

        [HttpPost]
        public IActionResult CreateNewProject(Project project)
        {
            dataManager.ProjectRepository.Add(project);
            return Ok();
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
                    AssignedUsers = dataManager.TaskAssigneeRepository.GetTaskAssigneeUsers(task.Id)
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
        public IActionResult UpdateProjectInfo(ProjectInfoViewModel projectInfo)
        {
            dataManager.ProjectAsigneeRepository.UpdateProjectAsignees(projectInfo.Project.Id, projectInfo.AssignedUsers);

            return Ok();
        }

        [HttpGet]
        public IActionResult GetProject(Guid projectId)
        {
            var project = dataManager.ProjectRepository.GetProjectById(projectId);
            return Ok(project);
        }

        [HttpGet]
        public IActionResult GetProjectProjectTasks(Guid projectId)
        {
            var projects = dataManager.ProjectTaskRepository.GetProjectTasksByProjectId(projectId);
            return Ok(projects);
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

        [HttpGet]
        public IActionResult GetUserProjectTasks(Guid userId)
        {
            var projectTasks = dataManager.ProjectTaskRepository.GetAllProjectTasks();
            return Ok(projectTasks);
        }

      

        [HttpPost]
        public IActionResult AddProject(ProjectsAsigneeViewModel projectAsignees)
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
            return Ok();
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
        public IActionResult GetProjectAsigneeByProjectId(Guid projectId)
        {
            try
            {
                // Получаем информацию о создателе проекта по его идентификатору
                var projectAsignee = dataManager.ProjectAsigneeRepository.GetProjectAsigneeByProjectId(projectId);
                return Ok(projectAsignee);
            }
            catch (Exception ex)
            {
                // Обработка ошибок, например, возврат статуса ошибки
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ошибка при получении информации о создателе проекта: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetProjectAsigneeUsers(Guid projectId)
        {
            var users = dataManager.ProjectRepository.GetAsigneeProjectUsers(projectId);
            return Ok(users);
        }
        [HttpPost]
        public IActionResult AddProjectUser(Guid projectId, Guid userId)
        {
            dataManager.ProjectAsigneeRepository.AddProjectUser(projectId, userId);
            return Ok();
        }

        [HttpPost]
        public IActionResult RemoveProjectUser(Guid projectId, Guid userId)
        {
            dataManager.ProjectAsigneeRepository.RemoveProjectUser(projectId, userId);
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateTaskAssignee(TaskAssignee taskAssignee)
        {
            dataManager.TaskAssigneeRepository.Update(taskAssignee);
            return Ok();
        }

    }
}
