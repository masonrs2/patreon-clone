terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 5.0"
    }
  }


}

# Default variables that will be used unless overridden
locals {
  environment = terraform.workspace
  backend_url = {
    dev  = "http://localhost:5217"
    prod = "https://api.paramatic.com"
  }
  jwt_secret = {
    dev  = "1mnUU7IZwFwNCbQ3zLweMBDLeSn2aefASDSGbm3DDRw="
  }
}