using BugTrackingSystem.Database.DefaultData;
using BugTrackingSystem.Enums;
using BugTrackingSystem.Helpers;
using BugTrackingSystem.Interfaces.Repository;
using BugTrackingSystem.Models.Entities;

namespace BugTrackingSystem.Database
{
    public class Seeder
    {
        public static async Task SeedDataAsync(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateAsyncScope();

            var context = serviceScope.ServiceProvider.GetService<ApplicationDBContext>()
                ?? throw new NullReferenceException("ApplicationDBContext shouldn't be null.");

            await context.Database.EnsureCreatedAsync();

            var userRepository = serviceScope.ServiceProvider.GetService<IUserRepository>()
                ?? throw new NullReferenceException("UserRepository shouldn't be null.");

            var projectRepository = serviceScope.ServiceProvider.GetService<IProjectRepository>()
                ?? throw new NullReferenceException("ProjectRepository shouldn't be null.");

            var permissionRepository = serviceScope.ServiceProvider.GetService<IPermissionRepository>()
                ?? throw new NullReferenceException("PermissionRepository shouldn't be null.");

            var roleRepository = serviceScope.ServiceProvider.GetService<IRoleRepository>()
                ?? throw new NullReferenceException("RoleRepository shouldn't be null.");

            /// DEFAULT USERS AND PERSONAL SPACES
            ///

            var defaultAdminUser = DefaultUserData.Admin;
            var defaultAdminPassword = DefaultUserData.AdminPassword;
            var adminUser = await userRepository.GetByIdAsync(defaultAdminUser.Id);
            if (adminUser is null)
                await userRepository.CreateAsync(defaultAdminUser, defaultAdminPassword);

            var defaultNonAdminUser = DefaultUserData.NonAdmin;
            var defaultNonAdminPassword = DefaultUserData.NonAdminPassword;
            var nonAdminUser = await userRepository.GetByIdAsync(defaultNonAdminUser.Id);
            if (nonAdminUser is null)
                await userRepository.CreateAsync(defaultNonAdminUser, defaultNonAdminPassword);

            /// DEFAULT PROJECT
            /// 

            var project = new Project(defaultAdminUser.PersonalSpace.Id, "Fancy project", "The first ever project in this bug tracker");
            projectRepository.Add(project);

            /// DEFAULT PERMISSIONS
            /// 

            var permissionNames = EnumUtility.GetValues<PermissionName>();
            await permissionRepository.AddRangeAsync(permissionNames);

            /// ROLES
            /// 

            var projectOwnerRole = await roleRepository.CreateAsync("Project Owner");
            if (projectOwnerRole != null)
            {
                var projectOwnerPermissions = DefaultPermissionSets.All;
                await roleRepository.AddAsync(projectOwnerRole, projectOwnerPermissions);
            }

            var adminRole = await roleRepository.CreateAsync("Administrator");
            if (adminRole != null)
            {
                var adminPermissions = DefaultPermissionSets.Administrator;
                await roleRepository.AddAsync(adminRole, adminPermissions);
            }

            var managerRole = await roleRepository.CreateAsync("Manager");
            if (managerRole != null)
            {
                var managerPermissions = DefaultPermissionSets.Manager;
                await roleRepository.AddAsync(managerRole, managerPermissions);
            }

            var developerRole = await roleRepository.CreateAsync("Developer");
            if (developerRole != null)
            {
                var developerPermissions = DefaultPermissionSets.Developer;
                await roleRepository.AddAsync(developerRole, developerPermissions);
            }

            var QARole = await roleRepository.CreateAsync("QA");
            if (QARole != null)
            {
                var QAPermissions = DefaultPermissionSets.QA;
                await roleRepository.AddAsync(QARole, QAPermissions);
            }

            /// DEFAULT PRIORITIES
            /// 

            if (!context.Priorities.Any())
            {
                await context.Priorities.AddRangeAsync(DefaultPriorities.All);
            }

            /// DEFAULT SEVERITIES
            /// 

            if (!context.Severities.Any())
            {
                await context.Severities.AddRangeAsync(DefaultSeverities.All);
            }

            /// DEFAULT STATUSES
            /// 

            if (!context.Statuses.Any())
            {
                await context.Statuses.AddRangeAsync(DefaultStatuses.All);
            }

            await context.SaveChangesAsync();
        }
    }
}
