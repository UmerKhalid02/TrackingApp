namespace TrackingApp.Application.DataTransferObjects.AuthenticationDTO.Authentication
{
    public class LoginResponseDTO
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
