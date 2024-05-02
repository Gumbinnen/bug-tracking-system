using BugTrackingSystem.Database;
using BugTrackingSystem.Enums;
using BugTrackingSystem.Interfaces.Repository;
using BugTrackingSystem.Models.Entities;
using BugTrackingSystem.Models.LinkingEntities;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BugTrackingSystem.Repositories
{
    public sealed class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDBContext context;
        private readonly IPermissionRepository permissionRepository;

        public RoleRepository(ApplicationDBContext context, IPermissionRepository permissionRepository)
        {
            this.context = context;
            this.permissionRepository = permissionRepository;
        }

        public async Task<bool> AddAsync(ApplicationRole role)
        {
            try
            {
                var basePermissions = permissionRepository.UseDefaultSet().GetBasePermissions();
                List<RolePermission> rolePermissions = new();
                foreach (var permission in basePermissions)
                {
                    rolePermissions.Add(new RolePermission(role.Id, permission.Id));
                }

                await context.RolePermissions.AddRangeAsync(rolePermissions);

                await context.AddAsync(role);
                return Save();
            }
            catch (OperationCanceledException) { return false; }
        }

        public async Task<bool> AddAsync(ApplicationRole role, IEnumerable<PermissionName> permissionNames)
        {
            try
            {
                await context.Roles.AddAsync(role);

                List<RolePermission> rolePermissions = new();
                foreach (var name in permissionNames)
                {
                    var permission = await permissionRepository.GetByNameAsync(name);
                    if (permission != null)
                    {
                        rolePermissions.Add(new RolePermission(role.Id, permission.Id));
                    }
                }

                await context.RolePermissions.AddRangeAsync(rolePermissions);
                return Save();
            }
            catch (OperationCanceledException) { return false; }
        }

        public async Task<ApplicationRole?> CreateAsync(string name)
        {
            try
            {
                var role = new ApplicationRole(name);

                await context.Roles.AddAsync(role);

                return role;
            }
            catch (OperationCanceledException) { return null; }
        }

        public bool Save()
        {
            int savedCount = context.SaveChanges();
            return savedCount > 0;
        }
    }
}
