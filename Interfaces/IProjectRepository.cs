using BugTrackingSystem.Models.Entities;
using BugTrackingSystem.Repositories.ProjectRepository;

namespace BugTrackingSystem.Interfaces
{
    public interface IProjectRepository
    {
        ProjectBelongingToUserQueryBuilder BelongingToUser(ApplicationUser user);

        ProjectAccessibleToUserQueryBuilder AccessibleToUser(ApplicationUser user);

        bool Add(Project project);

        bool Update(Project project);

        bool Delete(Project project);

        bool Save();
    }
}
