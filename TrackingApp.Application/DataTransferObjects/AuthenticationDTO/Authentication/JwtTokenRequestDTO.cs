namespace TrackingApp.Application.DataTransferObjects.AuthenticationDTO.Authentication
{
    public class JwtTokenRequestDTO
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
