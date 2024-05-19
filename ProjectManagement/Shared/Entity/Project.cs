﻿using System;

namespace ProjectManagement.Shared.Entity
{
    
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        



        public Project()
        {
            CreatedAt = DateTime.UtcNow;
            Id = Guid.NewGuid();
        }

    }
}