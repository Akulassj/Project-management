using ProjectManagement.Shared.Entity;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.Server.Domain.Repositories.Abstract
{
    public interface INotificationRepository
    {
        public void AddNotification(Notification notification);


        public void AddNotifications(List<Notification> notifications);
       

        public  List<Notification> GetNotifications(string userName);

        public void UpdateNotification(Notification notification);
        public void DeleteReaded(string userName);
        public void MarkNotificationsAsDelivered(List<Notification> notifications);
        public List<Notification> GetUndeliveredNotifications(string telegramChatId);
    }
}
