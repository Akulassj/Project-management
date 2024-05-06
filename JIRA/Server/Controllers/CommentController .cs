using JIRA.Server.Domain;
using JIRA.Shared.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace JIRA.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly DataManager dataManager;

        public CommentController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }


        [HttpGet]
        public IActionResult GetCommentsByProjectTaskId(Guid projectTaskId)
        {
            var comments = dataManager.CommentRepository.GetCommentsByProjectTaskId(projectTaskId);
            return Ok(comments);
        }

        [HttpPost]
        public IActionResult Add(Comment comment)
        {//получаем задачу к которой написан коммент и уведомляем юзера назаначенного на задачу
            dataManager.CommentRepository.Add(comment);
            return Ok();
        }
        [HttpDelete]
        public IActionResult DeleteTaskComments(Guid taskId)
        {
            dataManager.CommentRepository.DeleteTaskComments(taskId);
            return Ok();
        }







    }
}
