using BugTrackingSystem.Database;
using BugTrackingSystem.Interfaces;
using BugTrackingSystem.Models.Entities;

namespace BugTrackingSystem.Repositories.ProjectRepository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDBContext context;

        public ProjectRepository(ApplicationDBContext context)
        {
            this.context = context;
        }

        public ProjectAccessibleToUserQueryBuilder AccessibleToUser(ApplicationUser user)
        {
            return new ProjectAccessibleToUserQueryBuilder(context, user);
        }

        public bool Add(Project project)
        {
            context.Add(project);
            return Save();
        }

        public ProjectBelongingToUserQueryBuilder BelongingToUser(ApplicationUser user)
        {
            return new ProjectBelongingToUserQueryBuilder(context, user);
        }

        public bool Delete(Project project)
        {
            context.Remove(project);
            return Save();
        }

        public bool Save()
        {
            int savedCount = context.SaveChanges();
            return savedCount > 0;
        }

        public bool Update(Project project)
        {
            context.Update(project);
            return Save();
        }
    }
}
