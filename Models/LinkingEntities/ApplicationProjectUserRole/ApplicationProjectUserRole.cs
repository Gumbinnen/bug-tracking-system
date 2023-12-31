using BugTrackingSystem.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTrackingSystem.Models.LinkingEntities
{
    public class ApplicationProjectUserRole : IdentityUserRole<string>
    {
        [Required(ErrorMessage = "Project ID is required.")]
        public string ProjectId { get; set; }

        // Navigation properties
        [Required(ErrorMessage = "User is required.")]
        public ApplicationUser User { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public ApplicationRole Role { get; set; }

        [Required(ErrorMessage = "Project is required.")]
        public Project Project { get; set; }

        public ApplicationProjectUserRole()
        { }
        public ApplicationProjectUserRole(string projectId, string userId, string roleId)
        {
            this.ProjectId = projectId;
            this.UserId = userId;
            this.RoleId = roleId;
        }
    }
}
