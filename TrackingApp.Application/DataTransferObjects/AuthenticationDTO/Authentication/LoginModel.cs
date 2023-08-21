namespace TrackingApp.Application.DataTransferObjects.AuthenticationDTO.Authentication
{
    public class LoginModel
    {
        public string ContactNo { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
