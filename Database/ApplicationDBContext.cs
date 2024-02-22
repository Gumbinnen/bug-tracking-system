using BugTrackingSystem.Models.Entities;
using BugTrackingSystem.Models.LinkingEntities;
using BugTrackingSystem.Models.LinkingEntities.AssignBugToUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BugTrackingSystem.Database;

public class ApplicationDBContext : IdentityDbContext<ApplicationUser, ApplicationRole, string,
                                                              IdentityUserClaim<string>, ApplicationProjectUserRole, IdentityUserLogin<string>,
                                                              IdentityRoleClaim<string>, IdentityUserToken<string>>
{
    public DbSet<PersonalSpace> PersonalSpaces { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<Bug> Bugs { get; set; }
    public DbSet<Priority> Priorities { get; set; }
    public DbSet<Severity> Severities { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");
        builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");
        builder.Entity<IdentityUserToken<string>>().ToTable("UserToken");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");

        builder.Entity<ApplicationRole>().ToTable("Role");
        builder.Entity<Priority>().ToTable("Priority");
        builder.Entity<Severity>().ToTable("Severity");
        builder.Entity<Status>().ToTable("Status");
        builder.Entity<Permission>().ToTable("Permission");

        builder.Entity<ApplicationUser>().ToTable("User");
        builder.Entity<ApplicationUser>()
            .HasOne(o => o.PersonalSpace)
            .WithOne(o => o.User)
            .HasForeignKey<PersonalSpace>(k => k.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<ApplicationUser>()
            .HasIndex(u => u.NormalizedEmail)
            .IsUnique();

        builder.Entity<PersonalSpace>()
            .ToTable("PersonalSpace")
            .HasMany(many => many.Projects)
            .WithOne(one => one.PersonalSpace)
            .HasForeignKey(key => key.PersonalSpaceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Project>()
            .ToTable("Project")
            .HasMany(m => m.Bugs)
            .WithOne(o => o.Project)
            .HasForeignKey(k => k.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Bug>().ToTable("Bug");
        builder.Entity<Bug>()
            .HasOne(o => o.Reporter)
            .WithMany()
            .HasForeignKey(k => k.ReporterId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.Entity<Bug>()
            .HasOne(o => o.Status)
            .WithMany()
            .HasForeignKey(k => k.StatusId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Bug>()
            .HasOne(o => o.Severity)
            .WithMany()
            .HasForeignKey(k => k.SeverityId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Bug>()
            .HasOne(o => o.Priority)
            .WithMany()
            .HasForeignKey(k => k.PriorityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<RolePermission>()
            .ToTable("RolePermission")
            .HasKey(k => new { k.RoleId, k.PermissionId });
        builder.Entity<RolePermission>()
            .HasOne(o => o.Role)
            .WithMany(m => m.RolePermissions)
            .HasForeignKey(k => k.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<RolePermission>()
            .HasOne(o => o.Permission)
            .WithMany(m => m.RolePermissions)
            .HasForeignKey(k => k.PermissionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<BugUserAssignation>()
            .ToTable("BugUserAssignation")
            .HasKey(k => new { k.BugId, k.UserId });
        builder.Entity<BugUserAssignation>()
            .HasOne(o => o.User)
            .WithMany(m => m.AssignedBugs)
            .HasForeignKey(k => k.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<BugUserAssignation>()
            .HasOne(o => o.Bug)
            .WithMany(m => m.AssignedUsers)
            .HasForeignKey(k => k.BugId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<ApplicationProjectUserRole>()
            .ToTable("ProjectUserRole")
            .HasKey(k => new { k.UserId, k.RoleId, k.ProjectId });
        builder.Entity<ApplicationProjectUserRole>()
            .HasOne(o => o.Project)
            .WithMany(m => m.ProjectUserRoles)
            .HasForeignKey(k => k.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<ApplicationProjectUserRole>()
            .HasOne(o => o.User)
            .WithMany(m => m.ProjectUserRoles)
            .HasForeignKey(k => k.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<ApplicationProjectUserRole>()
            .HasOne(o => o.Role)
            .WithMany(m => m.ProjectUserRoles)
            .HasForeignKey(k => k.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
