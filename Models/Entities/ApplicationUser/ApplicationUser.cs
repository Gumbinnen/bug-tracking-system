using BugTrackingSystem.Helpers;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BugTrackingSystem.Models.Entities;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [PersonalData]
    [Required(ErrorMessage = "First Name is required.")]
    [MaxLength(32, ErrorMessage = "First Name cannot exceed 32 characters.")]
    public string FirstName { get; set; }

    [PersonalData]
    [Required(ErrorMessage = "Last Name is required.")]
    [MaxLength(32, ErrorMessage = "Last Name cannot exceed 32 characters.")]
    public string LastName { get; set; }

    public ApplicationUser()
    {
        Id = HashGenerator.GenerateRandomHash();
    }
}
