# Server actions

Use a typed `ServerAction` to submit a server action. For example, this powers
on a server:

```csharp
using BinaryLane.Api.V2.Models;

var submission = await client.Servers.SubmitActionAsync(
    serverId,
    new PowerOnServerAction(),
    cancellationToken);
```

Common action types include `PowerOnServerAction`, `PowerOffServerAction`,
`RebootServerAction`, `ResizeServerAction`, and `RebuildServerAction`.

## Waiting for completion

BinaryLane can accept an action before it has finished. When the response
contains an action, use its identifier to wait for the final status:

```csharp
if (submission.Action is { } action)
{
    var completed = await client.Actions.WaitForCompletionAsync(
        action.Id,
        cancellationToken: cancellationToken);
}
```

Pass `ActionWaitOptions` when you need a different timeout or polling interval.
The default is a 15-minute timeout with a two-second polling interval.

## Status values

`BinaryLaneAction.Status` and `BinaryLaneAction.Type` are strings. Compare a
documented value with `BinaryLaneValues`:

```csharp
if (action.Status == BinaryLaneValues.ActionStatus.Completed)
{
    // Continue with the next step.
}
```
