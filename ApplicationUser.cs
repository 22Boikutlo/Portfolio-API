using Microsoft.AspNetCore.Identity;

namespace PortfolioAPI
{
    public class ApplicationUser: IdentityUser
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
    
}
