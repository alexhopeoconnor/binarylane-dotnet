# Getting started

`BinaryLane.Api` connects to BinaryLane's bearer-token v2 API at
`https://api.binarylane.com.au/v2/`.

## 1. Install the package

```bash
dotnet add package BinaryLane.Api --prerelease
```

## 2. Store the token safely

For local development, initialise user secrets for your application and add
the token:

```bash
dotnet user-secrets init
dotnet user-secrets set "BinaryLane:ApiToken" "your-token"
```

For hosted applications, use the platform's secret store or supply
`BINARYLANE_API_TOKEN` at runtime.

## 3. Register the client

```csharp
using BinaryLane.Api.V2;
using BinaryLane.Api.V2.DependencyInjection;
using BinaryLane.Api.V2.Models;

builder.Services.AddBinaryLaneApi(options =>
{
    options.ApiToken = builder.Configuration["BinaryLane:ApiToken"]
        ?? Environment.GetEnvironmentVariable("BINARYLANE_API_TOKEN")
        ?? throw new InvalidOperationException("A BinaryLane API token is required.");
});
```

## 4. Make a request

Inject `IBinaryLaneClient`, then select the resource you need:

```csharp
public sealed class ServerReader(IBinaryLaneClient binaryLane)
{
    public IAsyncEnumerable<Server> ListAsync(CancellationToken cancellationToken) =>
        binaryLane.Servers.ListAllAsync(cancellationToken: cancellationToken);
}
```

See [Configuration](configuration.md) for custom token providers and
[Pagination](pagination.md) for page-by-page access.
