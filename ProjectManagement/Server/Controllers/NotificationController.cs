using Blazorise;
using ProjectManagement.Client.Shared;
using ProjectManagement.Server.Domain;
using ProjectManagement.Shared.Entity;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagement.Server.Controllers
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
        [HttpPut]
        public async Task<IActionResult> UpdateNotification(Notification notification)
        {
           dataManager.NotificationRepository.UpdateNotification(notification);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteReaded(string userName)
        {
            dataManager.NotificationRepository.DeleteReaded(userName);
            return Ok();
        }
        [HttpGet]
        public IActionResult GetUndeliveredNotifications(string telegramChatId)
        {
            return Ok(dataManager.NotificationRepository.GetUndeliveredNotifications(telegramChatId));
        }

        [HttpPut]
        public IActionResult MarkNotificationsAsDelivered( List<Notification> notifications)
        {
            dataManager.NotificationRepository.MarkNotificationsAsDelivered(notifications);
            return Ok();
        }
    }
}
