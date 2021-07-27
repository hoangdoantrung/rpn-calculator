# To do : some features are not implemented due to a lack of time

- Add Authentication: should use OAuth2 to protect API
- Add Authorization: some important endpoints like deleteStack or clearStack or getAllStacks should check access rights
- Complete unit tests to cover all cases.
- Add health check controller: API should have health check which verify status of all API dependencies (database or other APIs)
- Add CORS policy to allow websites to call API.
- Swagger: add more documentations on swagger. Should create more examples for each endpoint then use AddSwaggerExamplesFromAssembly. Also, add more SwaggerResponse for error cases of each endpoint.
- Add configuration for each environment (DEV, UAT, PRD). Configuration should be loaded dynamically using some provider like Consul, Azure Key Vault, etc ...
- Add Middleware to handle business exceptions and format response to client. We should not return any technical exceptions to client. We should format response with HttpCode and readable message.
- AddPagination to GetAll endpoint.
- Store Stacks in database: we use a InMemory dictionnary to store stacks and a method to generate stack id. We should store stacks in database and get unique stack id from DB.
- Add monitoring system: we log in console just to demonstrate but we should use some monitoring system like ELK stack.
