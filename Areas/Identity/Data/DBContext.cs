using BugTrackingSystem.Areas.Identity.Data;
using BugTrackingSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BugTrackingSystem.Data;

public class DBContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<PersonalSpace> PersonalSpaces { get; set; }
    public DbSet<Project> Projects { get; set; }
    public new DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<Bug> Bugs { get; set; }
    public DbSet<Priority> Priorities { get; set; }
    public DbSet<Severity> Severity { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
