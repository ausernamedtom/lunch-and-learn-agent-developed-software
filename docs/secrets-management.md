# Secrets Management

This document outlines the approach for managing secrets and sensitive information in the Skill Management System.

## General Guidelines

1. **No Secrets in Source Control**:
   - Never commit actual secrets, passwords, or sensitive credentials to source control
   - Use `.env` files locally for development but do not commit these (they are in `.gitignore`)
   - Use sample files like `.env.sample` to document required environment variables

2. **Environment-Specific Management**:

   ### Local Development
   - Use local `.env` files for development (copy from `.env.sample` and add your values)
   - Database connection strings use Windows Authentication instead of passwords
   - In-memory database used for development to avoid credentials

   ### Production Deployment
   - Store deployment credentials in GitHub Secrets or Azure Key Vault
   - SQL admin passwords are provided at deployment time, not stored in code
   - Azure credentials are stored in GitHub Secrets for CI/CD pipelines

3. **Azure Resource Manager/Bicep**:
   - Parameters files do not contain actual passwords, only placeholders
   - The deployment script prompts for sensitive information interactively
   - Consider using Azure Key Vault for automated deployments in production

## Environment Variables Reference

### Backend API
```
ASPNETCORE_ENVIRONMENT=Development
API_URLS=http://localhost:5054;https://localhost:7294
```

### Frontend
```
REACT_APP_API_URL=http://localhost:5054/api
```

### Docker Compose
```
API_HTTP_PORT=5054
API_HTTPS_PORT=7294
ASPNET_ENVIRONMENT=Development
FRONTEND_PORT=3000
```

## CI/CD Pipeline Secrets

The GitHub Actions workflow uses the following secrets:

- `AZURE_CREDENTIALS`: Azure service principal credentials for deploying resources
- `AZURE_RG`: Azure resource group name
- `SQL_ADMIN_PASSWORD`: Password for the SQL admin user

These secrets should be configured in GitHub repository settings under "Secrets and variables" > "Actions".
