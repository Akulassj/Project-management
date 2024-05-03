using JIRA.Shared.Entity;
using System.Net.Http.Json;

namespace JIRA.Client.Services
{
    public class NotificationService:INotificationService
    {
        private readonly HttpClient _httpClient;

        public NotificationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Notification>> GetNotifications(string userName)
        {
            return await _httpClient.GetFromJsonAsync<List<Notification>>($"api/notification/GetNotifications?userName={userName}");
        }

        public async Task AddNotification(Notification notification)
        {
            await _httpClient.PostAsJsonAsync("api/notification/AddNotification", notification);
        }

        public async Task AddNotifications(List<Notification> notifications)
        {
            await _httpClient.PostAsJsonAsync("api/notification/AddNotifications", notifications);
        }
    }
}
