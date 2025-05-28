@description('The name of the project, used as a prefix for all resources.')
param projectName string = 'skillmgmt'

@description('The environment (dev, test, prod)')
@allowed([
  'dev'
  'test'
  'prod'
])
param environment string = 'dev'

@description('The location for all resources')
param location string = 'westeurope'

@description('The administrator login username for the SQL Server')
param sqlAdminLogin string

@description('The administrator login password for the SQL Server')
@secure()
param sqlAdminPassword string

@description('The SKU name for App Service Plan')
param appServicePlanSku object = {
  name: 'B1'
  tier: 'Basic'
  capacity: 1
}

@description('The SKU name for SQL Database')
param sqlDatabaseSku object = {
  name: 'Basic'
  tier: 'Basic'
}

// Variables for resource naming
var resourceNamePrefix = '${projectName}-${environment}'
var sqlServerName = '${resourceNamePrefix}-sqlserver'
var sqlDatabaseName = '${resourceNamePrefix}-db'
var appServicePlanName = '${resourceNamePrefix}-plan'
var apiAppName = '${resourceNamePrefix}-api'
var webAppName = '${resourceNamePrefix}-web'

// SQL Server and Database
resource sqlServer 'Microsoft.Sql/servers@2021-11-01' = {
  name: sqlServerName
  location: location
  properties: {
    administratorLogin: sqlAdminLogin
    administratorLoginPassword: sqlAdminPassword
    version: '12.0'
  }
}

resource sqlServerFirewallRule 'Microsoft.Sql/servers/firewallRules@2021-11-01' = {
  parent: sqlServer
  name: 'AllowAllAzureIPs'
  properties: {
    startIpAddress: '0.0.0.0'
    endIpAddress: '0.0.0.0'
  }
}

resource sqlDatabase 'Microsoft.Sql/servers/databases@2021-11-01' = {
  parent: sqlServer
  name: sqlDatabaseName
  location: location
  sku: {
    name: sqlDatabaseSku.name
    tier: sqlDatabaseSku.tier
  }
  properties: {
    collation: 'SQL_Latin1_General_CP1_CI_AS'
  }
}

// App Service Plan for hosting both the API and Web frontend
resource appServicePlan 'Microsoft.Web/serverfarms@2022-03-01' = {
  name: appServicePlanName
  location: location
  sku: {
    name: appServicePlanSku.name
    tier: appServicePlanSku.tier
    capacity: appServicePlanSku.capacity
  }
  kind: 'linux'
  properties: {
    reserved: true
  }
}

// Backend API App Service
resource apiApp 'Microsoft.Web/sites@2022-03-01' = {
  name: apiAppName
  location: location
  kind: 'app,linux'
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true
    siteConfig: {
      linuxFxVersion: 'DOTNETCORE|8.0'
      alwaysOn: true
      ftpsState: 'Disabled'
      cors: {
        allowedOrigins: [
          'https://${webAppName}.azurewebsites.net'
        ]
      }
      appSettings: [
        {
          name: 'ASPNETCORE_ENVIRONMENT'
          value: environment == 'prod' ? 'Production' : 'Development'
        }
        {
          name: 'API_URLS'
          value: 'http://+:80'
        }
        {
          name: 'AllowedOrigins__0'
          value: 'https://${webAppName}.azurewebsites.net'
        }
      ]
      connectionStrings: [
        {
          name: 'DefaultConnection'
          connectionString: 'Server=tcp:${sqlServer.name}.database.windows.net,1433;Initial Catalog=${sqlDatabase.name};Persist Security Info=False;User ID=${sqlAdminLogin};Password=${sqlAdminPassword};MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;'
          type: 'SQLAzure'
        }
      ]
    }
  }
}

// Frontend Web App Service
resource webApp 'Microsoft.Web/sites@2022-03-01' = {
  name: webAppName
  location: location
  kind: 'app,linux'
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true
    siteConfig: {
      linuxFxVersion: 'NODE|18-lts'
      alwaysOn: true
      ftpsState: 'Disabled'
      appSettings: [
        {
          name: 'REACT_APP_API_URL'
          value: 'https://${apiAppName}.azurewebsites.net/api'
        }
        {
          name: 'SCM_DO_BUILD_DURING_DEPLOYMENT'
          value: 'true'
        }
        {
          name: 'WEBSITE_NODE_DEFAULT_VERSION'
          value: '~18'
        }
      ]
    }
  }
}

// Application Insights for monitoring
resource apiAppInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: '${apiAppName}-insights'
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
    publicNetworkAccessForIngestion: 'Enabled'
    publicNetworkAccessForQuery: 'Enabled'
  }
}

resource webAppInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: '${webAppName}-insights'
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
    publicNetworkAccessForIngestion: 'Enabled'
    publicNetworkAccessForQuery: 'Enabled'
  }
}

// Configure Application Insights instrumentation keys
resource apiAppSettingAppInsights 'Microsoft.Web/sites/config@2022-03-01' = {
  parent: apiApp
  name: 'appsettings'
  properties: {
    APPINSIGHTS_INSTRUMENTATIONKEY: apiAppInsights.properties.InstrumentationKey
    ApplicationInsightsAgent_EXTENSION_VERSION: '~2'
  }
}

resource webAppSettingAppInsights 'Microsoft.Web/sites/config@2022-03-01' = {
  parent: webApp
  name: 'appsettings'
  properties: {
    APPINSIGHTS_INSTRUMENTATIONKEY: webAppInsights.properties.InstrumentationKey
    ApplicationInsightsAgent_EXTENSION_VERSION: '~2'
  }
}

// Outputs for reference
output sqlServerFqdn string = sqlServer.properties.fullyQualifiedDomainName
output apiUrl string = 'https://${apiApp.properties.defaultHostName}'
output webUrl string = 'https://${webApp.properties.defaultHostName}'
