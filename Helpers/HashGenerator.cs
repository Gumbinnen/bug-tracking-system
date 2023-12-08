using System.Text;

namespace BugTrackingSystem.Helpers
{
    public class HashGenerator
    {
        public static string GenerateRandomHash()
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
        }
    }
}
