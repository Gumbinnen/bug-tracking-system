using BugTrackingSystem.Database;
using BugTrackingSystem.Interfaces;
using BugTrackingSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BugTrackingSystem.Repositories
{
    public class PersonalSpaceRepository : IPersonalSpaceRepository
    {
        private readonly ApplicationDBContext context;

        public PersonalSpaceRepository(ApplicationDBContext context)
        {
            this.context = context;
        }

        public async Task<bool> IsPersonalSpaceExistsForUser(ApplicationUser user)
        {
            return await context.PersonalSpaces.AnyAsync(ps => ps.UserId == user.Id);
        }

        public async Task<bool> AddAsync(PersonalSpace personalSpace)
        {
            await context.PersonalSpaces.AddAsync(personalSpace);
            return Save();
        }

        public bool Delete(PersonalSpace personalSpace)
        {
            context.PersonalSpaces.Remove(personalSpace);
            return Save();
        }

        public bool Save()
        {
            int savedCount = context.SaveChanges();
            return savedCount > 0;
        }

        public bool Update(PersonalSpace personalSpace)
        {
            context.PersonalSpaces.Update(personalSpace);
            return Save();
        }
    }
}
