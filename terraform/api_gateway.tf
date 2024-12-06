# Create the API Gateway
resource "aws_api_gateway_rest_api" "api" {
  name = "paramatic-api"
  body = templatefile("${path.module}/templates/swagger.yaml", {
    authorizer_uri = aws_lambda_function.authorizer.invoke_arn
    backend_url    = var.backend_url
    environment   = var.environment
  })

  endpoint_configuration {
    types = ["REGIONAL"]
  }
}

# Create /posts resource
resource "aws_api_gateway_resource" "posts" {
  rest_api_id = aws_api_gateway_rest_api.api.id
  parent_id   = aws_api_gateway_rest_api.api.root_resource_id
  path_part   = "posts"
}

# Create GET method
resource "aws_api_gateway_method" "get_posts" {
  rest_api_id   = aws_api_gateway_rest_api.api.id
  resource_id   = aws_api_gateway_resource.posts.id
  http_method   = "GET"
  authorization = "CUSTOM"
  authorizer_id = aws_api_gateway_authorizer.token_authorizer.id
}

# Create POST method
resource "aws_api_gateway_method" "post_posts" {
  rest_api_id   = aws_api_gateway_rest_api.api.id
  resource_id   = aws_api_gateway_resource.posts.id
  http_method   = "POST"
  authorization = "CUSTOM"
  authorizer_id = aws_api_gateway_authorizer.token_authorizer.id
}

# Create GET integration
resource "aws_api_gateway_integration" "get_posts" {
  rest_api_id = aws_api_gateway_rest_api.api.id
  resource_id = aws_api_gateway_resource.posts.id
  http_method = aws_api_gateway_method.get_posts.http_method
  type        = "MOCK"
  
  request_templates = {
    "application/json" = jsonencode({
      statusCode = 200
    })
  }
}

# Create POST integration
resource "aws_api_gateway_integration" "post_posts" {
  rest_api_id = aws_api_gateway_rest_api.api.id
  resource_id = aws_api_gateway_resource.posts.id
  http_method = aws_api_gateway_method.post_posts.http_method
  type        = "MOCK"
  
  request_templates = {
    "application/json" = jsonencode({
      statusCode = 201
    })
  }
}

# Create deployment
resource "aws_api_gateway_deployment" "api" {
  rest_api_id = aws_api_gateway_rest_api.api.id

  triggers = {
    redeployment = sha1(jsonencode([
      aws_api_gateway_integration.get_posts,
      aws_api_gateway_integration.post_posts
    ]))
  }

  depends_on = [
    aws_api_gateway_integration.get_posts,
    aws_api_gateway_integration.post_posts
  ]

  lifecycle {
    create_before_destroy = true
  }
}

# Then create stage
resource "aws_api_gateway_stage" "api" {
  deployment_id = aws_api_gateway_deployment.api.id
  rest_api_id   = aws_api_gateway_rest_api.api.id
  stage_name    = var.environment
}

# Create OPTIONS method
resource "aws_api_gateway_method" "cors" {
  rest_api_id   = aws_api_gateway_rest_api.api.id
  resource_id   = aws_api_gateway_rest_api.api.root_resource_id
  http_method   = "OPTIONS"
  authorization = "NONE"
}

# Create method response for OPTIONS
resource "aws_api_gateway_method_response" "cors" {
  rest_api_id   = aws_api_gateway_rest_api.api.id
  resource_id   = aws_api_gateway_rest_api.api.root_resource_id
  http_method   = aws_api_gateway_method.cors.http_method
  status_code   = "200"

  response_parameters = {
    "method.response.header.Access-Control-Allow-Headers" = true
    "method.response.header.Access-Control-Allow-Methods" = true
    "method.response.header.Access-Control-Allow-Origin"  = true
  }

  depends_on = [aws_api_gateway_method.cors]
}

# Add CloudWatch logging
resource "aws_api_gateway_account" "api" {
  cloudwatch_role_arn = aws_iam_role.cloudwatch.arn
}

# CloudWatch role
resource "aws_iam_role" "cloudwatch" {
  name = "api-gateway-cloudwatch-role"

  assume_role_policy = jsonencode({
    Version = "2012-10-17"
    Statement = [{
      Action = "sts:AssumeRole"
      Effect = "Allow"
      Principal = {
        Service = "apigateway.amazonaws.com"
      }
    }]
  })
}

# CloudWatch policy
resource "aws_iam_role_policy" "cloudwatch" {
  name = "api-gateway-cloudwatch-policy"
  role = aws_iam_role.cloudwatch.id

  policy = jsonencode({
    Version = "2012-10-17"
    Statement = [{
      Effect = "Allow"
      Action = [
        "logs:CreateLogGroup",
        "logs:CreateLogStream",
        "logs:DescribeLogGroups",
        "logs:DescribeLogStreams",
        "logs:PutLogEvents",
        "logs:GetLogEvents",
        "logs:FilterLogEvents"
      ]
      Resource = "*"
    }]
  })
} 