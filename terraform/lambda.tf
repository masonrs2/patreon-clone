resource "aws_lambda_function" "authorizer" {
  filename         = "./templates/authorizer.zip"
  function_name    = "api-authorizer"
  role            = aws_iam_role.lambda_role.arn
  handler         = "authorizer.lambda_handler"
  runtime         = "python3.9"
  
  environment {
    variables = {
      JWT_SECRET = "your-secret-key"
    }
  }

  provisioner "local-exec" {
   command = "./templates/build_lambda.sh"
    working_dir = path.module
  }
}

resource "aws_api_gateway_authorizer" "token_authorizer" {
  name                   = "token-authorizer"
  rest_api_id           = aws_api_gateway_rest_api.api.id
  type                  = "TOKEN"
  authorizer_uri        = aws_lambda_function.authorizer.invoke_arn
  authorizer_credentials = aws_iam_role.invocation_role.arn
}
