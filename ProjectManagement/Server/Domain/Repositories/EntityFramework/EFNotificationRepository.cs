﻿using Microsoft.EntityFrameworkCore;
using ProjectManagement.Server.Domain.Repositories.Abstract;
using ProjectManagement.Shared.Domain;
using ProjectManagement.Shared.Entity;

namespace ProjectManagement.Server.Domain.Repositories.EntityFramework
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

        public void UpdateNotification(Notification notification)
        {
            context.Entry(notification).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteReaded(string userName)
        {
            context.Notifications.RemoveRange(context.Notifications.Where(not => not.RecieverName == userName && not.IsReaded));
            context.SaveChanges();
        }
    }
}
