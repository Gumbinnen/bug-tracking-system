using BugTrackingSystem.Models.Entities;

namespace BugTrackingSystem.ViewModels.ProjectViewModels
{
    public class IndexProjectViewModel
    {
        public IEnumerable<Project> Projects { get; set; }

        public IndexProjectViewModel(IEnumerable<Project> projects)
        {
            this.Projects = projects;
        }
    }
}
