using Amazon.DynamoDBv2.DataModel;

namespace Paramatic.Models
{
    [DynamoDBTable("Posts")]
    public class Post
    {
        [DynamoDBHashKey("id")]
        public string id { get; set; } = Guid.NewGuid().ToString();

        [DynamoDBProperty]
        public string Title { get; set; } = string.Empty;

        [DynamoDBProperty]
        public string Description { get; set; } = string.Empty;

        [DynamoDBProperty]
        public string ImagePreview { get; set; } = string.Empty;

        [DynamoDBProperty]
        public string VideoUrl { get; set; } = string.Empty;

        [DynamoDBProperty]
        public string CreatorId { get; set; } = string.Empty;

        [DynamoDBProperty]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [DynamoDBIgnore]
        public IFormFile? VideoContent { get; set; }
    }
}