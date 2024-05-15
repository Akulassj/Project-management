using ProjectManagement.Server.Domain.Repositories.Abstract;
using ProjectManagement.Shared.Domain;
using ProjectManagement.Shared.Entity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace ProjectManagement.Server.Domain.Repositories.EntityFramework
{
    public class EFCommentRepository : ICommentRepository
    {
        private readonly ProjectManagementContext context;
        public EFCommentRepository(ProjectManagementContext context)
        {
            this.context = context;
        }
        public Comment GetCommentById(Guid id)
        {
            return context.Comments.FirstOrDefault(c => c.Id == id);
        }

        public List<Comment> GetCommentsByProjectTaskId(Guid projectTaskId)
        {
            return context.Comments.Where(c => c.ProjectTaskId == projectTaskId).Include(c => c.User).ToList();
        }
        public void Add(Comment comment)
        {
            context.Comments.Add(comment);
            context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            context.Comments.Remove(context.Comments.FirstOrDefault(x => x.Id == id));
            context.SaveChanges();
        }

        public void DeleteTaskComment(Guid commentId)
        {
            
            var comments = context.Comments.FirstOrDefault(c => c.Id == commentId);
            context.Comments.Remove(comments);
            context.SaveChanges();
        }

    }
}
