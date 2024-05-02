using BugTrackingSystem.Database;
using BugTrackingSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BugTrackingSystem.Repositories.ProjectRepository
{
    public sealed class ProjectAccessibleToUserQueryBuilder
    {
        private readonly ApplicationDBContext context;
        private readonly ApplicationUser user;

        public ProjectAccessibleToUserQueryBuilder(ApplicationDBContext context, ApplicationUser user)
        {
            this.context = context;
            this.user = user;
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await context.UserRoles.Where(pur => pur.UserId == user.Id).Select(pur => pur.Project).ToListAsync();

        }

        public async Task<Project?> GetByIdAsync(string id)
        {
            return await context.UserRoles.Where(pur => pur.UserId == user.Id)
                                                    .Select(pur => pur.Project)
                                                    .Where(p => p.Id == id).FirstOrDefaultAsync();
        }
    }
}
