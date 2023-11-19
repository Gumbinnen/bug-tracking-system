using BugTrackingSystem.Models.LinkingEntities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTrackingSystem.Models.Entities
{
    public class Role : IdentityRole
    {
        [MaxLength(4000, ErrorMessage = "Description cannot exceed 4000 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Created User ID is required.")]
        [ForeignKey(nameof(ApplicationUser))]
        public int CreatedUserID { get; set; }

        [Required(ErrorMessage = "Created Project ID is required.")]
        [ForeignKey(nameof(Project))]
        public int CreatedProjectID { get; set; }

        [Required(ErrorMessage = "Created User is required.")]
        public ApplicationUser CreatedUser { get; set; }

        [Required(ErrorMessage = "Created Project is required.")]
        public Project CreatedProject { get; set; }

        // Navigation properties
        public List<UserRole> UserRoles { get; set; }

        public List<RolePermission> RolePermissions { get; set; }
    }
}
