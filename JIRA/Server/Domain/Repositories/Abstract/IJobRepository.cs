using JIRA.Shared.Domain;
using JIRA.Shared.Entity;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JIRA.Server.Domain.Repositories.Abstract
{
    public interface IJobRepository
    {
        public Job GetJobById(Guid id);
        public List<Job> GetJobsByProjectId(Guid projectId);
        public List<Job> GetAllJobs();


        public List<Job> GetProjectJobsByDate(Guid projectID, DateTime date);
        public List<JobStatus> GetJobStatuses();
    }
}
