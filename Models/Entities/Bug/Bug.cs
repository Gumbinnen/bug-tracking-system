using BugTrackingSystem.Helpers;
using BugTrackingSystem.Models.LinkingEntities.AssignBugToUser;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTrackingSystem.Models.Entities
{
    public class Bug
    {
        [Key]
        public string Id { get; private set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(256, ErrorMessage = "Title cannot exceed 256 characters.")]
        public string Title { get; set; }

        [MaxLength(4000, ErrorMessage = "Description cannot exceed 4000 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public string StatusId { get; set; }

        [Required(ErrorMessage = "Severity is required.")]
        public string SeverityId { get; set; }

        [Required(ErrorMessage = "Priority is required.")]
        public string PriorityId { get; set; }

        [Required(ErrorMessage = "Reporter is required.")]
        public string ReporterId { get; set; }

        [Required(ErrorMessage = "Created Date is required.")]
        [DataType(DataType.DateTime)]
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.Now;

        [Required(ErrorMessage = "Modified Date is required.")]
        [DataType(DataType.DateTime)]
        public DateTimeOffset ModifiedDate { get; set; }

        [Required(ErrorMessage = "Project is required.")]
        public string ProjectId { get; set; }

        [MaxLength(32, ErrorMessage = "Version cannot exceed 32 characters.")]
        public string? Version { get; set; }

        [DataType(DataType.DateTime)]
        public DateTimeOffset? DueDate { get; set; }

        // Navigation properties
        public Status Status { get; set; }
        public Severity Severity { get; set; }
        public Priority Priority { get; set; }

        [ForeignKey(nameof(ReporterId))]
        public ApplicationUser Reporter { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public Project Project { get; set; }
        public List<BugUserAssignation> AssignedUsers { get; set; }

        public Bug()
        {
            Id = HashGenerator.GenerateRandomHash();
        }
    }
}
