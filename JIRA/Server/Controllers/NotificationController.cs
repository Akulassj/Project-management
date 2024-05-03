using Blazorise;
using JIRA.Client.Shared;
using JIRA.Server.Domain;
using JIRA.Shared.Entity;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JIRA.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NotificationController : Controller
    {
        private readonly DataManager dataManager;
        public NotificationController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        [HttpPost]
        public IActionResult AddNotification(Notification notification)
        {
            dataManager.NotificationRepository.AddNotification(notification);
            return Ok();
        }

        [HttpPost]
        public IActionResult AddNotifications(List<Notification> notifications)
        {
            dataManager.NotificationRepository.AddNotifications(notifications);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetNotifications(string userName)
        {
            return Ok(dataManager.NotificationRepository.GetNotifications(userName));
        }
    }
}
