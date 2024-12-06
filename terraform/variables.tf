variable "aws_region" {
  description = "AWS region"
  type        = string
  default     = "us-east-2"
}

variable "backend_url" {
  description = "The URL of the backend service"
  type        = string
}

variable "environment" {
  description = "Environment (dev/prod)"
  type        = string
}

variable "domain_name" {
  description = "Domain name for the API"
  type        = string
  default     = ""
}

variable "route53_zone_id" {
  description = "Route53 hosted zone ID"
  type        = string
  default     = ""  # Will be provided in prod.tfvars
}

variable "vpc_cidr" {
  description = "CIDR block for VPC"
  type        = string
  default     = "10.0.0.0/16"
}

variable "private_subnets" {
  description = "List of private subnet CIDR blocks"
  type        = list(string)
  default     = ["10.0.10.0/24", "10.0.11.0/24"]
}

variable "public_subnets" {
  description = "List of public subnet CIDR blocks"
  type        = list(string)
  default     = ["10.0.1.0/24", "10.0.2.0/24"]
}

variable "jwt_secret" {
  description = "Secret key for JWT token generation and validation"
  type        = string
  sensitive   = true  # This marks the variable as sensitive in Terraform logs
}

locals {
  tags = {
    Environment = var.environment
    Project     = "Paramatic"
    ManagedBy   = "Terraform"
  }

  vpc_link_id = var.environment == "prod" ? aws_api_gateway_vpc_link.this[0].id : ""
}