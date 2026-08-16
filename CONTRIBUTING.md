# Contributing

Thanks for improving the SDK. Keep changes focused, tested, documented, and
backwards compatible unless a breaking change is explicitly planned.

## Local setup

Install the SDK pinned in `global.json`, then run:

```bash
dotnet restore src/BinaryLane.Api/BinaryLane.Api.csproj --locked-mode --disable-parallel
dotnet restore tests/BinaryLane.Api.Tests/BinaryLane.Api.Tests.csproj --locked-mode --disable-parallel
dotnet restore examples/BinaryLane.Api.Demo/BinaryLane.Api.Demo.csproj --locked-mode --disable-parallel
dotnet build src/BinaryLane.Api/BinaryLane.Api.csproj -c Release --no-restore -m:1 -nodeReuse:false -p:BuildInParallel=false
dotnet build tests/BinaryLane.Api.Tests/BinaryLane.Api.Tests.csproj -c Release --no-restore -m:1 -nodeReuse:false -p:BuildInParallel=false
dotnet build examples/BinaryLane.Api.Demo/BinaryLane.Api.Demo.csproj -c Release --no-restore -m:1 -nodeReuse:false -p:BuildInParallel=false
dotnet test tests/BinaryLane.Api.Tests/BinaryLane.Api.Tests.csproj -c Release --no-build -m:1 -nodeReuse:false
dotnet format src/BinaryLane.Api/BinaryLane.Api.csproj --verify-no-changes --no-restore
dotnet format tests/BinaryLane.Api.Tests/BinaryLane.Api.Tests.csproj --verify-no-changes --no-restore
dotnet format examples/BinaryLane.Api.Demo/BinaryLane.Api.Demo.csproj --verify-no-changes --no-restore
```

Run these commands one at a time. The serial options keep local build resource
use predictable on a shared checkout.

The tracked demo application lives in `examples/BinaryLane.Api.Demo`. It reads
`BinaryLane:ApiToken` from its configuration providers, including user secrets.

```bash
dotnet user-secrets set \
  "BinaryLane:ApiToken" "your-token" \
  --project examples/BinaryLane.Api.Demo
```

## Pull requests

- Keep each pull request narrowly scoped and explain the user-visible effect.
- Add or update unit tests for every changed HTTP request, response, error, or
  public API contract.
- Use scrubbed fixtures only. Never record a real bearer token, password,
  user-data payload, IP address, server name, or account email unless it is
  demonstrably non-sensitive and intended for publication.
- Update XML documentation, the README, and the appropriate page in `docs/`
  when a public behavior changes.
- Update `docs/api-coverage.md` when support for a documented BinaryLane route
  changes.
- Add a changelog entry under **Unreleased** for a user-visible change.

## API design rules

- Keep public APIs in `BinaryLane.Api.V2.*`.
- Prefer focused resource interfaces over a monolithic service interface.
- Accept a `CancellationToken` on every asynchronous operation.
- Keep provider fields forward compatible: do not turn preview response values
  into closed C# enums unless the API contract guarantees them.
- Do not add automatic retries for state-changing requests. BinaryLane does not
  document idempotency keys, so retries can provision or mutate resources twice.
- Avoid breaking changes in a minor or patch SDK version. Follow
  [Semantic Versioning](https://semver.org/).

## Upstream OpenAPI changes

The provider's OpenAPI document is a preview contract and can change without a
version increment. See [the contract workflow](docs/upstream-contract.md)
before updating `eng/openapi/`. Review any change by hand; the raw contract
contains virtual `#ActionName` documentation paths that must not become real
HTTP endpoints in the SDK.

## Releases

Only maintainers may release packages. See [the release guide](docs/releasing.md).
The release workflow uses NuGet trusted publishing through GitHub OIDC; no
long-lived NuGet API key belongs in this repository or its GitHub secrets.
