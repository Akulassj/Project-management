using ProjectManagement.Shared.Entity;

namespace ProjectManagement.Client.Services
{
    public interface INotificationService
    {
        Task<List<Notification>> GetNotifications(string userName);
        Task AddNotification(Notification notification);
        Task AddNotifications(List<Notification> notifications);
    }
}
