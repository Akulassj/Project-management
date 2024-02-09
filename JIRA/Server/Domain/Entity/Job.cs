﻿using System.Net.Mail;

namespace JIRA.Server.Domain.Entity
{
    public class Job
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid ProjectId { get; set; } // Внешний ключ для связи с таблицей проектов
    }
}
