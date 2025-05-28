# Azure Deployment for Skill Management System

This directory contains the infrastructure as code (IaC) setup for deploying the Skill Management application to Azure using Bicep.

## Infrastructure Overview

The Bicep template (`main.bicep`) deploys the following Azure resources:

- **App Service Plan**: Hosts both the frontend and backend applications
- **App Services**:
  - Frontend Web App (React)
  - Backend API App (ASP.NET Core)
- **SQL Server and Database**: For data persistence
- **Application Insights**: For monitoring both applications

## Deployment Environments

There are parameter files for different environments:
- `parameters.dev.json`: Development environment (Basic tier)
- `parameters.prod.json`: Production environment (Standard tier)

## How to Deploy

### Prerequisites

1. Install Azure CLI
   ```
   brew install azure-cli
   ```

2. Log in to Azure
   ```
   az login
   ```

3. Install Bicep tools
   ```
   az bicep install
   ```

### Deployment Steps

1. Navigate to the deploy directory
   ```
   cd /Users/tomhofman/repositories/experiments/lunch-and-learn/deploy
   ```

2. Run the deployment script
   ```
   ./deploy.sh <environment> <resourceGroupName> <location>
   ```
   
   Default values:
   - environment: `dev`
   - resourceGroupName: `skillmgmt-rg`
   - location: `westeurope`

   Example:
   ```
   ./deploy.sh dev skillmgmt-rg eastus
   ```

   Parameters:
   - `environment`: Target environment (`dev` or `prod`)
   - `resourceGroupName`: Azure resource group name
   - `location`: Azure region

3. Follow the instructions provided by the script to deploy your application code

### Manual Deployment

You can also deploy manually:

```
az deployment group create \
  --resource-group skillmgmt-rg \
  --template-file main.bicep \
  --parameters @parameters.dev.json \
  --parameters sqlAdminPassword=<SecurePassword>
```

## Security Notes

- SQL Server admin passwords should be securely managed and not stored in code
- For production deployments, consider integrating with Azure Key Vault
- The firewall rule allows Azure services by default - restrict further for production

## Cost Optimization

- The dev environment uses Basic tier services to minimize costs
- For even lower costs, consider using Free tier for development
- The production environment uses Standard tier for better performance and SLAs

## Scaling Considerations

- The App Service Plan can be scaled up (better hardware) or out (more instances)
- For higher traffic loads, consider using an Application Gateway or Front Door
- SQL Database can be scaled up as needed
