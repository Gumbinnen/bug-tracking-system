using BugTrackingSystem.Database;
using BugTrackingSystem.Models.Entities;

namespace BugTrackingSystem.Repositories.ProjectRepository
{
    public class WithProjectQueryBuilder
    {
        private readonly ApplicationDBContext context;
        private readonly Project project;

        public WithProjectQueryBuilder(ApplicationDBContext context, Project project)
        {
            this.context = context;
            this.project = project;
        }

        public ForUserQueryBuilder ForUser(ApplicationUser user)
        {
            return new ForUserQueryBuilder(context, project, user);
        }
    }
}
