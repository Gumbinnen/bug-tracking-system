using BugTrackingSystem.Helpers;
using BugTrackingSystem.Models.LinkingEntities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BugTrackingSystem.Models.Entities
{
    public class ApplicationRole : IdentityRole
    {
        [MaxLength(4000, ErrorMessage = "Description cannot exceed 4000 characters.")]
        public string? Description { get; set; }

        // Navigation properties
        public List<ApplicationProjectUserRole> ProjectUserRoles { get; set; }
        public List<RolePermission> RolePermissions { get; set; }

        public ApplicationRole()
        {
            Id = HashGenerator.GenerateRandomHash();
        }

        public ApplicationRole(string name) : this()
        {
            Name = name;
            NormalizedName = NormalizeName(name);
        }

        private static string? NormalizeName(string name)
        {
            if (name == null)
            {
                return null;
            }

            return name.Trim().ToUpperInvariant();
        }
    }
}
