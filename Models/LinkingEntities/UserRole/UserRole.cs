using BugTrackingSystem.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace BugTrackingSystem.Models.LinkingEntities
{
    public class UserRole : IdentityUserRole<string>
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public int ProjectID { get; set; }
        public int CreatedByUserID { get; set; }
        public ApplicationUser User { get; set; }
        public Role Role { get; set; }
        public Project Project { get; set; }
        public ApplicationUser CreatedByUser { get; set; }
    }
}
