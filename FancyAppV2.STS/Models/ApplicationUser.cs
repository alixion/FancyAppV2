using Microsoft.AspNetCore.Identity;

namespace FancyAppV2.STS.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public bool IsAdmin { get; set; }
        public string Name { get; set; }
    }
}