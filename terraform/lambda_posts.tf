resource "aws_lambda_function" "posts" {
  filename         = "./templates/posts.zip"
  function_name    = "posts-handler"
  role            = aws_iam_role.lambda_role.arn
  handler         = "posts.lambda_handler"
  runtime         = "python3.9"

  environment {
    variables = {
      DYNAMODB_TABLE = "Posts"
    }
  }
}

# Lambda permission for API Gateway
resource "aws_lambda_permission" "api_gateway_posts" {
  statement_id  = "AllowAPIGatewayInvoke"
  action        = "lambda:InvokeFunction"
  function_name = aws_lambda_function.posts.function_name
  principal     = "apigateway.amazonaws.com"
  source_arn    = "${aws_api_gateway_rest_api.api.execution_arn}/*/*"
}