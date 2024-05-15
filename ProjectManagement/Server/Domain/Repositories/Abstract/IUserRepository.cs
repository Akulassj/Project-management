using ProjectManagement.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagement.Server.Domain.Repositories.Abstract
{
    public interface IUserRepository
    {
        public User GetUserById(Guid id);
        public User GetUserByName(string name);
        public List<User> GetAllUsers();
        public List<User> SearchUsersByUsername(string username);
    }
}
