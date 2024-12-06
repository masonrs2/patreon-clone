using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Paramatic.Services
{
    public class S3Service
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName = "paramatic-video-posts";

        public S3Service()
        {
            _s3Client = new AmazonS3Client(new AmazonS3Config
            {
                RegionEndpoint = Amazon.RegionEndpoint.USEast2
            });
        }

        public async Task<string> UploadVideoAsync(IFormFile file, string creatorId)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("No file provided");

            string fileExtension = Path.GetExtension(file.FileName);
            string fileName = $"{creatorId}/{Guid.NewGuid()}{fileExtension}";

            try
            {
                using var stream = file.OpenReadStream();
                var uploadRequest = new PutObjectRequest
                {
                    BucketName = _bucketName,
                    Key = fileName,
                    InputStream = stream,
                    ContentType = file.ContentType
                };

                await _s3Client.PutObjectAsync(uploadRequest);
                return $"https://{_bucketName}.s3.amazonaws.com/{fileName}";
            }
            catch (Exception ex)
            {
                throw new Exception($"Error uploading video: {ex.Message}");
            }
        }
    }
}