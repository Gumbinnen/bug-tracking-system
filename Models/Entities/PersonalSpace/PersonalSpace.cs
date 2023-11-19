using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BugTrackingSystem.Models.Entities
{
    public class PersonalSpace
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Space Name is required.")]
        [MaxLength(64, ErrorMessage = "Name cannot exceed 64 characters.")]
        public string SpaceName { get; set; }

        // Navigation properties
        [Required(ErrorMessage = "User is required.")]
        public ApplicationUser User { get; set; }

        public List<Project> Projects { get; set; }
    }
}
