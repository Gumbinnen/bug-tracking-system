using BugTrackingSystem.Enums;
using BugTrackingSystem.Models.Entities;
using BugTrackingSystem.Repositories.PermissionRepository;

namespace BugTrackingSystem.Interfaces.Repository
{
    public interface IPermissionRepository
    {
        Task<bool> AddAsync(Permission permission);

        Task<bool> AddRangeAsync(IEnumerable<Permission> permissions);

        Task<bool> AddRangeAsync(IEnumerable<PermissionName> permissionNames);

        PermissionResult CheckPair(ApplicationUser user, Project project);

        Task<bool> ContainsPermissionAsync(IEnumerable<Permission> permissions, PermissionName targetPermissionName);

        Task<bool> ContainsPermissionAsync(IEnumerable<ApplicationRole> roles, Permission permission);

        Task<bool> ContainsPermissionAsync(ApplicationRole role, Permission permission);

        Permission CreateFromName(PermissionName permissionName);

        Task<Permission?> GetByNameAsync(PermissionName permissionName);

        bool Save();

        PermissionSetProvider UseDefaultSet();
    }
}
