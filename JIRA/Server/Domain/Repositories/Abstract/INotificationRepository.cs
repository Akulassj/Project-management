using JIRA.Shared.Entity;
using Microsoft.AspNetCore.Mvc;

namespace JIRA.Server.Domain.Repositories.Abstract
{
    public interface INotificationRepository
    {
        public void AddNotification(Notification notification);


        public void AddNotifications(List<Notification> notifications);
       

        public  List<Notification> GetNotifications(string userName);
       
    }
}
