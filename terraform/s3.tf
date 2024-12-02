resource "aws_s3_bucket" "video_bucket" {
   bucket = "paramatic-video-posts"
}

resource "aws_s3_bucket_cors_configuration" "video_bucket_cors" {
    bucket = aws_s3_bucket.video_bucket.id

    cors_rule {
        allowed_headers = ["*"]
        allowed_methods = ["GET", "POST", "PUT", "DELETE"]
        allowed_origins = ["*"]
        expose_headers = ["ETag"]
        max_age_seconds = 3000
    }
}   