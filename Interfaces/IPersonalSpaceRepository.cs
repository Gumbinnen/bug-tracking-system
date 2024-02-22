using BugTrackingSystem.Models.Entities;

namespace BugTrackingSystem.Interfaces
{
    public interface IPersonalSpaceRepository
    {
        Task<bool> IsPersonalSpaceExistsForUser(ApplicationUser user);

        Task<bool> AddAsync(PersonalSpace personalSpace);

        bool Update(PersonalSpace personalSpace);

        bool Delete(PersonalSpace personalSpace);

        bool Save();
    }
}
