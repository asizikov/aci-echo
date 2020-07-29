Build and run docker container: 

`docker build -t local . && docker run -p 80:80 --name echo-api local  `

Verify that it's up and running:

```bash
curl -GET http://localhost/echo\?message\=Hi
{"message":"Hi"}
```

Make sure you have Azure CLI installed: 

```bash
az --version
```

Create a resource group

```bash
az group create \
    --name rg-aci-echo \
    --location westeurope
```
then create ACI resource
```bash
az container create --resource-group rg-aci-echo \
    --location westeurope \
    --name aci-echo-api \
    --image asizikov/echo-api:latest \
    --cpu 1 --memory 0.5 \
    --dns-name-label echo-api \
    --ip-address Public
```

verify that it's up and running: 

```bash
curl -GET $(az container show -g rg-aci-echo -n aci-echo-api --query "ipAddress.fqdn" -o tsv)/echo\?message\=Hi
```