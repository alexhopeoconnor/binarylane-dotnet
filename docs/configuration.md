# Configuration

## Standard registration

In an ASP.NET Core or generic-host application, `AddBinaryLaneApi` configures
`IBinaryLaneClient`, individual resource interfaces, and the underlying typed
`HttpClient`.

```csharp
builder.Services.AddBinaryLaneApi(options =>
{
    options.ApiToken = builder.Configuration["BinaryLane:ApiToken"];
});
```

| Setting | Default | Description |
| --- | --- | --- |
| `BaseUrl` | `https://api.binarylane.com.au/` | Root URI used for API requests. |
| `ApiToken` | none | Bearer token used by the default provider. |
| `RequestTimeoutSeconds` | `100` | Timeout for an individual HTTP request. |

`BaseUrl` must be an HTTPS URL without credentials, a query string, or a
fragment. `BaseUrl` and `RequestTimeoutSeconds` are validated when the host
starts; request timeouts can range from 1 to 300 seconds.

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

The provider is called for every outgoing request.

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
builder.Services.AddBinaryLaneApi(options =>
    options.ApiToken = builder.Configuration["BinaryLane:ApiToken"])
    .ConfigureHttpClient(client => client.DefaultRequestHeaders.Add("X-App", "my-service"));
```

The client does not apply retries itself. Applications can add their own
handlers through the returned `IHttpClientBuilder`.
