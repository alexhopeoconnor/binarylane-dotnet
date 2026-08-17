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

/// <summary>Lists available licensed software and operating-system software.</summary>
public interface ISoftwareApi
{
    Task<Page<Software>> ListAsync(PageRequest? page = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<Software> ListAllAsync(PageRequest? page = null, CancellationToken cancellationToken = default);
    Task<Software> GetAsync(long softwareId, CancellationToken cancellationToken = default);
    Task<Page<Software>> ListOperatingSystemAsync(string operatingSystemIdOrSlug, PageRequest? page = null, CancellationToken cancellationToken = default);
}

/// <inheritdoc />
public sealed class SoftwareApi : BinaryLaneResourceBase, ISoftwareApi
{
    /// <summary>Initializes the software resource.</summary>
    public SoftwareApi(IBinaryLaneApiExecutor executor, BinaryLaneJsonSerializerOptions json)
        : base(executor, json)
    {
    }

    /// <inheritdoc />
    public Task<Page<Software>> ListAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetPageAsync<Software>("v2/software", "software", page, null, cancellationToken);

    /// <inheritdoc />
    public IAsyncEnumerable<Software> ListAllAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetAllPagesAsync<Software>("v2/software", "software", page, null, cancellationToken);

    /// <inheritdoc />
    public Task<Software> GetAsync(long softwareId, CancellationToken cancellationToken = default) =>
        GetItemAsync<Software>($"v2/software/{softwareId}", "software", cancellationToken);

    /// <inheritdoc />
    public Task<Page<Software>> ListOperatingSystemAsync(string operatingSystemIdOrSlug, PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetPageAsync<Software>(
            $"v2/software/operating_system/{EscapePathSegment(operatingSystemIdOrSlug)}",
            "software",
            page,
            null,
            cancellationToken);
}
