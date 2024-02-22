using BugTrackingSystem.Database;
using BugTrackingSystem.Helpers;
using BugTrackingSystem.Interfaces;
using BugTrackingSystem.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BugTrackingSystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPersonalSpaceRepository personalSpaceRepository;

        public UserRepository(ApplicationDBContext context, UserManager<ApplicationUser> userManager, IPersonalSpaceRepository personalSpaceRepository)
        {
            this.context = context;
            this.userManager = userManager;
            this.personalSpaceRepository = personalSpaceRepository;
        }

        public async Task<bool> AddAsync(ApplicationUser user)
        {
            await context.Users.AddAsync(user);
            
            bool hasPersonalSpace = await personalSpaceRepository.IsPersonalSpaceExistsForUser(user);
            if (!hasPersonalSpace)
            {
                var personalSpace = new PersonalSpace(user.Id, user.UserName ?? string.Empty);

                await personalSpaceRepository.AddAsync(personalSpace);
            }

            return Save();
        }

        public bool CheckPassword(ApplicationUser user, string password)
        {
            if (user.PasswordHash is null)
                return false;
            return new PasswordHasher().VerifyHashedPassword(user, password, user.PasswordHash) switch
            {
                PasswordVerificationResult.Success => true,
                PasswordVerificationResult.Failed => false,
                PasswordVerificationResult.SuccessRehashNeeded => true,
                _ => throw new NullReferenceException("VerifyHashedPassword() return null.")
            };
        }

        public async Task<bool> CreateAsync(ApplicationUser user, string password)
        {
            var result = await userManager.CreateAsync(user, password);

            if (result != IdentityResult.Success)
            {
                return false;
            }

            bool hasPersonalSpace = await personalSpaceRepository.IsPersonalSpaceExistsForUser(user);
            if (!hasPersonalSpace)
            {
                var personalSpace = new PersonalSpace(user.Id, user.UserName ?? string.Empty);

                hasPersonalSpace = await personalSpaceRepository.AddAsync(personalSpace);
            }

            if (!hasPersonalSpace)
            {
                await userManager.DeleteAsync(user);
                return false;
            }

            return true;
        }

        public bool Delete(ApplicationUser user)
        {
            context.Users.Remove(user);
            return Save();
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            return await context.Users.Include(u => u.PersonalSpace)
                                      .Include(u => u.AssignedBugs)
                                      .Include(u => u.ProjectUserRoles).ToListAsync();
        }

        public async Task<ApplicationUser?> GetByIdAsync(string id)
        {
            return await context.Users.Where(u => u.Id == id).Include(u => u.PersonalSpace)
                                                             .Include(u => u.AssignedBugs)
                                                             .Include(u => u.ProjectUserRoles).FirstOrDefaultAsync();
        }

        public async Task<ApplicationUser?> GetAsync(ClaimsPrincipal claimsPrincipal)
        {
            ApplicationUser? user = await userManager.GetUserAsync(claimsPrincipal);
            return user == null ? null : await GetByIdAsync(user.Id);
        }

        public bool Save()
        {
            int savedCount = context.SaveChanges();
            return savedCount > 0;
        }

        public bool Update(ApplicationUser user)
        {
            context.Update(user);
            return Save();
        }
    }
}
