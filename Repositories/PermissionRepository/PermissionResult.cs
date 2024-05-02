using BugTrackingSystem.Database;
using BugTrackingSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BugTrackingSystem.Repositories.PermissionRepository
{
    public class PermissionResult
    {
        private List<Permission> Permissions { get; }

        public PermissionResult(List<Permission> permissions)
        {
            Permissions = permissions;
        }

        public async Task<bool> HasPermission(Permission permission)
        {
            return Permissions.Any(p => p.Type == permission.Type && p.SubTypeValue == permission.SubTypeValue);
        }
    }
}
