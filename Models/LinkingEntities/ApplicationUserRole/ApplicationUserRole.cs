using BugTrackingSystem.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTrackingSystem.Models.LinkingEntities
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        [Required(ErrorMessage = "Project ID is required.")]
        public string ProjectId { get; set; }

        [Required(ErrorMessage = "Created By User ID is required.")]
        public string CreatedByUserId { get; set; }

        // Navigation properties
        [Required(ErrorMessage = "User is required.")]
        public ApplicationUser ApplicationUser { get; set; }

        [Required(ErrorMessage = "Project is required.")]
        public Project Project { get; set; }

        [Required(ErrorMessage = "Created By User is required.")]
        [ForeignKey(nameof(CreatedByUserId))]
        public ApplicationUser CreatedByUser { get; set; }
    }
}
