using ProjectManagement.Server.Domain.Repositories.Abstract;
using ProjectManagement.Shared.Entity;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Shared.Domain;
using System;
using ProjectManagement.Shared;
using System.Threading.Tasks;

namespace ProjectManagement.Server.Domain.Repositories.EntityFramework
{
    public class EFAttachmentRepository: IAttachmentRepository
    {
        private readonly ProjectManagementContext context;
        public EFAttachmentRepository(ProjectManagementContext context)
        {
            this.context = context;
        }
        public Attachment GetAttachmentById(Guid id)
        {
            return context.Attachments.FirstOrDefault(a => a.Id == id);
        }

        public List<Attachment> GetAttachmentsByProjectTaskId(Guid projectTaskId)
        {
            return context.Attachments.Where(a => a.ProjectTaskId == projectTaskId).ToList();
        }
        public async Task SaveAttachmentAsync(Attachment attachment)
        {
            context.Attachments.Add(attachment);
            await context.SaveChangesAsync();
        }
        public Attachment GetAttachmentByFileName(Guid projectTaskId, string fileName)
        {
            return context.Attachments.FirstOrDefault(a => a.ProjectTaskId == projectTaskId && a.FileName == fileName);
        }

        public void DeleteAttachment(Guid attachmentId)
        {
            var attachment = context.Attachments.FirstOrDefault(c => c.Id == attachmentId);
            context.Attachments.Remove(attachment);
            context.SaveChanges();
        }
    }
}
