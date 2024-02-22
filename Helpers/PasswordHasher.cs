using BugTrackingSystem.Models.Entities;
using Microsoft.AspNetCore.Identity;
using BC = BCrypt.Net.BCrypt;

namespace BugTrackingSystem.Helpers
{
    public class PasswordHasher : IPasswordHasher<ApplicationUser>
    {
        private const int WorkFactor = 11;

        public string HashPassword(ApplicationUser user, string password)
        {
            return BC.EnhancedHashPassword(password, WorkFactor);
        }

        public PasswordVerificationResult VerifyHashedPassword(ApplicationUser user, string hashedPassword, string providedPassword)
        {
            return BC.EnhancedVerify(providedPassword, hashedPassword) ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
        }
    }
}
