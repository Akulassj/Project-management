using JIRA.Server.Domain.Repositories.EntityFramework;
using JIRA.Shared.Domain;
using JIRA.Shared.Entity;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestProjectManagment
{
    [TestFixture]
    public class Tests
    {
        private ProjectManagementContext _dbContext;
        private EFProjectRepository _projectService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ProjectManagementContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _dbContext = new ProjectManagementContext(options);
            _projectService = new EFProjectRepository(_dbContext);
        }

        [Test]
        public void GetProjectById_ValidId_ReturnsProject()
        {
            // Arrange
            Guid projectId = Guid.NewGuid();
            var projectList = new List<Project>
            {
                new Project { Id = projectId, Name = "Test Project", Description = "Test Description" }
            };
            _dbContext.Projects.AddRange(projectList);
            _dbContext.SaveChanges();

            // Act
            var result = _projectService.GetProjectById(projectId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(projectId, result.Id);
            Assert.AreEqual("Test Project", result.Name);
            Assert.AreEqual("Test Description", result.Description);
        }

        [Test]
        public void GetAllProjects_ReturnsAllProjects()
        {
            // Arrange
            var projectList = new List<Project>
    {
        new Project { Id = Guid.NewGuid(), Name = "Project 1", Description = "Description 1" },
        new Project { Id = Guid.NewGuid(), Name = "Project 2", Description = "Description 2" },
        new Project { Id = Guid.NewGuid(), Name = "Project 3", Description = "Description 3" },
        new Project { Id = Guid.NewGuid(), Name = "Project 4", Description = "Description 4" },
        new Project { Id = Guid.NewGuid(), Name = "Project 5", Description = "Description 5" },
        new Project { Id = Guid.NewGuid(), Name = "Project 6", Description = "Description 6" },
        new Project { Id = Guid.NewGuid(), Name = "Project 7", Description = "Description 7" },
        new Project { Id = Guid.NewGuid(), Name = "Project 8", Description = "Description 8" },
        new Project { Id = Guid.NewGuid(), Name = "Project 9", Description = "Description 9" },
        new Project { Id = Guid.NewGuid(), Name = "Project 10", Description = "Description 10" },
        new Project { Id = Guid.NewGuid(), Name = "Project 11", Description = "Description 11" },
        new Project { Id = Guid.NewGuid(), Name = "Project 12", Description = "Description 12" },
    };
            _dbContext.Projects.AddRange(projectList);
            _dbContext.SaveChanges(); 
            // Act
            var result = _projectService.GetAllProjects();
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(23, result.Count);
        }


        [Test]
        public void Add_ValidProject_ProjectAdded()
        {
            // Arrange
            var project = new Project { Id = Guid.NewGuid(), Name = "New Project", Description = "New Description" };
            // Act
            _projectService.Add(project);
            _dbContext.SaveChanges();
            // Assert
            Assert.IsTrue(_dbContext.Projects.Any(p => p.Id == project.Id));
        }


        [Test]
        public void Delete_ValidId_ProjectDeleted()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var project = new Project { Id = projectId, Name = "Project to Delete", Description = "Description to Delete" };
            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();
            // Act
            _projectService.Delete(projectId);
            _dbContext.SaveChanges();
            // Assert
            Assert.IsFalse(_dbContext.Projects.Any(p => p.Id == projectId));
        }

        [Test]
        public void Update_ValidProject_ProjectUpdated()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var project = new Project { Id = projectId, Name = "Old Project", Description = "Old Description" };
            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();

            // Act
            var updatedProject = new Project { Id = projectId, Name = "Updated Project", Description = "Updated Description" };
            _dbContext.Entry(project).State = EntityState.Detached; 
            _projectService.Update(updatedProject);

            // Assert
            var result = _dbContext.Projects.FirstOrDefault(p => p.Id == projectId);
            Assert.IsNotNull(result);
            Assert.AreEqual(updatedProject.Name, result.Name);
            Assert.AreEqual(updatedProject.Description, result.Description);
        }

    }
}
