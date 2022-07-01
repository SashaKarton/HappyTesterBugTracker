using HappyTesterWeb.Data.Enum;
using HappyTesterWeb.Models;

namespace HappyTesterWeb.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();


                //Project
                if (!context.Projects.Any())
                {
                    context.Projects.AddRange(new List<Project>()
                    {
                        new Project()
                        {
                            Title = "Test Project 1",
                            Description = " First app project"
                        }
                    });
                    context.SaveChanges();
                }

                //Ticket
                if (!context.Tickets.Any())
                {
                    context.Tickets.AddRange(new List<Ticket>()
                    {
                        new Ticket()
                        {
                            Title = "Login Crash",
                            Description = "When you run .exe file white screen pop and app closing enstantly after.",
                            ProjectId = 1,
                            IssuePriority = IssuePriorityEnum.High,
                            TicketStatus = IssueStatusEnum.New,
                            IssueType = IssueTypeEnum.Bug

                        },
                        new Ticket()
                        {
                            Title = "Error 1332",
                            Description = "Old Error 1332 retun when use Next button",
                            ProjectId = 1,
                            IssuePriority = IssuePriorityEnum.Low,
                            TicketStatus = IssueStatusEnum.Reopened,
                            IssueType = IssueTypeEnum.Error

                        }
                    });
                    context.SaveChanges();
                }
                
                

            }

        }

    }
}
