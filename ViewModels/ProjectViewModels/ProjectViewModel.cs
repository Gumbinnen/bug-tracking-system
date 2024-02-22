using BugTrackingSystem.Models.Entities;
using BugTrackingSystem.Models.LinkingEntities;
using Microsoft.AspNetCore.Mvc;

namespace BugTrackingSystem.ViewModels.ProjectViewModels
{
    public class ProjectViewModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public PersonalSpace PersonalSpace { get; set; }
        public IEnumerable<Bug> Bugs { get; set; }
        public IEnumerable<ApplicationRole> CreatedRoles { get; set; }
        public IEnumerable<ApplicationProjectUserRole> ProjectUserRoles { get; set; }

        public ProjectViewModel()
        {
            
        }

        public ProjectViewModel(Project project) 
        {
            Name = project.Name;
            Description = project.Description;
            PersonalSpace = project.PersonalSpace;
            Bugs = project.Bugs;
            CreatedRoles = project.CreatedRoles;
            ProjectUserRoles = project.ProjectUserRoles;
        }
    }
}
