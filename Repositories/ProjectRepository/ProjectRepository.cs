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

        public ProjectByUserQueryBuilder BelongingToUser(ApplicationUser user)
        {
            return new ProjectByUserQueryBuilder(context, user);
        }

        public WithProjectQueryBuilder WithProject(Project project)
        {
            return new WithProjectQueryBuilder(context, project);
        }

        public bool Add(Project project)
        {
            context.Add(project);
            return Save();
        }

        public bool Update(Project project)
        {
            context.Update(project);
            return Save();
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
    }
}
