using System.ComponentModel.DataAnnotations;

namespace TrackingApp.Application.DataTransferObjects.AuthenticationDTO.Authentication
{
    public class LoginRequestModel
    {
        [Required]
        [MaxLength(20, ErrorMessage = "UserName must be less than 20 characters")]
        public string UserName { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 15 characters")]
        public string Password { get; set; }
    }
}
