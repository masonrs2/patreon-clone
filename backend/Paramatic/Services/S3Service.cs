using Amazon.S3;
using Amazon.S3.Transfer;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Paramatic.Services
{
    public class S3Service
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public S3Service()
        {
            _s3Client = new AmazonS3Client(
                Environment.GetEnvironmentVariable("AWS_ACCESS_KEY"),
                Environment.GetEnvironmentVariable("AWS_SECRET_KEY"),
                Amazon.RegionEndpoint.GetBySystemName(Environment.GetEnvironmentVariable("AWS_REGION"))
            );
            _bucketName = Environment.GetEnvironmentVariable("AWS_BUCKET_NAME");
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
        {
            var fileTransferUtility = new TransferUtility(_s3Client);
            var key = $"videos/{Guid.NewGuid()}-{fileName}";

            await fileTransferUtility.UploadAsync(fileStream, _bucketName, key);
            
            return $"https://{_bucketName}.s3.amazonaws.com/{key}";
        }
    }
}