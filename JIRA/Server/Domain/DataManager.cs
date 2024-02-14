using JIRA.Server.Domain.Repositories.Abstract;

namespace JIRA.Server.Domain
{
    public class DataManager
    {
        public IProjectRepository ProjectRepository { get; set; }

        public DataManager(IProjectRepository projectRepository)
        {
            this.ProjectRepository = projectRepository;
            
        }
    }
}
