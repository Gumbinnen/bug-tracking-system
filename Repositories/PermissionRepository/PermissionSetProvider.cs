using BugTrackingSystem.Database;
using BugTrackingSystem.Enums;
using BugTrackingSystem.Models.Entities;

namespace BugTrackingSystem.Repositories.PermissionRepository
{
    public sealed class PermissionSetProvider
    {
        public IEnumerable<Permission> GetBasePermissions()
        {
            return new List<Permission>()
            {
                new(PermissionName.GUEST.ToString())
            };
        }
    }
}
