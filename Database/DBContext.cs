using BugTrackingSystem.Models.Entities.ApplicationUser;
using BugTrackingSystem.Models.Entities.Bug;
using BugTrackingSystem.Models.Entities.Permission;
using BugTrackingSystem.Models.Entities.PersonalSpace;
using BugTrackingSystem.Models.Entities.Priority;
using BugTrackingSystem.Models.Entities.Project;
using BugTrackingSystem.Models.Entities.Role;
using BugTrackingSystem.Models.Entities.Severity;
using BugTrackingSystem.Models.Entities.Status;
using BugTrackingSystem.Models.LinkingEntities.RolePermission;
using BugTrackingSystem.Models.LinkingEntities.UserRole;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BugTrackingSystem.Database;

public class DBContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<PersonalSpace> PersonalSpaces { get; set; }
    public DbSet<Project> Projects { get; set; }
    public new DbSet<Role> Roles { get; set; } // TODO: use of "new" operator is unnecessery?
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<Bug> Bugs { get; set; }
    public DbSet<Priority> Priorities { get; set; }
    public DbSet<Severity> Severity { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<RolePermission> PermissionRoles { get; set; }
    public new DbSet<UserRole> UserRoles { get; set; }
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
