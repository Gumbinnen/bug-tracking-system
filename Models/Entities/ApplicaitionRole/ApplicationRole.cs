using BugTrackingSystem.Helpers;
using BugTrackingSystem.Models.LinkingEntities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTrackingSystem.Models.Entities
{
    public class ApplicationRole : IdentityRole
    {
        [MaxLength(4000, ErrorMessage = "Description cannot exceed 4000 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Created User ID is required.")]
        public string CreatedUserId { get; set; }

        [Required(ErrorMessage = "Created Project ID is required.")]
        public string CreatedProjectId { get; set; }

        [Required(ErrorMessage = "Created User is required.")]

        // Navigation properties
        [ForeignKey(nameof(CreatedUserId))]
        public ApplicationUser CreatedUser { get; set; }

        [Required(ErrorMessage = "Created Project is required.")]
        [ForeignKey(nameof(CreatedProjectId))]
        public Project CreatedProject { get; set; }
        public List<ApplicationUserRole> UserRoles { get; set; }
        public List<RolePermission> RolePermissions { get; set; }

        public ApplicationRole()
        {
            Id = HashGenerator.GenerateRandomHash();
        }
    }
}
