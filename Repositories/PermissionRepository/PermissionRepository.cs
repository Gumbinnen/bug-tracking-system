using BugTrackingSystem.Database;
using BugTrackingSystem.Enums;
using BugTrackingSystem.Interfaces;
using BugTrackingSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BugTrackingSystem.Repositories.PermissionRepository
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly ApplicationDBContext context;

        public PermissionRepository(ApplicationDBContext context)
        {
            this.context = context;
        }

        public async Task<bool> AddAsync(Permission permission)
        {
            await context.Permissions.AddAsync(permission);
            return Save();
        }

        public async Task<bool> AddRangeAsync(IEnumerable<Permission> permissions)
        {
            await context.Permissions.AddRangeAsync(permissions);
            return Save();
        }

        public async Task<bool> AddRangeAsync(IEnumerable<PermissionName> permissionNames)
        {
            var permissions = new List<Permission>();
            try
            {
                foreach (var permissionName in permissionNames)
                {
                    var permission = CreateFromName(permissionName);
                    if (permission != null)
                        permissions.Add(permission);
                }
            }
            catch (NullReferenceException ex)
            {
                throw new NullReferenceException("IEnumerable<PermissionName> item shouldn't contain null.", ex);
            }

            await context.Permissions.AddRangeAsync(permissions);
            return Save();
        }

        public PermissionSetProvider UseDefaultSet()
        {
            return new PermissionSetProvider();
        }

        public bool Save()
        {
            int savedRows = context.SaveChanges();
            return savedRows > 0;
        }

        public async Task<bool> ContainsPermissionAsync(IEnumerable<Permission> permissions, PermissionName targetPermissionName)
        {
            try
            {
                return permissions.Contains(await GetByNameAsync(targetPermissionName));
            }
            catch (NullReferenceException ex)
            {
                throw new NullReferenceException("targetPermission shouldn't be null", ex);
            }
        }

        public async Task<bool> ContainsPermissionAsync(ApplicationRole role, Permission permission)
        {
            return await context.RolePermissions.Where(rp => rp.RoleId == role.Id & rp.PermissionId == permission.Id).AnyAsync();
        }

        public async Task<bool> ContainsPermissionAsync(IEnumerable<ApplicationRole> roles, Permission permission)
        {
            bool IsPermissionFoundInRole = false;
            foreach (var role in roles)
            {
                IsPermissionFoundInRole = await context.RolePermissions.Where(rp => rp.RoleId == role.Id & rp.PermissionId == permission.Id).AnyAsync();
                if (IsPermissionFoundInRole)
                    return true;
            }
            return false;
        }

        public Permission CreateFromName(PermissionName permissionName)
        {
            return new Permission(permissionName.ToString());
        }

        public async Task<Permission?> GetByNameAsync(PermissionName permissionName)
        {
            var name = permissionName.ToString();
            return await context.Permissions.FirstOrDefaultAsync(p => p.NormalizedName == name);
        }
    }
}
