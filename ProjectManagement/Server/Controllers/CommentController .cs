using ProjectManagement.Server.Domain;
using ProjectManagement.Shared.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace ProjectManagement.Server.Controllers
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
        public IActionResult DeleteComment(Guid commentId)
        {
           
            dataManager.CommentRepository.DeleteTaskComment(commentId);
            return Ok();
        }







    }
}
