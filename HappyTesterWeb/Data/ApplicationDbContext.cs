using HappyTesterWeb.Models;
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
    public DbSet<AppUser> Users { get; set; }
}
