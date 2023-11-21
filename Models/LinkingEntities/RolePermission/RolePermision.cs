using BugTrackingSystem.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BugTrackingSystem.Models.LinkingEntities
{
    public class RolePermission
    {
        [Key]
        public string Id { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public string RoleId { get; set; }

        [Required(ErrorMessage = "Permission is required.")]
        public string PermissionId { get; set; }

        // Navigation properties
        [ForeignKey(nameof(RoleId))]
        public ApplicationRole Role { get; set; }

        [ForeignKey(nameof(PermissionId))]
        public Permission Permission { get; set; }
    }
}
