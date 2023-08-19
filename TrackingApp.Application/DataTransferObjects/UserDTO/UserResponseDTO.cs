

using System.ComponentModel.DataAnnotations.Schema;
namespace TrackingApp.Application.DataTransferObjects.UserDTO
{
    public class UserResponseDTO
    {
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public string? ContactNo { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
    }
}
