using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using BinaryLane.Api.V2.Http;
using BinaryLane.Api.V2.Models;
using BinaryLane.Api.V2.Pagination;

namespace BinaryLane.Api.V2.Resources;

/// <summary>Reads and waits for asynchronous account and server actions.</summary>
public interface IActionsApi
{
    /// <summary>Gets an action by its global action ID.</summary>
    Task<BinaryLaneAction> GetAsync(long actionId, CancellationToken cancellationToken = default);

    /// <summary>Lists global actions.</summary>
    Task<Page<BinaryLaneAction>> ListAsync(PageRequest? page = null, CancellationToken cancellationToken = default);

    /// <summary>Streams all global actions.</summary>
    IAsyncEnumerable<BinaryLaneAction> ListAllAsync(PageRequest? page = null, CancellationToken cancellationToken = default);

    /// <summary>Supplies a requested user interaction for an action.</summary>
    Task ProceedAsync(long actionId, ProceedRequest request, CancellationToken cancellationToken = default);

    /// <summary>Polls an action until BinaryLane reports a terminal state.</summary>
    Task<BinaryLaneAction> WaitForCompletionAsync(
        long actionId,
        ActionWaitOptions? options = null,
        CancellationToken cancellationToken = default);
}

/// <inheritdoc />
public sealed class ActionsApi : BinaryLaneResourceBase, IActionsApi
{
    private const string FailedStatus = "failed";
    private const string CancelledStatus = "cancelled";
    private const string CanceledStatus = "canceled";

    /// <summary>Initializes the actions resource.</summary>
    public ActionsApi(IBinaryLaneApiExecutor executor, BinaryLaneJsonSerializerOptions json)
        : base(executor, json)
    {
    }

    /// <inheritdoc />
    public Task<BinaryLaneAction> GetAsync(long actionId, CancellationToken cancellationToken = default) =>
        GetItemAsync<BinaryLaneAction>($"v2/actions/{actionId}", "action", cancellationToken);

    /// <inheritdoc />
    public Task<Page<BinaryLaneAction>> ListAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetPageAsync<BinaryLaneAction>("v2/actions", "actions", page, null, cancellationToken);

    /// <inheritdoc />
    public IAsyncEnumerable<BinaryLaneAction> ListAllAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetAllPagesAsync<BinaryLaneAction>("v2/actions", "actions", page, null, cancellationToken);

    /// <inheritdoc />
    public Task ProceedAsync(long actionId, ProceedRequest request, CancellationToken cancellationToken = default) =>
        SendNoContentAsync(HttpMethod.Post, $"v2/actions/{actionId}/proceed", request ?? throw new ArgumentNullException(nameof(request)), null, cancellationToken);

    /// <inheritdoc />
    public async Task<BinaryLaneAction> WaitForCompletionAsync(
        long actionId,
        ActionWaitOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        options ??= new ActionWaitOptions();
        options.Validate();
        var startedAt = DateTimeOffset.UtcNow;

        while (true)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var action = await GetAsync(actionId, cancellationToken).ConfigureAwait(false);
            if (IsTerminal(action.Status))
            {
                return action;
            }

            if (DateTimeOffset.UtcNow - startedAt >= options.Timeout)
            {
                throw new TimeoutException(
                    $"Timed out waiting for BinaryLane action {actionId} after {options.Timeout}.");
            }

            await Task.Delay(options.PollInterval, cancellationToken).ConfigureAwait(false);
        }
    }

    private static bool IsTerminal(string status) =>
        string.Equals(status, BinaryLaneValues.ActionStatus.Completed, StringComparison.OrdinalIgnoreCase) ||
        string.Equals(status, BinaryLaneValues.ActionStatus.Errored, StringComparison.OrdinalIgnoreCase) ||
        string.Equals(status, FailedStatus, StringComparison.OrdinalIgnoreCase) ||
        string.Equals(status, CancelledStatus, StringComparison.OrdinalIgnoreCase) ||
        string.Equals(status, CanceledStatus, StringComparison.OrdinalIgnoreCase);
}
