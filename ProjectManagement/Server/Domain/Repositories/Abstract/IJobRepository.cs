﻿using ProjectManagement.Shared;
using ProjectManagement.Shared.Domain;
using ProjectManagement.Shared.Entity;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjectManagement.Server.Domain.Repositories.Abstract
{
    public interface IProjectTaskRepository
    {
        public ProjectTask GetProjectTaskById(Guid projectId);
        public List<ProjectTask> GetProjectTasksByProjectId(Guid projectId);
        public List<ProjectTask> GetAllProjectTasks();

        public List<ProjectTask> GetProjectProjectTasksByDate(Guid projectID, DateTime date);
        public List<ProjectTaskStatus> GetProjectTaskStatuses();

        public List<ProjectTask> GetProjectTasksByUserName(string userName);
        public void Add(ProjectTask projectTask);
        public void Update(ProjectTask projectTask);
        public List<ProjectTask> GetUncompletedTasksForUserAndProject(Guid userId, Guid projectId);
        public void DeleteTask(Guid taskId);
        public List<ProjectTask> GetProjectTasksByTelegramChatId(string telegramChatId);
        public List<ProjectTask> GetProjectTasksByTelegramChatIdWithProjects(string telegramChatId);
        public List<ProjectTask> GetTodayProjectTasksByTelegramChatId(string telegramChatId);
    }
}
