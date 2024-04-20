using JIRA.Server.Domain.Repositories.Abstract;
using JIRA.Shared.Domain;
using JIRA.Shared.Entity;
using Microsoft.EntityFrameworkCore;
namespace JIRA.Server.Domain.Repositories.EntityFramework
{
    public class EFUserRepository : IUserRepository
    {
        private readonly ProjectManagementContext context;

        public EFUserRepository(ProjectManagementContext context)
        {
            this.context = context;
        }
        public User GetUserById(Guid id)
        {
            return context.Users.FirstOrDefault(u => u.Id == id);
        }

        public List<User> GetAllUsers()
        {
            return context.Users.ToList();
        }

        public User GetUserByName(string name)
        {
            return context.Users.FirstOrDefault(u => u.UserName == name);
        }
        public List<User> SearchUsersByUsername(string username)
        {
            return context.Users.Where(u => u.UserName.ToLower().Contains(username.ToLower())).ToList();
        }
    }
}
