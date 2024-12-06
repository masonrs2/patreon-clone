resource "aws_dynamodb_table" "posts_table" {
    name = "Posts"
    billing_mode = "PAY_PER_REQUEST"
    hash_key = "id"
    attribute {
        name = "id"
        type = "S"
    }
} 
       
resource "aws_dynamodb_table" "users_table" {
    name = "Users"
    billing_mode = "PAY_PER_REQUEST"
    hash_key = "id"
    attribute {
        name = "id"
        type = "S"
    }

    attribute {
        name = "Email"
        type = "S"
    }

    global_secondary_index {
        name = "EmailIndex"
        hash_key = "Email"
        projection_type = "ALL"
    }
}