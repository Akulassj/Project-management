using JIRA.Server.Domain.Repositories.Abstract;
using JIRA.Shared.Domain;
using JIRA.Shared.Entity;
using Microsoft.EntityFrameworkCore;
namespace JIRA.Server.Domain.Repositories.EntityFramework
{
    public class EFCommentRepository : ICommentRepository
    {
        private readonly ProjectManagementContext context;
        public EFCommentRepository(ProjectManagementContext context)
        {
            this.context = context;
        }
        public Comment GetCommentById(Guid id)
        {
            return context.Comments.FirstOrDefault(c => c.Id == id);
        }

        public List<Comment> GetCommentsByJobId(Guid jobId)
        {
            return context.Comments.Where(c => c.JobId == jobId).ToList();
        }
    }
}
