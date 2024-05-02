using BugTrackingSystem.Database;
using BugTrackingSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BugTrackingSystem.Repositories.ProjectRepository
{
    public sealed class ProjectBelongingToUserQueryBuilder
    {
        private readonly ApplicationDBContext context;
        private readonly PersonalSpace personalSpace;

        public ProjectBelongingToUserQueryBuilder(ApplicationDBContext context, ApplicationUser user)
        {
            this.context = context;
            personalSpace = user.PersonalSpace ??
                throw new NullReferenceException("user.PersonalSpace shouldn't be null but happend to be.");
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await context.Projects.Include(p => p.Bugs)
                                         .Include(p => p.PersonalSpace)
                                         .Include(p => p.ProjectUserRoles)
                                         .Include(p => p.CreatedRoles)
                                         .Where(p => p.PersonalSpaceId == personalSpace.Id).ToListAsync();
        }

        public async Task<Project?> GetByIdAsync(string id)
        {
            return await context.Projects.Include(p => p.Bugs)
                                         .Include(p => p.PersonalSpace)
                                         .Include(p => p.ProjectUserRoles)
                                         .Include(p => p.CreatedRoles)
                                         .FirstOrDefaultAsync(p => p.PersonalSpaceId == personalSpace.Id && p.Id == id);
        }
    }
}
