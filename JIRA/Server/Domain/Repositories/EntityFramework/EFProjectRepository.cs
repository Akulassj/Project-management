using JIRA.Server.Domain.Entity;
using JIRA.Server.Domain.Repositories.Abstract;
using JIRA.Shared.Domain;
using Microsoft.EntityFrameworkCore;

namespace JIRA.Server.Domain.Repositories.EntityFramework
{
    public class EFProjectRepository: IProjectRepository
    {
        private readonly ProjectManagementContext context;

        public EFProjectRepository(ProjectManagementContext context)
        {
            this.context = context;
        }
        public List<Project> GetAllProjects()
        {
            return context.Projects.ToList();
        }

        public List<Job> GetJobsByProjectId(Guid projectId)
        {
            return context.Jobs
                          .Where(job => job.ProjectId == projectId)
                          .ToList();
        }

        public List<Comment> GetCommentsByJobID(Guid jobId)
        {
            return context.Comments
                          .Where(comment => comment.TaskId == jobId)
                          .ToList();
        }

        public List<Attachment> GetAttachmentsByJobID(Guid jobId)
        {
            return context.Attachments
                          .Where(attachment => attachment.TaskId == jobId)
                          .ToList();
        }

        public List<Job> GetAssignedTasksByDeveloperId(Guid developerId)
        {
            return context.TaskAssignees
                          .Where(taskAssignee => taskAssignee.DeveloperId == developerId)
                          .Select(taskAssignee => taskAssignee.Job)
                          .ToList();
        }

        public List<Job> GetAssignedTasksByProjectId(Guid projectId)
        {
            return context.Jobs
                          .Where(job => job.ProjectId == projectId)
                          .ToList();
        }
    }
}
