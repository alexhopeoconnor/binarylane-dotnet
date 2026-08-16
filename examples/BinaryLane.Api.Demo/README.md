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

Alternatively, start the process with `BINARYLANE_API_TOKEN` set.

## Run it

```bash
dotnet run --project examples/BinaryLane.Api.Demo -- account
dotnet run --project examples/BinaryLane.Api.Demo -- servers
dotnet run --project examples/BinaryLane.Api.Demo -- server 12345
dotnet run --project examples/BinaryLane.Api.Demo -- regions
```

Press Ctrl+C to cancel a request or listing.
