# Higher-Level-Education

## Infrastructure as Code

### Download Azure CLI

https://learn.microsoft.com/en-us/cli/azure/

### Log in into Azure - Download CLI if az not found

az login

### Create Resource Group

az group create --name higherLevelEducation-dev --location westeurope

### Deploy Bicep

### What if

az deployment group what-if --resource-group dometrain-urlshortener-dev --template-file infrastructure/main.bicep

### Create User for GH Actions

az ad sp create-for-rbac --name "GitHub-Actions-SP" \
 --role contributor \
 --scopes /subscriptions/178387ed-94be-47b5-8b72-61b2815286bf \
 --sdk-auth
