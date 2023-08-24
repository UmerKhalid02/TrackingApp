namespace TrackingApp.Application.DataTransferObjects.AuthenticationDTO.Authentication
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
