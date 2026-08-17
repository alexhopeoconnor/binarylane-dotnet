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

/// <summary>Reads data-transfer usage data.</summary>
public interface IDataUsageApi
{
    Task<Page<DataUsage>> ListCurrentAsync(PageRequest? page = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<DataUsage> ListAllCurrentAsync(PageRequest? page = null, CancellationToken cancellationToken = default);
    Task<DataUsage> GetCurrentAsync(long serverId, CancellationToken cancellationToken = default);
}

/// <inheritdoc />
public sealed class DataUsageApi : BinaryLaneResourceBase, IDataUsageApi
{
    /// <summary>Initializes the data-usage resource.</summary>
    public DataUsageApi(IBinaryLaneApiExecutor executor, BinaryLaneJsonSerializerOptions json)
        : base(executor, json)
    {
    }

    /// <inheritdoc />
    public Task<Page<DataUsage>> ListCurrentAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetPageAsync<DataUsage>("v2/data_usages/current", "data_usages", page, null, cancellationToken);

    /// <inheritdoc />
    public IAsyncEnumerable<DataUsage> ListAllCurrentAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetAllPagesAsync<DataUsage>("v2/data_usages/current", "data_usages", page, null, cancellationToken);

    /// <inheritdoc />
    public Task<DataUsage> GetCurrentAsync(long serverId, CancellationToken cancellationToken = default) =>
        GetItemAsync<DataUsage>($"v2/data_usages/{serverId}/current", "data_usage", cancellationToken);
}
