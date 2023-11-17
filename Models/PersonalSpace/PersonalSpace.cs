using BugTrackingSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace BugTrackingSystem.Models
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
