using BugTrackingSystem.Enums;
using BugTrackingSystem.Models.Entities;

namespace BugTrackingSystem.Interfaces.Repository
{
    public interface IRoleRepository
    {
        Task<bool> AddAsync(ApplicationRole role);

        Task<bool> AddAsync(ApplicationRole role, IEnumerable<PermissionName> permissionNames);

        Task<ApplicationRole?> CreateAsync(string name);

        bool Save();
    }
}
