# VPC for the backend
resource "aws_vpc" "main" {
  cidr_block           = "10.0.0.0/16"
  enable_dns_hostnames = true
  enable_dns_support   = true

  tags = merge(local.tags, {
    Name = "paramatic-${var.environment}-vpc"
  })
}

# Public subnets
resource "aws_subnet" "public" {
  count             = 2
  vpc_id            = aws_vpc.main.id
  cidr_block        = "10.0.${count.index + 1}.0/24"
  availability_zone = "${var.aws_region}${count.index == 0 ? "a" : "b"}"

  tags = merge(local.tags, {
    Name = "paramatic-${var.environment}-public-${count.index + 1}"
  })
}

# Private subnets
resource "aws_subnet" "private" {
  count             = 2
  vpc_id            = aws_vpc.main.id
  cidr_block        = "10.0.${count.index + 10}.0/24"
  availability_zone = "${var.aws_region}${count.index == 0 ? "a" : "b"}"

  tags = merge(local.tags, {
    Name = "paramatic-${var.environment}-private-${count.index + 1}"
  })
}

# VPC Link for API Gateway (only in production)
resource "aws_api_gateway_vpc_link" "this" {
  count       = var.environment == "prod" ? 1 : 0
  name        = "paramatic-${var.environment}-vpclink"
  target_arns = [aws_lb.backend[0].arn]

  tags = local.tags
}

# Network Load Balancer (only in production)
resource "aws_lb" "backend" {
  count              = var.environment == "prod" ? 1 : 0
  name               = "paramatic-${var.environment}-nlb"
  internal           = true
  load_balancer_type = "network"
  subnets            = aws_subnet.private[*].id

  tags = local.tags
}

# Load Balancer Target Group (only in production)
resource "aws_lb_target_group" "backend" {
  count       = var.environment == "prod" ? 1 : 0
  name        = "paramatic-${var.environment}-tg"
  port        = 80
  protocol    = "TCP"
  vpc_id      = aws_vpc.main.id
  target_type = "ip"

  health_check {
    enabled = true
    port    = 80
  }
}