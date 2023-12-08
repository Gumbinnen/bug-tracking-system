using BugTrackingSystem.Helpers;
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

        public Permission()
        {
            Id = HashGenerator.GenerateRandomHash();
        }
    }
}
