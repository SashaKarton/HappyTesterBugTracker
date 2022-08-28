using HappyTesterWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HappyTesterWeb.Data;

public class ApplicationDbContext :IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<AppUserProject> AppUserProjects { get; set; }
    public DbSet<AppUserTicket> AppUserTickets { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<AppUserProject>()
            .HasKey(e => new { e.AppUsersId, e.ProjectsId });

        modelBuilder.Entity<Project>()
            .HasMany(u => u.AppUsers)
            .WithMany(p => p.Projects)
            .UsingEntity<AppUserProject>();


        modelBuilder.Entity<AppUserTicket>()
            .HasKey(e => new { e.AppUsersId, e.TicketsId });

        modelBuilder.Entity<Ticket>()
            .HasMany(u => u.AppUsers)
            .WithMany(p => p.Tickets)
            .UsingEntity<AppUserTicket>();


    }

}
