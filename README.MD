# Paramatic - A Patreon Clone

A full-stack platform that allows creators to monetize their content and connect with their supporters, built with modern web technologies.

## 🚀 Tech Stack

### Frontend
- React 18
- Vite
- TailwindCSS
- TypeScript

### Backend
- .NET 8
- AWS DynamoDB
- AWS S3
- PostgreSQL
- Entity Framework Core

## 🛠️ Prerequisites

- Node.js (v18 or higher)
- .NET 8 SDK
- AWS Account
- PostgreSQL
- Terraform (for infrastructure)

## 📦 Installation

1. Clone the repository:
2. Install frontend dependencies:
3. Install backend dependencies:
4. Set up your environment variables:
   - Create `.env` files in both frontend and backend directories
   - Configure AWS credentials
   - Set up database connection strings

## 🚀 Running the Application

### Frontend
1. cd frontend
2. npm install
2. npm run dev

### Backend
1. cd backend
2. dotnet run


## 🏗️ Infrastructure

The project uses Terraform to manage AWS infrastructure. To initialize:
1. cd terraform
2. terraform init
3. terraform plan
4. terraform apply