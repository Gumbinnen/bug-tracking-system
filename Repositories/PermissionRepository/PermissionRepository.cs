using BugTrackingSystem.Database;
using BugTrackingSystem.Enums;
using BugTrackingSystem.Interfaces.Repository;
using BugTrackingSystem.Models.Entities;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace BugTrackingSystem.Repositories.PermissionRepository
{
    public sealed class PermissionRepository : IPermissionRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly DapperDBContext _dapperContext;

        public PermissionRepository(ApplicationDBContext context, DapperDBContext dapperContext)
        {
            this._context = context;
            this._dapperContext = dapperContext;
        }

        public async Task<bool> AddAsync(Permission permission)
        {
            await _context.Permissions.AddAsync(permission);
            return Save();
        }

        public async Task<bool> AddRangeAsync(IEnumerable<Permission> permissions)
        {
            await _context.Permissions.AddRangeAsync(permissions);
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

            await _context.Permissions.AddRangeAsync(permissions);
            return Save();
        }

        public async Task<PermissionResult> CheckPairAsync(ApplicationUser user, Project project)
        {
            var sql = @"SELECT p.Type, p.Value
                            FROM Permissions p
                            INNER JOIN RolePermissions rp ON p.Id = rp.PermissionId
                            INNER JOIN ProjectUserRoles pur ON rp.RoleId = pur.RoleId
                            WHERE pur.ProjectId = @ProjectId AND pur.UserId = @UserId";

            IEnumerable<Permission> permissions;
            using (var connection = _dapperContext.Connection)
            {
                connection.Open();
                permissions = await connection.QueryAsync<Permission>(sql, new { ProjectId = project.Id, UserId = user.Id });
            }
            return new PermissionResult(permissions.ToList());
        }

        public Permission CreateFromName(PermissionName permissionName)
        {
            return new Permission(permissionName.ToString());
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
            return await _context.RolePermissions.Where(rp => rp.RoleId == role.Id & rp.PermissionId == permission.Id).AnyAsync();
        }

        public async Task<bool> ContainsPermissionAsync(IEnumerable<ApplicationRole> roles, Permission permission)
        {
            bool IsPermissionFoundInRole = false;
            foreach (var role in roles)
            {
                IsPermissionFoundInRole = await _context.RolePermissions.Where(rp => rp.RoleId == role.Id & rp.PermissionId == permission.Id).AnyAsync();
                if (IsPermissionFoundInRole)
                    return true;
            }
            return false;
        }

        public async Task<Permission?> GetByNameAsync(PermissionName permissionName)
        {
            var name = permissionName.ToString().ToUpperInvariant();
            return await _context.Permissions.FirstOrDefaultAsync(p => p.NormalizedName == name);
        }

        public bool Save()
        {
            int savedRows = _context.SaveChanges();
            return savedRows > 0;
        }

        public PermissionSetProvider UseDefaultSet()
        {
            return new PermissionSetProvider();
        }
    }
}
