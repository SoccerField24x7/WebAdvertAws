using System;
using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;

namespace WebAdvert.Web.Services
{
    public class S3FileUploader : IFileUploader
    {
        public IConfiguration _configuration { get; }

        public S3FileUploader(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        public async Task<bool> UploadFileAsync(string fileName, Stream storageStream)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentException("File name must be specified.");

            var bucketName = _configuration.GetValue<string>("ImageBucket");
            
            using (var client = new AmazonS3Client())
            {
                if (storageStream.Length > 0)
                {
                    if (storageStream.CanSeek)
                    {
                        storageStream.Seek(0, SeekOrigin.Begin);
                    }
                }

                var request = new PutObjectRequest
                {
                    AutoCloseStream = true,
                    BucketName = bucketName,
                    InputStream = storageStream,
                    Key = fileName
                };

                var response = await client.PutObjectAsync(request);

                return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
        }
    }
}