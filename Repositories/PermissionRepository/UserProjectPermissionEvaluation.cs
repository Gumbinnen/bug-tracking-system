using BugTrackingSystem.Database;
using BugTrackingSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BugTrackingSystem.Repositories.PermissionRepository
{
    public class UserProjectPermissionEvaluation
    {
        private readonly ApplicationDBContext context;
        private readonly Project project;
        private readonly ApplicationUser user;

        public UserProjectPermissionEvaluation(ApplicationDBContext context, ApplicationUser user, Project project)
        {
            this.context = context;
            this.user = user;
            this.project = project;
        }

        public async Task<bool> HasPermission(Permission permission)
        {
            var permissions = await (
                from pur in context.UserRoles
                join rp in context.RolePermissions on pur.RoleId equals rp.RoleId
                where pur.UserId == user.Id & pur.ProjectId == project.Id
                select rp.Permission
                ).Distinct().ToListAsync();

            return permissions.Contains(permission);
        }
    }
}
