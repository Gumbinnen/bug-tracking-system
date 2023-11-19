using Microsoft.AspNetCore.Identity;

namespace BugTrackingSystem.Models.Entities
{
    public class Bug
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int StatusID { get; set; }
        public int SeverityID { get; set; }
        public int PriorityID { get; set; }
        public int ReporterID { get; set; }
        public int AssignedToID { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
        public int ProjectID { get; set; }
        public string Version { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public string Category { get; set; }
        public Status Status { get; set; }
        public Severity Severity { get; set; }
        public Priority Priority { get; set; }
        public ApplicationUser Reporter { get; set; }
        public ApplicationUser AssignedTo { get; set; }
        public Project Project { get; set; }
    }
}
