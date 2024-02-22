using System.ComponentModel.DataAnnotations;

namespace BugTrackingSystem.ViewModels.AccountViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name (optional)")]
        public string? LastName { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "The password must be at least 6 characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string? ReturnUrl { get; set; }

        public ExternalLoginsViewModel? ExternalLogins { get; set; } = default!;
    }
}
