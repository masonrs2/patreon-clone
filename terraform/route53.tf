resource "aws_route53_zone" "main" {
  count = var.environment == "prod" ? 1 : 0
  name  = "paramatic.com"
  
  tags = local.tags
}

# Output the zone ID for reference
output "route53_zone_id" {
  value = var.environment == "prod" ? aws_route53_zone.main[0].zone_id : null
}