#!/bin/zsh

# Azure deployment script for Skill Management Application
# Usage: ./deploy.sh <environment> <resourceGroupName> <location>

# Default values
DEFAULT_ENV="dev"
DEFAULT_RG_NAME="skillmgmt-rg"
DEFAULT_LOCATION="westeurope"

# Get parameters or use defaults
ENVIRONMENT=${1:-$DEFAULT_ENV}
RESOURCE_GROUP=${2:-$DEFAULT_RG_NAME}
LOCATION=${3:-$DEFAULT_LOCATION}

echo "Deploying Skill Management System to Azure..."
echo "Environment: $ENVIRONMENT"
echo "Resource Group: $RESOURCE_GROUP"
echo "Location: $LOCATION"

# Check if resource group exists, create if it doesn't
if ! az group show --name $RESOURCE_GROUP &> /dev/null; then
    echo "Creating resource group $RESOURCE_GROUP..."
    az group create --name $RESOURCE_GROUP --location $LOCATION
else
    echo "Using existing resource group $RESOURCE_GROUP"
fi

# Prompt for SQL Admin Password (or use Azure KeyVault in production)
echo "Enter SQL Admin Password (will not be displayed):"
read -s SQL_ADMIN_PASSWORD

# Deploy the Bicep template
echo "Deploying infrastructure using Bicep..."
az deployment group create \
    --resource-group $RESOURCE_GROUP \
    --template-file main.bicep \
    --parameters @parameters.$ENVIRONMENT.json \
    --parameters sqlAdminPassword=$SQL_ADMIN_PASSWORD

# Get deployed resources for output
API_URL=$(az deployment group show --resource-group $RESOURCE_GROUP --name main --query properties.outputs.apiUrl.value -o tsv)
WEB_URL=$(az deployment group show --resource-group $RESOURCE_GROUP --name main --query properties.outputs.webUrl.value -o tsv)

echo "Deployment completed!"
echo "API URL: $API_URL"
echo "Web URL: $WEB_URL"

echo "Next steps:"
echo "1. Build and publish your backend API:"
echo "   cd ../backend/API && dotnet publish -c Release"
echo ""
echo "2. Deploy backend API:"
echo "   az webapp deployment source config-zip --resource-group $RESOURCE_GROUP --name $(echo $API_URL | cut -d'/' -f3 | cut -d'.' -f1) --src ./bin/Release/net9.0/publish.zip"
echo ""
echo "3. Build the frontend:"
echo "   cd ../frontend && npm run build"
echo ""
echo "4. Deploy frontend:"
echo "   cd ../frontend && zip -r build.zip ./build && az webapp deployment source config-zip --resource-group $RESOURCE_GROUP --name $(echo $WEB_URL | cut -d'/' -f3 | cut -d'.' -f1) --src ./build.zip"
