using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace Paramatic.Config
{
    public static class DynamoDBConfig
    {
        public static AmazonDynamoDBClient CreateClient()
        {
            return new AmazonDynamoDBClient(new AmazonDynamoDBConfig
            {
                RegionEndpoint = Amazon.RegionEndpoint.USEast2
            });
        }

        public static DynamoDBContext CreateContext()
        {
            var client = CreateClient();
            return new DynamoDBContext(client);
        }
    }
} 