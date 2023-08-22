using System.ComponentModel.DataAnnotations;

namespace TrackingApp.Application.DataTransferObjects.AuthenticationDTO.Authentication
{
    public class RegisterRequestModel
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9_-]{3,20}$", ErrorMessage = "Username must start with an alphabet and can only contain letters, numbers, hyphens and underscores")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 20 characters")]
        public string? UserName { get; set; }
        [Required]
        [Phone]
        public string? ContactNo { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 15 characters")]
        public string? Password { get; set; }
    }
}
