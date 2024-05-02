using BugTrackingSystem.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BugTrackingSystem.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAllAsync();

        Task<ApplicationUser?> GetByIdAsync(string id);

        Task<ApplicationUser?> GetAsync(ClaimsPrincipal claimsPrincipal);

        Task<bool> AddAsync(ApplicationUser user);

        bool CheckPassword(ApplicationUser user, string password);

        Task<bool> CreateAsync(ApplicationUser user, string password);

        bool Update(ApplicationUser user);

        bool Delete(ApplicationUser user);

        bool Save();
    }
}
