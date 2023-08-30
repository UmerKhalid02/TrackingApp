using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using TrackingApp.Application.DataTransferObjects.Shared;

namespace TrackingApp.Web.Extensions
{
    public class AWS 
    {
        private readonly BasicAWSCredentials _credentials;
        public AWS()
        {
            _credentials = new BasicAWSCredentials(AWSS3Model.AccessKey, AWSS3Model.SecretKey);
        }
        public async Task<bool> UploadImage(byte[] bytes, string filePath)
        {
            using (var client = new AmazonS3Client(_credentials, RegionEndpoint.APSouth1))
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

        // reads image data from S3 bucket and returns base64 string
        public async Task<string> ReadImageData(string fileName)
        {
            try
            {
                using (var client = new AmazonS3Client(_credentials, RegionEndpoint.APSouth1))
                {
                    var request = new GetObjectRequest
                    {
                        BucketName = AWSS3Model.BucketName,
                        Key = fileName
                    };

                    using (var getObjectResponse = await client.GetObjectAsync(request))
                    {
                        using (var responseStream = getObjectResponse.ResponseStream)
                        {
                            var stream = new MemoryStream();
                            await responseStream.CopyToAsync(stream);
                            stream.Position = 0;
                            byte[] bytes = stream.ToArray();
                            string base64String = Convert.ToBase64String(bytes);
                            var types = GetMimeTypes();
                            var ext = Path.GetExtension(fileName).ToLowerInvariant();
                            var type = types[ext];
                            string base64 = "data:" + types[ext] + ";base64," + base64String;
                            return base64;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Read object operation failed.", exception);
            }
        }

        public async Task<bool> DeleteImage(string filePath)
        {
            using (var client = new AmazonS3Client(_credentials, RegionEndpoint.APSouth1))
            {
                DeleteObjectRequest request = new DeleteObjectRequest
                {
                    BucketName = AWSS3Model.BucketName,
                    Key = filePath
                };
                await client.DeleteObjectAsync(request);
            }
            return true;
        }

        public Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"}
            };
        }

    }
}
