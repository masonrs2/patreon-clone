# Only create these resources in production
resource "aws_api_gateway_domain_name" "api" {
  count = var.environment == "prod" ? 1 : 0
  
  domain_name              = "api.paramatic.com"
  regional_certificate_arn = aws_acm_certificate.api[0].arn

  endpoint_configuration {
    types = ["REGIONAL"]
  }
}

resource "aws_route53_record" "api" {
  count = var.environment == "prod" ? 1 : 0
  
  name    = aws_api_gateway_domain_name.api[0].domain_name
  type    = "A"
  zone_id = var.route53_zone_id

  alias {
    name                   = aws_api_gateway_domain_name.api[0].regional_domain_name
    zone_id                = aws_api_gateway_domain_name.api[0].regional_zone_id
    evaluate_target_health = true
  }
} 