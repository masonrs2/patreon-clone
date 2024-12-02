resource "aws_dynamodb_table" "posts_table" {
    name = "Posts"
    billing_mode = "PAY_PER_REQUEST"
    hash_key = "id"
    attribute {
        name = "id"
        type = "S"
    }

    tags = {
        Environment = "dev"
        Project = "Paramatic"
    }
} 