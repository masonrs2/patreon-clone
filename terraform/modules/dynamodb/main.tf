resource "aws_dynamodb_table" "posts_table" {
  name           = "Posts"
  billing_mode   = "PAY_PER_REQUEST"  # Stays within free tier
  hash_key       = "Id"

  attribute {
    name = "Id"
    type = "S"
  }

  tags = {
    Environment = "Development"
    Project     = "Paramatic"
  }
}