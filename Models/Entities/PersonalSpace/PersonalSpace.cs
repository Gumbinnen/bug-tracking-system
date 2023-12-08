using BugTrackingSystem.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTrackingSystem.Models.Entities
{
    public class PersonalSpace
    {
        [Key]
        public string Id { get; private set; }

        [Required(ErrorMessage = "User ID is required.")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Space Name is required.")]
        [MaxLength(64, ErrorMessage = "Name cannot exceed 64 characters.")]
        public string SpaceName { get; set; }

        // Navigation properties
        [Required(ErrorMessage = "User is required.")]
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
        public List<Project> Projects { get; set; }

        public PersonalSpace()
        {
            Id = HashGenerator.GenerateRandomHash();
        }
    }
}
