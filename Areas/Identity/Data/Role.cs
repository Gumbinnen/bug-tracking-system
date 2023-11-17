using BugTrackingSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTrackingSystem.Models
{
    public class Role : IdentityRole
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(4000)")]
        public string Description { get; set; }
        [PersonalData]
        public int CreatedUserID { get; set; }
        [PersonalData]
        public int CreatedProjectID { get; set; }
        [PersonalData]
        public ApplicationUser CreatedUser { get; set; }
        [PersonalData]
        public Project CreatedProject { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public List<RolePermission> RolePermissions { get; set; }
    }
}
