using JIRA.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace JIRA.Server.Domain.Repositories.Abstract

{
    public interface ICommentRepository
    {
       public Comment GetCommentById(Guid id);
       public List<Comment> GetCommentsByJobId(Guid jobId);
    }
}
