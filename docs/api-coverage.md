# Maintainer guide: API coverage

This is a maintainer reference. Package consumers can start with the
[README](../README.md).

The SDK's scope is the BinaryLane v2 OpenAPI contract committed at
`eng/openapi/binarylane-v2.openapi.yaml`. The raw contract currently declares
version `0.39.1`; it is a developer-preview contract and may change without a
version change.

The raw document contains virtual paths such as
`/v2/servers/{server_id}/actions#PowerOn`. These are API-reference aliases for
action payload variants, not real HTTP routes. Coverage and generated-code
checks must use the normalized contract and count only the real
`POST /v2/servers/{server_id}/actions` route.

| API area | Public resource boundary |
| --- | --- |
| Account | `IAccountApi` |
| Balances, invoices, and unpaid invoices | `IBillingApi` |
| Actions | `IActionsApi` |
| Servers and server subresources | `IServersApi` |
| Images | `IImagesApi` |
| SSH keys | `ISshKeysApi` |
| DNS domains, nameservers, and records | `IDomainsApi` |
| Load balancers and forwarding rules | `ILoadBalancersApi` |
| VPCs and members | `IVpcsApi` |
| Regions, sizes, and software catalogues | `IRegionsApi`, `ISizesApi`, `ISoftwareApi` |
| Reverse names | `IReverseNamesApi` |
| Data usage | `IDataUsageApi` |
| Sample sets | `ISampleSetsApi` |

Keep this table and the related tests up to date when API support changes.
