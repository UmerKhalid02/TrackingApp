namespace TrackingApp.Application.DataTransferObjects.AuthenticationDTO.Authentication
{
    public class LogoutRequestModel
    {
        public Guid UserId { get; set; }
        public string RefreshToken { get; set; }
    }
}
