using JIRA.Shared.Domain;
using JIRA.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace JIRA.Server.Domain.Repositories.Abstract
{
    public interface IJobRepository
    {
      public  Job GetJobById(Guid id);
      public  List<Job> GetJobsByProjectId(Guid projectId);

     public List<Job> GetAllJobs();

        public List<Job> GetJobsByDate(DateTime date);
        public List<JobStatus> GetJobStatuses();
    }
}
