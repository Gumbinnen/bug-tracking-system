using BugTrackingSystem.Database;
using BugTrackingSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BugTrackingSystem.Repositories.ProjectRepository
{
    public class ForUserQueryBuilder
    {
        private readonly ApplicationDBContext context;
        private readonly Project project;
        private readonly ApplicationUser user;

        public ForUserQueryBuilder(ApplicationDBContext context, Project project, ApplicationUser user)
        {
            this.context = context;
            this.project = project;
            this.user = user;
        }

        public async Task<IEnumerable<Permission>> GetPermissions()
        {
            var roles = await context.UserRoles.Where(pur => pur.ProjectId == project.Id & pur.UserId == user.Id).Select(pur => pur.Role).ToListAsync();

            List<Permission> permissions = new();
            foreach (var role in roles)
            {
                permissions.AddRange(await context.RolePermissions.Where(rp => rp.RoleId == role.Id).Select(rp => rp.Permission).ToListAsync());
            }

            return permissions.Distinct();
        }
    }
}
