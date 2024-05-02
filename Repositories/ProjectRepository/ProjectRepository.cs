using BugTrackingSystem.Database;
using BugTrackingSystem.Interfaces.Repository;
using BugTrackingSystem.Models.Entities;
using System.ComponentModel;

namespace BugTrackingSystem.Repositories.ProjectRepository
{
    public sealed class ProjectRepository : IProjectRepository
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
