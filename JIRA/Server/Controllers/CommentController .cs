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
        {
            dataManager.CommentRepository.Add(comment);
            return Ok();
        }








    }
}
