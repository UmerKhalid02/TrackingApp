using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using TrackingApp.Application.DataTransferObjects.Shared;

namespace TrackingApp.Web.Extensions
{
    public class AWS
    {
        public async Task<bool> UploadImage(byte[] bytes, string filePath)
        {
            var credentails = new BasicAWSCredentials(AWSS3Model.AccessKey, AWSS3Model.SecretKey);

            using (var client = new AmazonS3Client(credentails, RegionEndpoint.APSouth1))
            {
                using (var newMemoryStream = new MemoryStream(bytes))
                {
                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = filePath,
                        BucketName = AWSS3Model.BucketName,
                        CannedACL = S3CannedACL.PublicRead
                    };

                    var fileTransferUtility = new TransferUtility(client);
                    await fileTransferUtility.UploadAsync(uploadRequest);
                }

            }
            return true;
        }
    }
}
