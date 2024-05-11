using JIRA.Server.Domain.Repositories.Abstract;
using JIRA.Shared.Entity;
using Microsoft.EntityFrameworkCore;
using JIRA.Shared.Domain;
using System;
using JIRA.Shared;

namespace JIRA.Server.Domain.Repositories.EntityFramework
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

    }
}
