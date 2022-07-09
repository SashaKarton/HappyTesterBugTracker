using HappyTesterWeb.Data.Enum;
using HappyTesterWeb.Models;
using Microsoft.AspNetCore.Identity;

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
                            Description = "First app project"
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
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "leshchenkoah@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "SashaKarton",
                        Email = adminUserEmail,
                        EmailConfirmed = true,

                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@bugtracker.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,

                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }


            }
        }
    }
}
