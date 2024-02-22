using BugTrackingSystem.Helpers;
using BugTrackingSystem.Models.LinkingEntities;
using System.ComponentModel.DataAnnotations;

namespace BugTrackingSystem.Models.Entities
{
    public class Permission
    {
        [Key]
        public string Id { get; private set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(256, ErrorMessage = "Name cannot exceed 256 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "NormalizedName is required.")]
        [StringLength(256, ErrorMessage = "Name cannot exceed 256 characters.")]
        public string NormalizedName { get; set; }

        // Navigation properties
        public List<RolePermission> RolePermissions { get; set; }

        public Permission(string name)
        {
            Id = HashGenerator.GenerateRandomHash();
            Name = name;
            NormalizedName = NormalizeName(name);
        }

        public static string NormalizeName(string name)
        {
            return name.Trim().ToUpperInvariant().Replace(" ", "_") ?? string.Empty;
        }

    }
}
