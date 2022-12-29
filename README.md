# fluentpos microservices

Stack in Mind (Early Stages):
- .NET 6 Microservices
- Golang Microservice (For a simple microservice)
- AWS Cloud Deployment (ECS)
- RMQ for Queues / Messaging
- Docker Image for each Microservice
- CI/CD on Github Actions
- Terraform Support
- Kubernetes
- Postgre & MongoDB Datastore
- Redis Cache
- ELK Stack
- Push Docker images to public Docker Hub Repos
- Postman Collection for Testing
- VS Code as Primary IDE


## Docker
 Generate Development certificate by running the following:

 ```
dotnet dev-certs https --trust
dotnet dev-certs https -ep $env:USERPROFILE\.aspnet\https\dev_cert.pfx -p password!
dotnet dev-certs https --trust
 ```

 Once the certificates are installed, navigate to `deployments/docker-compose` and run `docker compose up`.
 
## References

### Adding Migrations

Always runs this from the Host Project where EF Design is referenced.
`dotnet ef migrations add <commit-message> --project ../Application/ -o Data/Migrations `