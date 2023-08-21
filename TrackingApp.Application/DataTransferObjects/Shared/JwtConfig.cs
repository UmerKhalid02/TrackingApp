namespace TrackingApp.Application.DataTransferObjects.Shared
{
    public class JwtConfig
    {
        public static string Secret { get; set; }
        public static string ValidIssuer { get; set; }
        public static string ValidAudience { get; set; }
    }
}
