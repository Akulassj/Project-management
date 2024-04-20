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



                List<ProjectTask> projectTasks = new List<ProjectTask>();

                for (int i = 0; i < 100; i++)
                {
                    int c = rnd.Next(Data.projects.Count);
                    var projectTask = new ProjectTask();
                    projectTask.Name = Data.projectTasks[c][0];
                    projectTask.Description = Data.projectTasks[c][1];
                    projectTask.Status = Faker.Enum.Random<ProjectTaskStatus>().ToString();
                    projectTask.ProjectId = projects[Random.Shared.Next(projects.Count)].Id;
                    projectTasks.Add(projectTask);
                }



                List<ProjectAsignee> projectAsignees = new List<ProjectAsignee>();

                foreach (var user in users)
                {
                    var userProjects = new List<Project>();
                    for (int i = 0; i < Random.Shared.Next(1, 4); i++)
                    {                    
                        var project = projects[Random.Shared.Next(projects.Count)];
                        if (!userProjects.Contains(project))
                        {
                            userProjects.Add(project);
                            var projectAsignee = new ProjectAsignee
                            {
                                ProjectId = project.Id,
                                UserId = user.Id
                            };
                            projectAsignees.Add(projectAsignee);
                        }
                    }
                }

                List<TaskAssignee> taskAssignees = new List<TaskAssignee>();

                foreach (var task in projectTasks)
                {
                    var projectUsers = projectAsignees.Where(p => p.ProjectId == task.ProjectId)
                                                    .Select(p => p.UserId)
                                                    .SelectMany(id => users.Where(user => user.Id == id))
                                                    .ToList();

                    var taskAssignee = new TaskAssignee() 
                    { 
                        ProjectTaskId = task.Id,
                        UserId = projectUsers[Random.Shared.Next(projectUsers.Count)].Id
                    };
                    taskAssignees.Add(taskAssignee);


                    //var taskAssignee = new TaskAssignee();
                    //var userProjects = projectAsignees
                    //    .Where(x => x.UserId == user.Id)
                    //    .Select(x => x.ProjectId)
                    //    .SelectMany(p => context.Projects.Where(x => x.Id == p))
                    //    .ToList();
                    //var project = userProjects[Random.Shared.Next(userProjects.Count)];
                    //var projectProjectTasks = projectTasks.Where(j => j.ProjectId == project.Id).ToList();
                    //taskAssignee.ProjectTaskId = projectProjectTasks[Random.Shared.Next(1, projectProjectTasks.Count)].Id;
                    //taskAssignee.UserId = user.Id;
                    //taskAssignees.Add(taskAssignee);

                }

                List<Comment> comments = new List<Comment>();

                foreach (var projectTask in projectTasks)
                {
                    for (int i = 0; i < Random.Shared.Next(2, 5); i++)
                    {
                        var comment = new Comment();
                        comment.Text = Faker.Lorem.Sentence(5);
                        comment.ProjectTaskId = projectTask.Id;
                        comment.UserId = users[Random.Shared.Next(users.Count)].Id;
                        comments.Add(comment);
                    }
                }

                context.Users.AddRange(users);
                context.ProjectTasks.AddRange(projectTasks);

                context.TaskAssignees.AddRange(taskAssignees);

                context.Comments.AddRange(comments);
                context.ProjectAsignees.AddRange(projectAsignees);
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
            return context.ProjectAsignees.Where(pa => pa.UserId == id)
                .Select(pa => pa.ProjectId)
                .SelectMany(projectId => context.Projects.Where(p => p.Id == projectId))
                .Distinct()
                .ToList();
        }

        public List<User> GetAsigneeProjectUsers(Guid projectId)
        {
            return context.ProjectAsignees.Where(p => p.ProjectId == projectId).Select(a => a.UserId).SelectMany(u => context.Users.Where(user => user.Id == u)).ToList();
        }

    }
}
