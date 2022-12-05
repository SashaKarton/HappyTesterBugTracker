using FakeItEasy;
using FluentAssertions;
using HappyTesterWeb.Controllers;
using HappyTesterWeb.Interfaces;
using HappyTesterWeb.Models;
using HappyTesterWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HappyTesterWeb.Tests.ControllerTests
{
    public class ProjectControllerTests
    {
        private ProjectController _projectController;
        private IProjectRepository _projectRepository;
        private ITicketRepository _ticketRepository;
        
        public ProjectControllerTests()
        {
            _projectRepository = A.Fake<IProjectRepository>();
            _ticketRepository = A.Fake<ITicketRepository>();

            //SUT
            _projectController = new ProjectController(_projectRepository, _ticketRepository);
        }

        [Fact]
        public void ProjectController_Index_ReturnsSuccess()
        {
            //Arrange - What do i need to bring in?
            var projects = A.Fake<IEnumerable<Project>>();
            A.CallTo(() => _projectRepository.GetAll()).Returns(projects);
            var projectVMResult = A.Fake<List<IndexProjectViewModel>>();
            foreach (var project in projects)
            {
                var projectVM = A.Fake<IndexProjectViewModel>();
                projectVMResult.Add(projectVM);
            }
            //Act
            var result = _projectController.Index();
            //Assert - Object check actions
            result.Should().BeOfType <Task<IActionResult>>();
        }

        [Fact]
        public void ProjectController_Detail_ReturnSuccess()
        {
            //Arrange
            var projectId = 1;
            var tickets = A.Fake<Project>();
            A.CallTo(() => _projectRepository.GetTicketsByProjectIdAsync(projectId)).Returns(tickets);
            //Act
            var result = _projectController.Detail(projectId);
            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
    }
}
