using JIRA.Server.Domain.Repositories.Abstract;

namespace JIRA.Server.Domain
{
    public class DataManager
    {
        public IProjectRepository ProjectRepository { get; set; }
        public IAttachmentRepository AttachmentRepository { get; set; }
        public ICommentRepository CommentRepository { get; set; }
        public IProjectTaskRepository ProjectTaskRepository { get; set; }
        public ITaskAssigneeRepository TaskAssigneeRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
        public IProjectAsigneeRepository ProjectAsigneeRepository { get; set; }
        public INotificationRepository NotificationRepository { get; set; }


        public DataManager(IProjectRepository projectRepository, IAttachmentRepository attachmentRepository, ICommentRepository commentRepository, 
            IProjectTaskRepository projectTaskRepository, ITaskAssigneeRepository taskAssigneeRepository, IUserRepository userRepository,
            IProjectAsigneeRepository projectAsigneeRepository, INotificationRepository notificationRepository)
        {
            this.ProjectRepository = projectRepository;
            AttachmentRepository = attachmentRepository;
            CommentRepository = commentRepository;
            ProjectTaskRepository = projectTaskRepository;
            TaskAssigneeRepository = taskAssigneeRepository;
            UserRepository = userRepository;
            ProjectAsigneeRepository = projectAsigneeRepository;
            NotificationRepository = notificationRepository;
        }
    }
}
