using BugTrackingSystem.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTrackingSystem.Models.LinkingEntities
{
    public class UserRole : IdentityUserRole<string>
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        [ForeignKey(nameof(User))]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Role ID is required.")]
        [ForeignKey(nameof(Role))]
        public int RoleID { get; set; }

        [Required(ErrorMessage = "Project ID is required.")]
        [ForeignKey(nameof(Project))]
        public int ProjectID { get; set; }

        [Required(ErrorMessage = "Created By User ID is required.")]
        [ForeignKey(nameof(ApplicationUser))]
        public int CreatedByUserID { get; set; }

        // Navigation properties
        [Required(ErrorMessage = "User is required.")]
        public ApplicationUser User { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public Role Role { get; set; }

        [Required(ErrorMessage = "Project is required.")]
        public Project Project { get; set; }

        [Required(ErrorMessage = "Created By User is required.")]
        public ApplicationUser CreatedByUser { get; set; }
    }

}
