namespace TrackingApp.Application.DataTransferObjects.Shared
{
    public class AWSS3Model
    {
        public static string? AccessKey { get; set; }
        public static string? SecretKey { get; set; }
        public static string? Region { get; set; }
        public static string? BucketName { get; set; }
    }
}
