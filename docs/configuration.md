# Configuration

## Standard registration

In an ASP.NET Core or generic-host application, `AddBinaryLaneApi` configures
`IBinaryLaneClient` and the individual resource interfaces.

```csharp
builder.Services.AddBinaryLaneApi(options =>
{
    options.ApiToken = builder.Configuration["BinaryLane:ApiToken"];
    options.RequestTimeoutSeconds = 100;
});
```

| Setting | Default | Description |
| --- | --- | --- |
| `BaseUrl` | `https://api.binarylane.com.au/` | API base address. Leave unchanged for BinaryLane's public API. |
| `ApiToken` | none | Bearer token used by the default provider. |
| `RequestTimeoutSeconds` | `100` | Timeout for an individual HTTP request. |

The base URL must be an HTTPS URL without credentials, a query string, or a
fragment. Options are validated when the application starts.

## Rotating tokens

Register `IBinaryLaneTokenProvider` before calling `AddBinaryLaneApi` when the
token comes from a vault or changes during the lifetime of the application.
`VaultTokenProvider` below is your implementation of that interface.

```csharp
using BinaryLane.Api.V2.Authentication;
using BinaryLane.Api.V2.DependencyInjection;

builder.Services.AddSingleton<IBinaryLaneTokenProvider, VaultTokenProvider>();
builder.Services.AddBinaryLaneApi(options => options.RequestTimeoutSeconds = 100);
```

The provider is called for every request. Do not log the returned token.

## Use without dependency injection

```csharp
using BinaryLane.Api.V2;
using BinaryLane.Api.V2.Authentication;

using var httpClient = new HttpClient
{
    BaseAddress = new Uri("https://api.binarylane.com.au/"),
};

var token = Environment.GetEnvironmentVariable("BINARYLANE_API_TOKEN")
    ?? throw new InvalidOperationException("Set BINARYLANE_API_TOKEN.");

var client = new BinaryLaneClient(
    httpClient,
    new StaticBinaryLaneTokenProvider(token));
```

## HTTP configuration

`AddBinaryLaneApi` returns an `IHttpClientBuilder`, so you can configure proxy,
telemetry, or other application-specific handlers.

```csharp
builder.Services.AddBinaryLaneApi(options => options.ApiToken = token)
    .ConfigureHttpClient(client => client.DefaultRequestHeaders.Add("X-App", "my-service"));
```

Only add automatic retries when the request is safe to repeat. In particular,
POST, PUT, PATCH, and DELETE requests may have already been applied when a
network failure is reported.
