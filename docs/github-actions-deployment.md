# GitHub Actions CI/CD for Azure Deployment

This document explains how to set up the GitHub Actions workflow to automatically build and deploy the Skill Management application to Azure.

## Workflow Overview

The GitHub Actions workflow (`azure-deploy.yml`) includes the following stages:

1. **Testing**: Runs unit tests for both the backend API and frontend
2. **Infrastructure Deployment**: Uses Bicep to deploy or update Azure resources
3. **Backend API Deployment**: Builds and publishes the .NET API to Azure App Service
4. **Frontend Deployment**: Builds and deploys the React frontend to Azure App Service
5. **Notification**: Posts a comment with deployment details

## Required Secrets

Before your workflow can run successfully, you need to set up the following secrets in your GitHub repository:

1. **`AZURE_CREDENTIALS`**: Service Principal credentials for Azure authentication
2. **`AZURE_RG`**: The name of your Azure Resource Group
3. **`SQL_ADMIN_PASSWORD`**: Password for SQL Server administrator account

## Setting Up the Required Secrets

### 1. Creating an Azure Service Principal

Run these commands in Azure CLI to create a service principal:

```bash
# Login to Azure
az login

# Set subscription (replace with your subscription name or ID)
az account set --subscription "Your Subscription Name"

# Create a service principal with Contributor role
az ad sp create-for-rbac --name "GitHubActionsSP" --role contributor \
  --scopes /subscriptions/{subscription-id}/resourceGroups/{resource-group} \
  --sdk-auth
```

The output JSON contains your credentials. Copy this entire JSON object.

### 2. Creating GitHub Secrets

1. Navigate to your GitHub repository
2. Go to **Settings > Secrets and variables > Actions**
3. Click **New repository secret**
4. Add the following secrets:
   - **Name**: `AZURE_CREDENTIALS`  
     **Value**: *Paste the entire JSON output from the service principal creation*

   - **Name**: `AZURE_RG`  
     **Value**: *Your Azure resource group name* (example: `skillmgmt-rg`)

   - **Name**: `SQL_ADMIN_PASSWORD`  
     **Value**: *Secure password for your SQL database*

## Manual Deployments

You can also trigger manual deployments by:

1. Navigate to **Actions** in your GitHub repository
2. Select the **Azure Deployment** workflow
3. Click **Run workflow**
4. Choose the environment (`dev` or `prod`)
5. Click **Run workflow**

## Environment URLs

After successful deployment, your application will be available at:

- **Frontend**: `https://skillmgmt-{environment}-web.azurewebsites.net`
- **API**: `https://skillmgmt-{environment}-api.azurewebsites.net`
- **Swagger Documentation**: `https://skillmgmt-{environment}-api.azurewebsites.net/swagger`

Where `{environment}` is either `dev` or `prod`.

---

## Workflow Structure Update (2024-)

The deployment process is now split into two workflows:

- **Application Deployment:** `.github/workflows/azure-deploy.yml` — builds, tests, and deploys backend and frontend only.
- **Infrastructure Deployment:** `.github/workflows/infra-deploy.yml` — deploys Azure infrastructure using Bicep.

See `docs/github-actions-workflow-structure.md` for details and rationale.

Infrastructure and application deployments are now managed independently for clarity and maintainability.
