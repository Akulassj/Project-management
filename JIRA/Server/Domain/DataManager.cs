using JIRA.Server.Domain.Repositories.Abstract;

namespace JIRA.Server.Domain
{
    public class DataManager
    {
        public IProjectRepository ProjectRepository { get; set; }
        public IAttachmentRepository AttachmentRepository { get; set; }
        public ICommentRepository CommentRepository { get; set; }
        public IJobRepository JobRepository { get; set; }
        public ITaskAssigneeRepository TaskAssigneeRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
        public IProjectAsigneeRepository ProjectAsigneeRepository { get; set; }


        public DataManager(IProjectRepository projectRepository, IAttachmentRepository attachmentRepository, ICommentRepository commentRepository, 
            IJobRepository jobRepository, ITaskAssigneeRepository taskAssigneeRepository, IUserRepository userRepository,
            IProjectAsigneeRepository projectAsigneeRepository)
        {
            this.ProjectRepository = projectRepository;
            AttachmentRepository = attachmentRepository;
            CommentRepository = commentRepository;
            JobRepository = jobRepository;
            TaskAssigneeRepository = taskAssigneeRepository;
            UserRepository = userRepository;
            ProjectAsigneeRepository = projectAsigneeRepository;
        }
    }
}
