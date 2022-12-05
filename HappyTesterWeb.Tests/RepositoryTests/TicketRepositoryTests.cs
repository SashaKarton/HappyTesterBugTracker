using FluentAssertions;
using HappyTesterWeb.Data;
using HappyTesterWeb.Data.Enum;
using HappyTesterWeb.Models;
using HappyTesterWeb.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HappyTesterWeb.Tests.RepositoryTests
{
    public class TicketRepositoryTests
    {
        private async Task<ApplicationDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Tickets.CountAsync() < 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Tickets.Add(
                        new Ticket()
                        {
                            Title = "Login Crash",
                            Description = "When you run .exe file white screen pop and app closing enstantly after.",
                            ProjectId = 1,
                            IssuePriority = IssuePriorityEnum.High,
                            TicketStatus = IssueStatusEnum.New,
                            IssueType = IssueTypeEnum.Bug,
                        });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }

        [Fact]
        public async void TicketRepository_Add_ReturnsBool()
        {
            //Arrange
            var ticket = new Ticket()
            {
                Title = "Login Crash",
                Description = "When you run .exe file white screen pop and app closing enstantly after.",
                ProjectId = 1,
                IssuePriority = IssuePriorityEnum.High,
                TicketStatus = IssueStatusEnum.New,
                IssueType = IssueTypeEnum.Bug,
            };
            var dbContext = await GetDbContext();
            var ticketRepository = new TicketRepository(dbContext);

            //Act
            var result = ticketRepository.Add(ticket);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void TicketRepository_GetTicketByIdAsync_ReturnsTicket()
        {
            //Arrange
            var id = 1;
            var dbContext = await GetDbContext();
            var ticketRepository = new TicketRepository(dbContext);

            //Act
            var result = ticketRepository.GetTicketByIdAsync(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<Ticket>>();
        }
    }
}
