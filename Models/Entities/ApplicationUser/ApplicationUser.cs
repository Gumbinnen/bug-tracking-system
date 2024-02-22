using BugTrackingSystem.Helpers;
using BugTrackingSystem.Models.LinkingEntities;
using BugTrackingSystem.Models.LinkingEntities.AssignBugToUser;
using BugTrackingSystem.ViewModels.AccountViewModels;
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

    private ApplicationUser()
    {
        Id = HashGenerator.GenerateRandomHash();
    }

    public ApplicationUser(RegisterViewModel model) : this()
    {
        Email = model.Email;
        NormalizedEmail = NormalizeEmail(model.Email);
        UserName = model.UserName;
        NormalizedUserName = NormalizeUserName(model.UserName);
        FirstName = model.FirstName;
        LastName = model.LastName;
    }

    public ApplicationUser(string email, string userName) : this()
    {
        Email = email;
        NormalizedEmail = NormalizeEmail(email);
        UserName = userName;
    }

    public ApplicationUser(string email, string userName, string firstName) : this()
    {
        Email = email;
        NormalizedEmail = NormalizeEmail(email);
        UserName = userName;
        FirstName = firstName;
    }

    public static string? NormalizeEmail(string? email)
    {
        if (email == null)
        {
            return null;
        }

        return email.Trim().ToUpperInvariant();
    }

    public static string? NormalizeUserName(string? userName)
    {
        if (userName == null)
        {
            return null;
        }

        return userName.Trim().ToUpperInvariant();
    }
}
