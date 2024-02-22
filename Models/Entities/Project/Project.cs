using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BugTrackingSystem.Helpers;
using BugTrackingSystem.Models.LinkingEntities;
using BugTrackingSystem.ViewModels.ProjectViewModels;

namespace BugTrackingSystem.Models.Entities
{
    public class Project
    {
        [Key]
        public string Id { get; private set; }

        [Required(ErrorMessage = "Personal Space ID is required.")]
        public string PersonalSpaceId { get; set; }

        [Required(ErrorMessage = "Project Name is required.")]
        [MaxLength(256, ErrorMessage = "Project Name cannot exceed 256 characters.")]
        public string Name { get; set; }

        [MaxLength(4000, ErrorMessage = "Description cannot exceed 4000 characters.")]
        public string? Description { get; set; }

        // Navigation properties
        [Required(ErrorMessage = "Personal Space is required.")]
        [ForeignKey(nameof(PersonalSpaceId))]
        public PersonalSpace PersonalSpace { get; set; }
        public List<Bug> Bugs { get; set; }
        public List<ApplicationProjectUserRole> ProjectUserRoles { get; set; }
        public List<ApplicationRole> CreatedRoles { get; set; }

        public Project()
        {
            Id = HashGenerator.GenerateRandomHash();
        }

        public Project(ProjectViewModel projectViewModel) : this()
        {
            PersonalSpaceId = projectViewModel.PersonalSpace.Id;
            Name = projectViewModel.Name;
            Description = projectViewModel.Description;
            Bugs = projectViewModel.Bugs.ToList();
            ProjectUserRoles = projectViewModel.ProjectUserRoles.ToList();
            CreatedRoles = projectViewModel.CreatedRoles.ToList();
        }

        public Project(string personalSpaceId, string name) : this()
        {
            PersonalSpaceId = personalSpaceId;
            Name = name;
        }

        public Project(string personalSpaceId, string name, string description) : this()
        {
            PersonalSpaceId = personalSpaceId;
            Name = name;
            Description = description;
        }
    }
}
