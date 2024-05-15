using ProjectManagement.Shared;
using ProjectManagement.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagement.Server.Domain.Repositories.Abstract
{
    public interface IAttachmentRepository
    {
        public Attachment GetAttachmentById(Guid id);
        public List<Attachment> GetAttachmentsByProjectTaskId(Guid projectTaskId);
        Task SaveAttachmentAsync(Attachment attachment);
        Attachment GetAttachmentByFileName(Guid projectTaskId, string fileName);
        public void DeleteAttachment(Guid attachmentId);
    }
}
