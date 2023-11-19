using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTrackingSystem.Models.Entities
{
    public class Bug
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(256, ErrorMessage = "Title cannot exceed 256 characters.")]
        public string Title { get; set; }

        [MaxLength(4000, ErrorMessage = "Description cannot exceed 4000 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [ForeignKey(nameof(Status))]
        public int StatusID { get; set; }

        [Required(ErrorMessage = "Severity is required.")]
        [ForeignKey(nameof(Severity))]
        public int SeverityID { get; set; }

        [Required(ErrorMessage = "Priority is required.")]
        [ForeignKey(nameof(Priority))]
        public int PriorityID { get; set; }

        [Required(ErrorMessage = "Reporter is required.")]
        [ForeignKey(nameof(ApplicationUser))]
        public int ReporterID { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public int? AssignedToID { get; set; }

        [Required(ErrorMessage = "Created Date is required.")]
        [DataType(DataType.DateTime)]
        public DateTimeOffset CreatedDate { get; set; }

        [Required(ErrorMessage = "Modified Date is required.")]
        [DataType(DataType.DateTime)]
        public DateTimeOffset ModifiedDate { get; set; }

        [Required(ErrorMessage = "Project is required.")]
        [ForeignKey(nameof(Project))]
        public int ProjectID { get; set; }

        [MaxLength(32, ErrorMessage = "Version cannot exceed 32 characters.")]
        public string? Version { get; set; }

        [Required(ErrorMessage = "Due Date is required.")]
        [DataType(DataType.DateTime)]
        public DateTimeOffset DueDate { get; set; }

        // Navigation properties
        public Status Status { get; set; }

        public Severity Severity { get; set; }

        public Priority Priority { get; set; }

        [Required(ErrorMessage = "Reporter is required.")]
        public ApplicationUser Reporter { get; set; }

        public ApplicationUser? AssignedTo { get; set; }

        [Required(ErrorMessage = "Project is required.")]
        public Project Project { get; set; }
    }
}
