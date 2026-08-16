# BinaryLane.Api

[![CI](https://github.com/alexhopeoconnor/binarylane-dotnet/actions/workflows/ci.yml/badge.svg)](https://github.com/alexhopeoconnor/binarylane-dotnet/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/vpre/BinaryLane.Api.svg)](https://www.nuget.org/packages/BinaryLane.Api)

`BinaryLane.Api` is an unofficial .NET client for the BinaryLane v2 API. It
provides typed clients for account, billing, servers, images, DNS, load
balancers, VPCs, and other v2 resources.

See the [BinaryLane API reference](https://api.binarylane.com.au/reference/) for
provider-specific API behaviour.

BinaryLane describes its API as a developer preview. SDK releases track the
committed upstream contract independently of the provider's reference version.

> This project is not affiliated with, endorsed by, or supported by BinaryLane.
> For account or infrastructure support, use
> [BinaryLane Support](https://support.binarylane.com.au/support/home).

## Install

```bash
dotnet add package BinaryLane.Api
```

## Quick start

The registration below reads `BinaryLane:ApiToken` from application
configuration and otherwise uses `BINARYLANE_API_TOKEN`.

```csharp
using BinaryLane.Api.V2;
using BinaryLane.Api.V2.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBinaryLaneApi(options =>
{
    options.ApiToken = builder.Configuration["BinaryLane:ApiToken"]
        ?? Environment.GetEnvironmentVariable("BINARYLANE_API_TOKEN")
        ?? throw new InvalidOperationException("A BinaryLane API token is required.");
});

var app = builder.Build();

app.MapGet("/account", async (IBinaryLaneClient binaryLane, CancellationToken cancellationToken) =>
    await binaryLane.Account.GetAsync(cancellationToken));

app.Run();
```

Use the resource clients from `IBinaryLaneClient`:

```csharp
static async Task ListServersAsync(
    IBinaryLaneClient binaryLane,
    CancellationToken cancellationToken)
{
    await foreach (var server in binaryLane.Servers.ListAllAsync(cancellationToken: cancellationToken))
    {
        Console.WriteLine($"{server.Id}: {server.Name}");
    }
}
```

All asynchronous resource methods accept a `CancellationToken`.

## Documentation

| Guide | Use it for |
| --- | --- |
| [Getting started](https://github.com/alexhopeoconnor/binarylane-dotnet/blob/main/docs/getting-started.md) | Your first request and user-secrets setup. |
| [Configuration](https://github.com/alexhopeoconnor/binarylane-dotnet/blob/main/docs/configuration.md) | Tokens, timeouts, direct construction, and HTTP configuration. |
| [Pagination](https://github.com/alexhopeoconnor/binarylane-dotnet/blob/main/docs/pagination.md) | Working with pages or async enumeration. |
| [Actions](https://github.com/alexhopeoconnor/binarylane-dotnet/blob/main/docs/actions-and-polling.md) | Submitting and optionally waiting for server actions. |
| [Error handling](https://github.com/alexhopeoconnor/binarylane-dotnet/blob/main/docs/errors.md) | HTTP exceptions and diagnostic information. |
| [Demo](https://github.com/alexhopeoconnor/binarylane-dotnet/tree/main/examples/BinaryLane.Api.Demo) | Running the included read-only console app. |

## Compatibility

The package targets `net8.0` and `netstandard2.0`; .NET 10 applications use
the `net8.0` asset. Package versions follow [Semantic Versioning](https://semver.org/).

## Contributing and security

See [CONTRIBUTING.md](https://github.com/alexhopeoconnor/binarylane-dotnet/blob/main/CONTRIBUTING.md) for development guidance and
[SECURITY.md](https://github.com/alexhopeoconnor/binarylane-dotnet/blob/main/SECURITY.md) for private vulnerability reporting.

## License

Distributed under the [MIT License](https://github.com/alexhopeoconnor/binarylane-dotnet/blob/main/LICENSE).
