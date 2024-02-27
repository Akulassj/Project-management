using JIRA.Server.Domain.Repositories.Abstract;
using JIRA.Shared.Entity;
using Microsoft.EntityFrameworkCore;
using JIRA.Shared.Domain;
using System;

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

        public List<Attachment> GetAttachmentsByJobId(Guid jobId)
        {
            return context.Attachments.Where(a => a.JobId == jobId).ToList();
        }
    }
}
