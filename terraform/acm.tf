# Create ACM certificate for the API domain
resource "aws_acm_certificate" "api" {
  count = var.environment == "prod" ? 1 : 0
  
  domain_name       = "api.paramatic.com"
  validation_method = "DNS"

  tags = local.tags

  lifecycle {
    create_before_destroy = true
  }
}

# DNS validation record
resource "aws_route53_record" "acm_validation" {
  count = var.environment == "prod" ? 1 : 0
  
  name    = tolist(aws_acm_certificate.api[0].domain_validation_options)[0].resource_record_name
  type    = tolist(aws_acm_certificate.api[0].domain_validation_options)[0].resource_record_type
  zone_id = var.route53_zone_id
  records = [tolist(aws_acm_certificate.api[0].domain_validation_options)[0].resource_record_value]
  ttl     = 60
}

# Certificate validation
resource "aws_acm_certificate_validation" "api" {
  count = var.environment == "prod" ? 1 : 0
  
  certificate_arn         = aws_acm_certificate.api[0].arn
  validation_record_fqdns = [aws_route53_record.acm_validation[0].fqdn]
} 