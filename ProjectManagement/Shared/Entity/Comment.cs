﻿namespace ProjectManagement.Shared.Entity
{
    public class Comment:IComparable<Comment>
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid ProjectTaskId { get; set; }
        public Guid UserId { get; set; } 
        public User? User { get; set; }
       

        public Comment()
        {
            CreatedAt = DateTime.UtcNow;
            Id = Guid.NewGuid();
        }

        public int CompareTo(Comment? other)
        {
            return this.CreatedAt.CompareTo(other.CreatedAt);
        }
    }

}