using JIRA.Server.Domain.Entity;

namespace JIRA.Server.Domain.Repositories.Abstract
{
    public interface IProjectRepository
    {
        public List<Project> GetAllProjects();
        public List<Job> GetJobsByProjectId(Guid projectId);
        public List<Comment> GetCommentsByJobID(Guid jobId);
        public List<Attachment> GetAttachmentsByJobID(Guid jobId);
        public List<Job> GetAssignedTasksByDeveloperId(Guid developerId);
        public List<Job> GetAssignedTasksByProjectId(Guid projectId);
    }
}
