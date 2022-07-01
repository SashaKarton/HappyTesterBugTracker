using HappyTesterWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace HappyTesterWeb.Data;

public class ApplicationDbContext :DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Project> Projects { get; set; }
}
