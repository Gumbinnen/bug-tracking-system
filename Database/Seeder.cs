using BugTrackingSystem.Database.DefaultData;
using BugTrackingSystem.Models.Entities;
using BugTrackingSystem.Models.LinkingEntities;
using Microsoft.AspNetCore.Identity;

namespace BugTrackingSystem.Database
{
    public class Seeder
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();

            var context = serviceScope.ServiceProvider.GetService<ApplicationDBContext>()
                ?? throw new NullReferenceException("ApplicationDBContext shouldn't be null.");

            context.Database.EnsureCreated();

            /// Priorities
            if (!context.Priorities.Any())
            {
                context.Priorities.AddRange(DefaultPriorities.All);
            }

            /// Severities
            if (!context.Severities.Any())
            {
                context.Severities.AddRange(DefaultSeverities.All);
            }

            /// Statuses
            if (!context.Statuses.Any())
            {
                context.Statuses.AddRange(DefaultStatuses.All);
            }

            context.SaveChanges();
        }

        public static async Task SeedDataAsync(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();

            var context = serviceScope.ServiceProvider.GetService<ApplicationDBContext>()
                ?? throw new NullReferenceException("ApplicationDBContext shouldn't be null.");

            /// Users
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var defaultAdminUser = DefaultUsers.Admin;
            var adminUser = userManager.FindByEmailAsync(defaultAdminUser.NormalizedEmail!).Result;
            if (adminUser == null)
            {
                await userManager.CreateAsync(defaultAdminUser, DefaultPasswords.AdminPassword);
            }

            var defaultNonAdminUser = DefaultUsers.NonAdmin;
            var nonAdminUser = userManager.FindByEmailAsync(defaultNonAdminUser.NormalizedEmail!).Result;
            if (nonAdminUser == null)
            {
                await userManager.CreateAsync(defaultNonAdminUser, DefaultPasswords.NonAdminPassword);
            }

            /// Personal spaces
            PersonalSpace? adminPersonalSpace = null;
            if (!context.PersonalSpaces.Any())
            {
                adminPersonalSpace = new PersonalSpace(defaultAdminUser.Id, name: "Admin personal space");

                context.PersonalSpaces.AddRange(new List<PersonalSpace>
                {
                    adminPersonalSpace,
                    new PersonalSpace(defaultNonAdminUser.Id, name: "NonAdmin personal space")
                });
            }

            /// Projects
            var defaultProject = new Project(adminPersonalSpace!.Id, "Fancy project", description: "The first ever project in this app");

            if (!context.Projects.Any())
            {
                context.Projects.Add(defaultProject);
            }

            /// Permissions
            var permissionNames = DefaultPermissionNames.GetAllNames();

            var defaultPermissions = new List<Permission>();
            foreach (var name in permissionNames)
            {
                defaultPermissions.Add(new Permission(name));
            }

            if (!context.Permissions.Any())
            {
                context.Permissions.AddRange(defaultPermissions);
            }

            /// Roles
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            var defaultRoles = DefaultRoles.All;

            foreach (var defaultRole in defaultRoles)
            {
                var role = roleManager.FindByNameAsync(defaultRole.NormalizedName!).Result;
                if (role == null)
                {
                    await roleManager.CreateAsync(defaultRole);

                    await context.UserRoles.AddAsync(new ApplicationProjectUserRole(defaultProject.Id, defaultAdminUser.Id, defaultRole.Id));

                    /// Assign permissions to roles
                    var selectedPermissionNames = new List<DefaultPermissionNames.PermissionName>();
                    switch (defaultRole.Name)
                    {
                        case "Owner":
                            selectedPermissionNames = DefaultPermissionNames.All;
                            break;

                        case "Administrator":
                            selectedPermissionNames = DefaultPermissionNames.Administrator;
                            break;

                        case "Manager":
                            selectedPermissionNames = DefaultPermissionNames.Manager;
                            break;

                        case "Developer":
                            selectedPermissionNames = DefaultPermissionNames.Developer;
                            break;

                        case "QA":
                            selectedPermissionNames = DefaultPermissionNames.QA;
                            break;

                        case "Viewer":
                            selectedPermissionNames = DefaultPermissionNames.Viewer;
                            break;

                        default:
                            break;
                    }

                    if (selectedPermissionNames.Count > 0)
                    {
                        await FiilterAndAssignPermissionsToRole(context, defaultRole, defaultPermissions, selectedPermissionNames);
                    }
                }
            }
        }

        private static async Task FiilterAndAssignPermissionsToRole(ApplicationDBContext context, ApplicationRole role,
                                                                                              List<Permission> permissions,
                                                                                              List<DefaultPermissionNames.PermissionName> targetPermissionNames)
        {
            var permissionStringifiedNames = new List<string>();
            foreach (var permissionName in targetPermissionNames)
            {
                var permissionStringifiedName = DefaultPermissionNames.GetNormalizedName(permissionName);
                if (permissionStringifiedName != null)
                {
                    permissionStringifiedNames.Add(permissionStringifiedName);
                }
            }

            var filteredPermissions = new List<Permission>();
            foreach (var permission in permissions)
            {
                if (permissionStringifiedNames.Contains(permission.NormalizedName))
                {
                    filteredPermissions.Add(permission);
                }
            }

            foreach (var permission in filteredPermissions)
            {
                await context.RolePermissions.AddRangeAsync(new List<RolePermission>()
                {
                    new RolePermission(role.Id, permission.Id)
                });
            }
        }
    }
}
