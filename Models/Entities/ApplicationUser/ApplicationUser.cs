using BugTrackingSystem.Helpers;
using BugTrackingSystem.Models.LinkingEntities;
using BugTrackingSystem.Models.LinkingEntities.AssignBugToUser;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BugTrackingSystem.Models.Entities;

public class ApplicationUser : IdentityUser
{
    [PersonalData]
    [MaxLength(32, ErrorMessage = "First Name cannot exceed 32 characters.")]
    public string? FirstName { get; set; }

    [PersonalData]
    [MaxLength(32, ErrorMessage = "Last Name cannot exceed 32 characters.")]
    public string? LastName { get; set; }

    // Navigation Properties
    public PersonalSpace PersonalSpace { get; set; }
    public List<BugUserAssignation> AssignedBugs { get; set; }
    public List<ApplicationProjectUserRole> ProjectUserRoles { get; set; }

    public ApplicationUser()
    {
        Id = HashGenerator.GenerateRandomHash();
    }
    public ApplicationUser(string email) : this()
    {
        Email = email;
        NormalizedEmail = NormalizeEmail(email);
    }
    public static string? NormalizeEmail(string email)
    {
        if (email == null)
        {
            return null;
        }

        return email.Trim().ToUpperInvariant();
    }
}
