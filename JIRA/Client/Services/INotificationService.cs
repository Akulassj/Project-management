using JIRA.Shared.Entity;

namespace JIRA.Client.Services
{
    public interface INotificationService
    {
        Task<List<Notification>> GetNotifications(string userName);
        Task AddNotification(Notification notification);
        Task AddNotifications(List<Notification> notifications);
    }
}
