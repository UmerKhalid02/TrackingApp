using Microsoft.AspNetCore.Http;

namespace TrackingApp.Application.Helpers
{
    public class CommonHelper
    {
        public static byte[] ConvertToByteArray(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
