using BugTrackingSystem.Database;
using BugTrackingSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BugTrackingSystem.Repositories.ProjectRepository
{
    public class ProjectByUserQueryBuilder
    {
        private readonly ApplicationDBContext context;
        private readonly PersonalSpace personalSpace;

        public ProjectByUserQueryBuilder(ApplicationDBContext context, ApplicationUser user)
        {
            this.context = context;
            personalSpace = user.PersonalSpace;
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