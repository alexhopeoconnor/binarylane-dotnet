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

/// <summary>Manages IPv6 reverse-name configuration.</summary>
public interface IReverseNamesApi
{
    Task<Page<string>> ListIpv6Async(PageRequest? page = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<string> ListAllIpv6Async(PageRequest? page = null, CancellationToken cancellationToken = default);
    Task<ActionSubmission> UpdateIpv6Async(ReverseNameserversRequest request, CancellationToken cancellationToken = default);
}

/// <inheritdoc />
public sealed class ReverseNamesApi : BinaryLaneResourceBase, IReverseNamesApi
{
    /// <summary>Initializes the reverse-names resource.</summary>
    public ReverseNamesApi(IBinaryLaneApiExecutor executor, BinaryLaneJsonSerializerOptions json)
        : base(executor, json)
    {
    }

    /// <inheritdoc />
    public Task<Page<string>> ListIpv6Async(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetPageAsync<string>("v2/reverse_names/ipv6", "reverse_nameservers", page, null, cancellationToken);

    /// <inheritdoc />
    public IAsyncEnumerable<string> ListAllIpv6Async(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetAllPagesAsync<string>("v2/reverse_names/ipv6", "reverse_nameservers", page, null, cancellationToken);

    /// <inheritdoc />
    public async Task<ActionSubmission> UpdateIpv6Async(ReverseNameserversRequest request, CancellationToken cancellationToken = default)
    {
        var response = await SendResponseAsync(
            HttpMethod.Post,
            "v2/reverse_names/ipv6",
            request ?? throw new ArgumentNullException(nameof(request)),
            null,
            cancellationToken).ConfigureAwait(false);
        TryDeserializeEnvelope<BinaryLaneAction>(response.Body, "action", out var action);
        return new ActionSubmission(response.StatusCode, action, response.Body);
    }
}
