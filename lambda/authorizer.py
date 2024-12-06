import json
import time
import os
import jwt

def lambda_handler(event, context):
    try:
        # Get the Authorization token from the headers
        token = event['headers'].get('Authorization', '').replace('Bearer ', '')
        
        if not token:
            return generate_policy('user', 'Deny', event['methodArn'])
            
        # Verify and decode the JWT token
        # Replace 'your-secret-key' with your actual secret key
        decoded_token = jwt.decode(token, 'your-secret-key', algorithms=['HS256'])
        
        # Check if token is expired
        if decoded_token['exp'] < time.time():
            return generate_policy('user', 'Deny', event['methodArn'])
            
        # Token is valid, allow the request
        return generate_policy(decoded_token['sub'], 'Allow', event['methodArn'])
        
    except Exception as e:
        print(f"Error: {str(e)}")
        return generate_policy('user', 'Deny', event['methodArn'])

def generate_policy(principal_id, effect, resource):
    auth_response = {
        'principalId': principal_id,
        'policyDocument': {
            'Version': '2012-10-17',
            'Statement': [{
                'Action': 'execute-api:Invoke',
                'Effect': effect,
                'Resource': resource
            }]
        }
    }
    
    # Optional: Include additional context
    auth_response['context'] = {
        'userId': principal_id,
        'timeStamp': str(time.time())
    }
    
    return auth_response