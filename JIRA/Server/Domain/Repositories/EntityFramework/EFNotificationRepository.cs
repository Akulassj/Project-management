using JIRA.Server.Domain.Repositories.Abstract;
using JIRA.Shared.Domain;
using JIRA.Shared.Entity;

namespace JIRA.Server.Domain.Repositories.EntityFramework
{
    public class EFNotificationRepository : INotificationRepository
    {
        private readonly ProjectManagementContext context;
        public EFNotificationRepository(ProjectManagementContext context)
        {
            this.context = context;
        }
        public void AddNotification(Notification notification)
        {
           context.Notifications.Add(notification);
           context.SaveChanges();
        }

        public List<Notification> GetNotifications(string userName)
        {
            return context.Notifications.Where(n => n.RecieverName == userName).ToList();
        }

        public void AddNotifications(List<Notification> notifications)
        {
            context.Notifications.AddRange(notifications);
            context.SaveChanges();
        }
    }
}
