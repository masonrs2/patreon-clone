# # Create Kinesis Firehose
# resource "aws_kinesis_firehose_delivery_stream" "example" {
#   name        = "example-firehose"
#   destination = "extended_s3"

#   extended_s3_configuration {
#     role_arn   = aws_iam_role.firehose_role.arn
#     bucket_arn = aws_s3_bucket.destination.arn
    
#     processing_configuration {
#       enabled = true

#       processors {
#         type = "Lambda"
#         parameters {
#           parameter_name  = "LambdaArn"
#           parameter_value = aws_lambda_function.transformer.arn
#         }
#       }
#     }
#   }
# }

# # S3 bucket for data destination
# resource "aws_s3_bucket" "destination" {
#   bucket = "my-firehose-destination-bucket"
# }

# # IAM role for Firehose
# resource "aws_iam_role" "firehose_role" {
#   name = "firehose-role"

#   assume_role_policy = jsonencode({
#     Version = "2012-10-17"
#     Statement = [{
#       Action = "sts:AssumeRole"
#       Effect = "Allow"
#       Principal = {
#         Service = "firehose.amazonaws.com"
#       }
#     }]
#   })
# }

# # IAM policy for Firehose to write to S│ Error: Reference to undeclared resource
# │ 
# │   on kinesis.tf line 17, in resource "aws_kinesis_firehose_delivery_stream" "example":
# │   17:           parameter_value = aws_lambda_function.transformer.arn
# │ 3
# resource "aws_iam_role_policy" "firehose_policy" {
#   name = "firehose-policy"
#   role = aws_iam_role.firehose_role.id

#   policy = jsonencode({
#     Version = "2012-10-17"
#     Statement = [
#       {
#         Effect = "Allow"
#         Action = [
#           "s3:PutObject",
#           "s3:GetObject",
#           "s3:ListBucket"
#         ]
#         Resource = [
#           aws_s3_bucket.destination.arn,
#           "${aws_s3_bucket.destination.arn}/*"
#         ]
#       }
#     ]
#   })
# }