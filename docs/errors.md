# Error handling

All API failures derive from `BinaryLaneApiException`.

| Exception | When it is used |
| --- | --- |
| `BinaryLaneUnauthorizedException` | The token is missing, invalid, or expired. |
| `BinaryLaneForbiddenException` | The token does not have access to the resource. |
| `BinaryLaneNotFoundException` | The requested resource does not exist or is not visible to the token. |
| `BinaryLaneValidationException` | BinaryLane rejected the request payload. |
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
available, `Problem` and `ResponseBody`. Provider detail text can contain user
data. Do not write `Problem`, `Headers`, or `ResponseBody` to application logs
without appropriate redaction.

Successful JSON responses are limited to 16 MiB. A larger response throws
`HttpRequestException` before the client buffers it in memory.
