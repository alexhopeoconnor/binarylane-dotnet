# BinaryLane API contract

`binarylane-v2.openapi.yaml` is an unmodified snapshot of BinaryLane's published
OpenAPI document. Its provenance and digest are recorded in `contract.json`.

The provider currently describes the API as a developer preview, so an upstream
version number alone must not be used as a compatibility guarantee. The contract
monitor compares the document digest as well as its declared version.

## Normalization rule

The document has 52 routable paths and 42 additional paths such as
`/v2/servers/{server_id}/actions#PowerOn`. The latter are documentation aliases
for payload variants of `POST /v2/servers/{server_id}/actions`; URI fragments do
not reach an HTTP server. They must be removed before an OpenAPI generator is
asked to create a route surface. `normalization.json` records the exact rule and
each affected path.

Public SDK DTOs deliberately model all provider enum values as strings. This is
forward-compatible with a preview API that may add a value before a new SDK
release is available.
