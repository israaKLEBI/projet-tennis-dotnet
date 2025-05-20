using System.ComponentModel.DataAnnotations;

namespace TennisShopApp.Models
{
    public class UserProfileViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string DisplayName { get; set; } = string.Empty;

        public string RegistrationDate { get; set; } = string.Empty;
    }
}