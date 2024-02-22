using BugTrackingSystem.Models.Entities;
using BugTrackingSystem.Repositories.ProjectRepository;

namespace BugTrackingSystem.Interfaces
{
    public interface IProjectRepository
    {
        ProjectByUserQueryBuilder BelongingToUser(ApplicationUser user);

        WithProjectQueryBuilder WithProject(Project project);

        bool Add(Project project);

        bool Update(Project project);

        bool Delete(Project project);

        bool Save();
    }
}
