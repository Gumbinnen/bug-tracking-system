using BugTrackingSystem.Helpers;
using System.ComponentModel.DataAnnotations;

namespace BugTrackingSystem.Models.Entities
{
    public class Status
    {
        [Key]
        public string Id { get; private set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(256, ErrorMessage = "Name cannot exceed 256 characters.")]
        public string Name { get; set; }

        public Status()
        {
            Id = HashGenerator.GenerateRandomHash();
        }
    }
}
