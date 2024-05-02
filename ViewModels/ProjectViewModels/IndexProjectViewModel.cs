using BugTrackingSystem.Enums;
using BugTrackingSystem.Interfaces;
using BugTrackingSystem.Models.Entities;

namespace BugTrackingSystem.ViewModels.ProjectViewModels
{
    public class IndexProjectViewModel : ISortableSearchable
    {
        public IEnumerable<Project> Projects { get; set; }
        public string SortColumn { get; set; }
        SortDirection ISortableSearchable.SortDirection { get; set; }
        public string SearchTerm { get; set; }
    }
}
