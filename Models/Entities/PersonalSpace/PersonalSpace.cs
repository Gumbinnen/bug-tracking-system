using Microsoft.AspNetCore.Identity;

namespace BugTrackingSystem.Models.Entities
{
    public class PersonalSpace
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public string SpaceName { get; set; }
        public ApplicationUser User { get; set; }
        public List<Project> Projects { get; set; }
    }
}
