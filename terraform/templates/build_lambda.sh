#!/bin/bash

# Create a temporary directory
mkdir -p lambda_build
cd lambda_build

# Install dependencies using pip3
pip3 install pyjwt -t .

# Copy the lambda function
cp ../authorizer.py .

# Create the zip file
zip -r ../authorizer.zip .

# Clean up
cd ..
rm -rf lambda_build