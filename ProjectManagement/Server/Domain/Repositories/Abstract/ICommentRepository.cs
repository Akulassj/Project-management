using ProjectManagement.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagement.Server.Domain.Repositories.Abstract

{
    public interface ICommentRepository
    {
       public Comment GetCommentById(Guid id);
       public List<Comment> GetCommentsByProjectTaskId(Guid projectTaskId);
        public void Add(Comment comment);
        public void Delete(Guid id);
        public void DeleteTaskComment(Guid commentId);
    }
}
