using BugTrackingSystem.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTrackingSystem.Models.LinkingEntities.AssignBugToUser
{
    public class BugUserAssignation
    {
        [Required(ErrorMessage = "Assigned user id is required.")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Bug id is required.")]
        public string BugId { get; set; }

        // Navigartion properties
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        [ForeignKey(nameof(BugId))]
        public Bug Bug { get; set; }
    }
}
