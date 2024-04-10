using JIRA.Server.Domain.Repositories.Abstract;
using JIRA.Shared.Domain;
using JIRA.Shared.Entity;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
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

        public List<Job> GetProjectJobsByDate(Guid projectID, DateTime date)
        {
            DateTime startDate = date.Date.ToUniversalTime(); 
            DateTime endDate = startDate.AddDays(1).AddSeconds(-1); 

            return context.Jobs
                .Where(j => j.CreatedAt >= startDate && j.CreatedAt <= endDate && j.ProjectId == projectID)
                .ToList();
        }
        public List<JobStatus> GetJobStatuses()
        {
            return context.Jobs.Select(j => Enum.Parse<JobStatus>(j.Status)).Distinct().ToList();
        }
        public List<Job> GetJobsByUserName(string userName)
        {
            var userId = context.Users.FirstOrDefault(u => u.UserName == userName).Id;
            if (userId == null)
            {
                throw new Exception($"Пользователь с именем {userName} не найден.");
            }

            return context.TaskAssignees
                .Where(ta => ta.UserId == userId)
                .Select(ta => ta.JobId)
                .Distinct()
                .SelectMany(jobId => context.Jobs.Where(j => j.Id == jobId))
                .Distinct()
                .ToList();
        }
        public void Add(Job job)
        {
            context.Jobs.Add(job);
            context.SaveChanges();
        }

        public void Update(Job job)
        {
            context.Jobs.Entry(job).State = EntityState.Modified;
            context.SaveChanges();
        }
      


    }
}
