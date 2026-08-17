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

/// <summary>Reads monitoring samples for servers.</summary>
public interface ISampleSetsApi
{
    Task<SampleSet?> GetLatestAsync(long serverId, string? dataInterval = null, CancellationToken cancellationToken = default);
    Task<Page<SampleSet>> ListAsync(long serverId, PageRequest? page = null, string? dataInterval = null, DateTimeOffset? start = null, DateTimeOffset? endAt = null, CancellationToken cancellationToken = default);
}

/// <inheritdoc />
public sealed class SampleSetsApi : BinaryLaneResourceBase, ISampleSetsApi
{
    /// <summary>Initializes the monitoring sample-set resource.</summary>
    public SampleSetsApi(IBinaryLaneApiExecutor executor, BinaryLaneJsonSerializerOptions json)
        : base(executor, json)
    {
    }

    /// <inheritdoc />
    public async Task<SampleSet?> GetLatestAsync(long serverId, string? dataInterval = null, CancellationToken cancellationToken = default)
    {
        IReadOnlyDictionary<string, object?>? query = string.IsNullOrWhiteSpace(dataInterval)
            ? null
            : new Dictionary<string, object?> { ["data_interval"] = dataInterval };
        var response = await GetRawAsync($"v2/samplesets/{serverId}/latest", query, cancellationToken).ConfigureAwait(false);
        return TryDeserializeEnvelope<SampleSet>(response, "sample_set", out var sampleSet) ? sampleSet : null;
    }

    /// <inheritdoc />
    public Task<Page<SampleSet>> ListAsync(
        long serverId,
        PageRequest? page = null,
        string? dataInterval = null,
        DateTimeOffset? start = null,
        DateTimeOffset? endAt = null,
        CancellationToken cancellationToken = default)
    {
        var query = new Dictionary<string, object?>
        {
            ["data_interval"] = dataInterval,
            ["start"] = start,
            ["end"] = endAt,
        };
        return GetPageAsync<SampleSet>($"v2/samplesets/{serverId}", "sample_sets", page, query, cancellationToken);
    }
}
