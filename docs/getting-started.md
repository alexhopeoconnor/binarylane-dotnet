# Getting started

`BinaryLane.Api` connects to BinaryLane's bearer-token v2 API at
`https://api.binarylane.com.au/v2/`.

## 1. Install the package

```bash
dotnet add package BinaryLane.Api
```

## 2. Configure the API token

For local development, .NET user secrets can provide the
`BinaryLane:ApiToken` configuration value:

```bash
dotnet user-secrets init
dotnet user-secrets set "BinaryLane:ApiToken" "your-token"
```

The registration below reads that value first, then falls back to
`BINARYLANE_API_TOKEN`.

## 3. Register the client

```csharp
using BinaryLane.Api.V2;
using BinaryLane.Api.V2.DependencyInjection;

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
using BinaryLane.Api.V2;
using BinaryLane.Api.V2.Models;

public sealed class ServerReader(IBinaryLaneClient binaryLane)
{
    public IAsyncEnumerable<Server> ListAsync(CancellationToken cancellationToken) =>
        binaryLane.Servers.ListAllAsync(cancellationToken: cancellationToken);
}
```

See [Configuration](configuration.md) for custom token providers and
[Pagination](pagination.md) for page-by-page access.
