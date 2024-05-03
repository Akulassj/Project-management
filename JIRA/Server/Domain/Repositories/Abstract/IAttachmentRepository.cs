using JIRA.Shared;
using JIRA.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace JIRA.Server.Domain.Repositories.Abstract
{
    public interface IAttachmentRepository
    {
       public Attachment GetAttachmentById(Guid id);
       public List<Attachment> GetAttachmentsByProjectTaskId(Guid projectTaskId);
       
    }
}
