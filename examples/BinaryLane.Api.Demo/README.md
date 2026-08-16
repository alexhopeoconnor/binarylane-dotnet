# BinaryLane.Api demo

This console app demonstrates read-only account, server, and region requests.
It does not create, change, or delete infrastructure.

## Configure a token

From the repository root, store a token with .NET user secrets:

```bash
dotnet user-secrets set \
  "BinaryLane:ApiToken" "your-token" \
  --project examples/BinaryLane.Api.Demo
```

Alternatively, set `BINARYLANE_API_TOKEN` for one process. Do not add a token
to `appsettings.json`.

## Run it

```bash
dotnet run --project examples/BinaryLane.Api.Demo -- account
dotnet run --project examples/BinaryLane.Api.Demo -- servers
dotnet run --project examples/BinaryLane.Api.Demo -- server 12345
dotnet run --project examples/BinaryLane.Api.Demo -- regions
```

Press Ctrl+C to cancel a request or listing.
