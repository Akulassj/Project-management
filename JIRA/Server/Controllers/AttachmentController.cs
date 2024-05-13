using JIRA.Server.Domain;
using Microsoft.AspNetCore.Mvc;
using JIRA.Shared.Entity;
using JIRA.Shared;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace JIRA.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AttachmentController : Controller
    {
       
        private readonly DataManager dataManager;
        private readonly IConfiguration config;
        public AttachmentController(DataManager dataManager, IConfiguration config, IWebHostEnvironment env)
        {
            this.dataManager = dataManager;
            this.config = config;
          
        }
        [HttpPost]
        public async Task<IActionResult> AddAttachment(Attachment attachment)
        {
            try
            {
                // Сохранение метаданных о вложении в базе данных
                await dataManager.AttachmentRepository.SaveAttachmentAsync(attachment);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding the attachment: {ex.Message}");
            }
        }


       
        [HttpGet]
        public IActionResult GetFilesForTask(Guid projectTaskId, string fileName)
        {
            var attachment = dataManager.AttachmentRepository.GetAttachmentByFileName(projectTaskId, fileName);

            if (attachment == null)
            {
                return NotFound();
            }

            var memoryStream = new MemoryStream(attachment.FileData);
            return File(memoryStream, "application/octet-stream", fileName);
        }

        [HttpGet]
        public IActionResult GetAttachmentsByProjectTaskId(Guid projectTaskId)
        {
            var attachments = dataManager.AttachmentRepository.GetAttachmentsByProjectTaskId(projectTaskId);
            return Ok(new ProjectTaskAttachments { Attachments = attachments });
        }
        [HttpGet]
        public IActionResult GetAttachmentsByProjectTaskIdForDelete(Guid attachmentId)
        {
            var attachments = dataManager.AttachmentRepository.GetAttachmentsByProjectTaskId(attachmentId);
            return Ok(attachments);
        }
        [HttpDelete]
        public IActionResult DeleteAttachment(Guid attachmentId)
        {
            dataManager.AttachmentRepository.DeleteAttachment(attachmentId);
            return Ok();
        }

    }
}
