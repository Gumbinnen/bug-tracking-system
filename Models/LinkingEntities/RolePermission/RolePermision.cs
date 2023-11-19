using BugTrackingSystem.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BugTrackingSystem.Models.LinkingEntities
{
    public class RolePermission
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Role ID is required.")]
        [ForeignKey(nameof(Role))]
        public int RoleID { get; set; }

        [Required(ErrorMessage = "Permission ID is required.")]
        [ForeignKey(nameof(Permission))]
        public int PermissionID { get; set; }

        // Navigation properties
        [Required(ErrorMessage = "Role is required.")]
        public Role Role { get; set; }

        [Required(ErrorMessage = "Permission is required.")]
        public Permission Permission { get; set; }
    }
}
