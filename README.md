# Higher-Level-Education

## Infrastructure as Code

### Download Azure CLI

https://learn.microsoft.com/en-us/cli/azure/

### Log in into Azure - Download CLI if az not found

az login

### Create Resource Group

```bash
az group create --name higherLevelEducation-dev --location westeurope
```

### Deploy Bicep

### What if

```bash
az deployment group what-if --resource-group dometrain-urlshortener-dev --template-file infrastructure/main.bicep
```

### Deploy

````bash
az deployment group create --resource-group donetrain-urlshortener-dev --template-file infrastructure/main.bicep


### Create User for GH Actions
```bash
az ad sp create-for-rbac --name "GitHub-Actions-SP" \
 --role contributor \
 --scopes /subscriptions/178387ed-94be-47b5-8b72-61b2815286bf \
 --sdk-auth
````

#### Configure a federated identity credential on an app - App registration -> Crets & secrets -> fed credentials
