using JIRA.Server.Domain.Repositories.Abstract;
using JIRA.Shared.Domain;
using JIRA.Shared.Entity;
using Microsoft.EntityFrameworkCore;
namespace JIRA.Server.Domain.Repositories.EntityFramework
{
    public class EFJobRepository : IJobRepository
    {
        private readonly ProjectManagementContext context;
        public EFJobRepository(ProjectManagementContext context)
        {
            this.context = context;
        }
        public Job GetJobById(Guid id)
        {
            return context.Jobs.FirstOrDefault(j => j.Id == id);
        }

        public List<Job> GetJobsByProjectId(Guid projectId)
        {
            return context.Jobs.Where(j => j.ProjectId == projectId).ToList();
        }
        public List<Job> GetAllJobs()
        {
            return context.Jobs.ToList();
        }
      
        public List<Job> GetJobsByDate(DateTime date)
        {
            DateTime startDate = date.Date.ToUniversalTime(); // Преобразование в UTC формат
            DateTime endDate = startDate.AddDays(1).AddSeconds(-1); // Конец дня

            return context.Jobs
                .Where(j => j.CreatedAt >= startDate && j.CreatedAt <= endDate)
                .ToList();
        }
        public List<JobStatus> GetJobStatuses()
        {
            return context.Jobs.Select(j => Enum.Parse<JobStatus>(j.Status)).Distinct().ToList();
        }



    }
}
