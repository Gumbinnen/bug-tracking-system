using BugTrackingSystem.Helpers;
using System.ComponentModel.DataAnnotations;

namespace BugTrackingSystem.Models.Entities
{
    public class Severity
    {
        [Key]
        public string Id { get; private set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(256, ErrorMessage = "Name cannot exceed 256 characters.")]
        public string Name { get; set; }

        public Severity()
        {
            Id = HashGenerator.GenerateRandomHash();
        }
    }
}
