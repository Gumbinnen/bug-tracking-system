using BugTrackingSystem.Models.Entities;

namespace BugTrackingSystem.ViewModels.ProjectViewModels
{
    public class DetailsViewModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public ApplicationUser Creator { get; set; }
        public int BugCount { get; set; }
        public IEnumerable<ApplicationRole> CreatedRoles { get; set; }
    }
}
