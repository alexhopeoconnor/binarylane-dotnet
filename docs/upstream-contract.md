# Maintainer guide: upstream API contract

This page describes SDK maintenance. Package consumers can start with the
[README](../README.md).

The source of truth for BinaryLane's API is its published OpenAPI document:

<https://api.binarylane.com.au/reference/openapi.yaml>

The committed raw snapshot is intentionally reviewable:

```text
eng/openapi/binarylane-v2.openapi.yaml
eng/openapi/contract.json
```

`contract.json` records the source URL, provider-declared version, SHA-256,
and retrieval time. It describes an upstream artifact; it does not set the
NuGet package version.

## Snapshot purpose

BinaryLane's preview API can change without a version change. The committed
snapshot makes those changes visible during SDK maintenance.

## Normalization

The raw document includes virtual `#ActionName` paths for individual server
action variants. They exist to improve the provider's reference UI but are not
HTTP paths. `eng/normalize-openapi.sh` strips them when producing a normalized
document for code generation or coverage checks. Do not issue requests to
those virtual paths.

## Monitoring and updating

The scheduled `contract-monitor.yml` workflow downloads the live document and
runs `eng/verify-openapi-contract.sh`. If the SHA-256 or provider-declared
version differs, it opens one issue rather than silently updating the SDK.

To intentionally refresh the snapshot after review:

```bash
./eng/refresh-openapi-contract.sh
git diff -- eng/openapi docs/api-coverage.md
```

Then update affected models, coverage documentation, changelog, and tests in
the same pull request. Review the change before merging it.
