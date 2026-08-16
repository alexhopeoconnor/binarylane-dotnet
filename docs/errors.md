# Error handling

`BinaryLaneApiException` represents a non-success response from the BinaryLane
API. Network, timeout, cancellation, and client-configuration failures use
their standard .NET exception types.

| Exception | When it is used |
| --- | --- |
| `BinaryLaneUnauthorizedException` | BinaryLane returned HTTP 401. |
| `BinaryLaneForbiddenException` | BinaryLane returned HTTP 403. |
| `BinaryLaneNotFoundException` | BinaryLane returned HTTP 404. |
| `BinaryLaneValidationException` | BinaryLane returned HTTP 400 or 422. |
| `BinaryLaneApiException` | Any other non-success HTTP response. |

```csharp
using BinaryLane.Api.V2.Errors;

try
{
    var server = await client.Servers.GetAsync(serverId, cancellationToken);
}
catch (BinaryLaneNotFoundException)
{
    // Handle a missing server.
}
catch (BinaryLaneValidationException)
{
    // Show an appropriate validation message to the caller.
}
catch (BinaryLaneApiException exception)
{
    // Use exception.StatusCode to choose an application-specific response.
}
```

Each exception provides `StatusCode`, `RequestUri`, `Headers`, and, when
available, `Problem` and `ResponseBody`. `ResponseBody` contains at most the
first 32,768 characters of diagnostic text, followed by an ellipsis when
truncated.

Successful response bodies are limited to 16 MiB. A larger response throws
`HttpRequestException` before the client buffers it in memory.
