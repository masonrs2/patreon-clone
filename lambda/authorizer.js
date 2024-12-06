const jwt = require('jsonwebtoken');

exports.handler = async (event, context) => {
    try {
        // Get the Authorization token from the headers
        const token = event.headers?.Authorization?.replace('Bearer ', '');
        
        if (!token) {
            return generatePolicy('user', 'Deny', event.methodArn);
        }
        
        // Verify and decode the JWT token
        // Replace 'your-secret-key' with your actual secret key
        const decoded = jwt.verify(token, 'your-secret-key');
        
        // Check if token is expired
        if (decoded.exp < Math.floor(Date.now() / 1000)) {
            return generatePolicy('user', 'Deny', event.methodArn);
        }
        
        // Token is valid, allow the request
        return generatePolicy(decoded.sub, 'Allow', event.methodArn);
        
    } catch (error) {
        console.error('Error:', error);
        return generatePolicy('user', 'Deny', event.methodArn);
    }
};

const generatePolicy = (principalId, effect, resource) => {
    const authResponse = {
        principalId,
        policyDocument: {
            Version: '2012-10-17',
            Statement: [{
                Action: 'execute-api:Invoke',
                Effect: effect,
                Resource: resource
            }]
        }
    };
    
    // Optional: Include additional context
    authResponse.context = {
        userId: principalId,
        timeStamp: Date.now()
    };
    
    return authResponse;
};