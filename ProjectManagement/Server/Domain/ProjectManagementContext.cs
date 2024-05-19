﻿using ProjectManagement.Shared.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagement.Shared.Domain
{
    public enum ProjectTaskStatus
    {
        Completed,
        InProgress,
        Uncompleted,
    }

    public class ProjectManagementContext: IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public ProjectManagementContext(DbContextOptions<ProjectManagementContext> options) : base(options) 
        { 

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            

            //builder.Entity<User>().HasData(users);
            //builder.Entity<Project>().HasData(projects) ;
            //builder.Entity<ProjectTask>().HasData(projectTasks);
            //builder.Entity<TaskAssignee>().HasData(taskAssignees);
            //builder.Entity<Comment>().HasData(comments);


            builder.Entity<IdentityRole<Guid>>().HasData(new IdentityRole<Guid>
            {
                Id = new Guid("44546e06-8719-4ad8-b88a-f271ae9d6eab"),
                Name = "moderator",
                NormalizedName = "MODERATOR"
            });

            builder.Entity<IdentityRole<Guid>>().HasData(new IdentityRole<Guid>
            {
                Id = new Guid("44546e06-8719-4ad8-b88a-f271ae9d6eaa"),
                Name = "user",
                NormalizedName = "USER"
            });

            builder.Entity<User>().HasData(new User
            {
                Id = new Guid("3b62472e-4f66-49fa-a20f-e7685b9565d8"),
                UserName = "moderator",
                FirstName = "Aleksei",
                LastName = "Kulabukhov",
                Email = "leha@mail.ru",
                NormalizedEmail = "LEHA@MAIL.RU",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<User>().HashPassword(null, "moderator"),
                SecurityStamp = string.Empty,
                Position = "Moderator"

            });

            builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = new Guid("44546e06-8719-4ad8-b88a-f271ae9d6eab"),
                UserId = new Guid("3b62472e-4f66-49fa-a20f-e7685b9565d8")
            });

        }

        public DbSet<Project> Projects { get; set; }
         
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<TaskAssignee> TaskAssignees { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

        public DbSet<ProjectAsignee> ProjectAsignees { get; set; }
        public DbSet<Notification> Notifications { get; set; }
    }
}