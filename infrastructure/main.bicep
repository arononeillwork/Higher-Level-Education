param location string = resourceGroup().location
@secure()
param pgSqlPassword string

var uniqueId = uniqueString(resourceGroup().id)

module keyVault './modules/secrets/keyvault.bicep' = {
  name: 'keyVaultDeployment'
  params: {
    vaultName: 'kv-${uniqueId}'
    location: location
  }
}

module apiService 'modules/compute/appservice.bicep' = {
  name: 'apiDeployment'
  params: {
    appName: 'api-${uniqueId}'
    appServicePlanName: 'plan-api-${uniqueId}'
    location: location
    keyVaultName: keyVault.outputs.name
  }
  dependsOn: [
    keyVault
  ]
}

module keyVaultToRoleAssignment 'modules/secrets/key-vault-role-assignment.bicep' = {
  name: 'keyVaultRoleAssignmentDeployment'
  params: {
    keyVaultName: keyVault.outputs.name
    principalIds: [
      apiService.outputs.principalId
      // Add more principal IDs as needed
    ]
  }
  dependsOn: [
    keyVault
    apiService
  ]
}

module postgres 'modules/storage/postgresql.bicep' = {
  name: 'PostgresDeployment'
  params: {
    name: 'postgresql-${uniqueString(resourceGroup().id)}'
    location: location
    administratorLogin: 'adminuser'
    administratorLoginPassword: pgSqlPassword
    keyVaultName: keyVault.outputs.name
  }
}
