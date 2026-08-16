# Pagination

List endpoints accept one-based `page` and `per_page` values. `per_page` can be
between 1 and 200.

Use `ListAsync` when you need a specific page or the provider's page links:

```csharp
using BinaryLane.Api.V2.Pagination;

var page = await client.Servers.ListAsync(
    new PageRequest { Page = 1, PerPage = 50 },
    cancellationToken);

foreach (var server in page.Items)
{
    await ProcessAsync(server, cancellationToken);
}
```

Use `ListAllAsync` to process every page as it is needed:

```csharp
await foreach (var server in client.Servers.ListAllAsync(cancellationToken: cancellationToken))
{
    await ProcessAsync(server, cancellationToken);
}
```

The cancellation token is carried across page requests made by `ListAllAsync`.
