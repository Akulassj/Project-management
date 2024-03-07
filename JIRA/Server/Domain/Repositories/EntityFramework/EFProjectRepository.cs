using JIRA.Shared.Entity;
using JIRA.Server.Domain.Repositories.Abstract;
using JIRA.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;
using JIRA.Shared;
using Blazorise.States;

namespace JIRA.Server.Domain.Repositories.EntityFramework
{
    public class EFProjectRepository : IProjectRepository
    {
        private readonly ProjectManagementContext context;

        public EFProjectRepository(ProjectManagementContext context)
        {
            this.context = context;
            if (!context.Projects.Any())
            {
                Random rnd = new Random();
                List<Project> projects = new List<Project>();

                for (int i = 0; i < 10; i++)
                {
                    int c = rnd.Next(Data.projects.Count);
                    var project = new Project();
                    project.Name = Data.projects[c][0];
                    project.Description = Data.projects[c][1];
                    projects.Add(project);
                }
                context.Projects.AddRange(projects);
                context.SaveChanges();

                List<User> users = new List<User>();
                for (int i = 0; i < 40; i++)
                {
                    var user = new User();
                    user.FirstName = Data.userFirstNames[Random.Shared.Next(Data.userFirstNames.Count)];
                    user.LastName = Data.userLastNames[Random.Shared.Next(Data.userLastNames.Count)];
                    user.Position = "Developer";
                    var name = Data.userNickNames[Random.Shared.Next(Data.userNickNames.Count)];
                    Data.userNickNames.Remove(name);
                    user.UserName = name;
                    user.Email = Faker.Internet.Email();
                    user.NormalizedUserName = user.UserName.ToUpperInvariant();
                    user.NormalizedEmail = user.Email.ToUpperInvariant();
                    user.PasswordHash = new PasswordHasher<User>().HashPassword(null, user.FirstName);
                    user.SecurityStamp = Guid.NewGuid().ToString();
                    users.Add(user);
                }



                List<Job> jobs = new List<Job>();

                for (int i = 0; i < 100; i++)
                {
                    int c = rnd.Next(Data.projects.Count);
                    var job = new Job();
                    job.Name = Data.jobs[c][0];
                    job.Description = Data.jobs[c][1];
                    job.Status = Faker.Enum.Random<JobStatus>().ToString();
                    job.ProjectId = projects[Random.Shared.Next(projects.Count)].Id;
                    jobs.Add(job);
                }

                List<TaskAssignee> taskAssignees = new List<TaskAssignee>();

                foreach (var user in users)
                {
                    for (int i = 0; i < Random.Shared.Next(1, 3); i++)
                    {
                        var taskAssignee = new TaskAssignee();
                        taskAssignee.JobId = jobs[Random.Shared.Next(jobs.Count)].Id;
                        taskAssignee.UserId = user.Id;
                        taskAssignees.Add(taskAssignee);
                    }
                }

                List<Comment> comments = new List<Comment>();

                foreach (var job in jobs)
                {
                    for (int i = 0; i < Random.Shared.Next(2, 5); i++)
                    {
                        var comment = new Comment();
                        comment.Text = Faker.Lorem.Sentence(5);
                        comment.JobId = job.Id;
                        comment.UserId = users[Random.Shared.Next(users.Count)].Id;
                        comments.Add(comment);
                    }
                }

                context.Users.AddRange(users);
                context.Jobs.AddRange(jobs);

                context.TaskAssignees.AddRange(taskAssignees);

                context.Comments.AddRange(comments);
                context.SaveChanges();
            }
        }
        public Project GetProjectById(Guid id)
        {
            return context.Projects.FirstOrDefault(p => p.Id == id);
        }
        public List<Project> GetAllProjects()
        {
            return context.Projects.ToList();
        }
        public void Add(Project project)
        {
            context.Projects.Add(project);
            context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            context.Projects.Remove(context.Projects.FirstOrDefault(x => x.Id == id));
            context.SaveChanges();
        }



        public void Update(Project project)
        {
            context.Projects.Entry(project).State = EntityState.Modified;
            context.SaveChanges();
        }

        public List<Project> GetProjectsByUserName(string userName)
        {
            var id = context.Users.FirstOrDefault(x => x.UserName == userName).Id;
            return context.TaskAssignees.Where(ta => ta.UserId == id)
                .Select(ta => ta.JobId)
                .Distinct()
                .SelectMany(jobId => context.Jobs.Where(j => j.Id == jobId))
                .Select(j => j.ProjectId)
                .Distinct()
                .SelectMany(projectId => context.Projects.Where(p => p.Id == projectId))
                .ToList();

        }
    }
}
