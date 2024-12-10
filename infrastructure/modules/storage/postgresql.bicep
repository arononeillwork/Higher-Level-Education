param name string
param location string
param administratorLogin string
@secure()
param administratorLoginPassword string
param keyVaultName string

resource postgresqlServer 'Microsoft.DBforPostgreSQL/flexibleServers@2023-12-01-preview' = {
  name: name
  location: location
  sku: {
    name: 'Basic_B1s'
    tier: 'Basic'
  }
  properties: {
    version: '16'
    storage: {
      storageSizeGB: 10
    }
    backup: {
      backupRetentionDays: 7
      geoRedundantBackup: 'Disabled'
    }
    administratorLogin: administratorLogin
    administratorLoginPassword: administratorLoginPassword
  }
  resource database 'databases' = {
    name: 'primaryEducation'
  }
  // Allow all Azure internal IPs to access the server
  resource firewallAzure 'firewallRules' = {
    name: 'allow-all-azure-internal-IPs'
    properties: {
      startIpAddress: '0.0.0.0' // Azure internal IP range
      endIpAddress: '0.0.0.0'
    }
  }
}

resource keyVault 'Microsoft.KeyVault/vaults@2023-07-01' existing = {
  name: keyVaultName
}

resource postgresDbConnectionString 'Microsoft.KeyVault/vaults/secrets@2023-07-01' = {
  parent: keyVault
  name: 'Postgres--ConnectionString'
  properties: {
    value: 'Server=${postgresqlServer.name}.postgres.database.azure.com;Database=primaryEducation;Port=5432;User Id=${postgresqlServer.properties.administratorLogin}@${postgresqlServer.name};Password=${administratorLoginPassword};SslMode=Require;'
  }
}

output serverId string = postgresqlServer.id
