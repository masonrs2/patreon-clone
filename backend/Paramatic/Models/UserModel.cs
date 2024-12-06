using System;
using Amazon.DynamoDBv2.DataModel;

namespace Paramatic.Models
{
    [DynamoDBTable("Users")]
    public class User
    {
        [DynamoDBHashKey("id")]
        public string Id { get; set;} = Guid.NewGuid().ToString();

        [DynamoDBProperty]
        public string Username { get; set; } = string.Empty;

        [DynamoDBProperty]
        public string Email { get; set; } = string.Empty;

        [DynamoDBProperty]
        public string FirstName { get; set; } = string.Empty;

        [DynamoDBProperty]
        public string LastName { get; set; } = string.Empty;
    }
}