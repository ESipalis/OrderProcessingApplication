Requires .NET 6 to run unit tests and the `WebApi` project.

### Tests:

Navigate to `Domain.Tests.Unit` directory and execute `dotnet test` in a CLI of your choice.

### WebApi:

- Navigate to `WebApi` directory and execute `dotnet run` in a CLI of your choice.
- Navigate to https://localhost:7280/swagger in your browser.
- Send a request to process order endpoint with some data. For example:
```json
{
  "id": 0,
  "confirmationDate": "2021-11-16T04:31:40.877Z",
  "agentName": "testAgentName",
  "physicalProductOrderData": {
    "sku": "123",
    "quantity": 1
  }
}
```
- Observe console output to see that the expected services were executed.