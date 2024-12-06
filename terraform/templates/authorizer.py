import json
import time
import os
import jwt

# Add this function to generate tokens
def generate_token(user_id):
    payload = {
        'sub': user_id,  # subject (user id)
        'iat': int(time.time()),  # issued at
        'exp': int(time.time()) + 3600,  # expires in 1 hour
    }
    
    token = jwt.encode(payload, 'your-secret-key', algorithm='HS256')
    return token

def lambda_handler(event, context):
    try:
        # Get the Authorization token from the headers
        token = event['headers'].get('Authorization', '').replace('Bearer ', '')
        
        if not token:
            return generate_policy('user', 'Deny', event['methodArn'])
            
        # Get secret from environment variable
        secret_key = os.environ['JWT_SECRET']
        print(f"Token: {token}")  # Add logging
        print(f"Secret: {secret_key}")  # Add logging
        decoded_token = jwt.decode(token, secret_key, algorithms=['HS256'])
        
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
