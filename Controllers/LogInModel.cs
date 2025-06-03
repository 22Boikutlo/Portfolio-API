using System.ComponentModel.DataAnnotations;

namespace PortfolioAPI.Controllers
{
    public class LogInModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
