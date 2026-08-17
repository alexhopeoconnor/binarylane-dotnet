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

/// <summary>Lists available server sizes.</summary>
public interface ISizesApi
{
    Task<Page<Size>> ListAsync(PageRequest? page = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<Size> ListAllAsync(PageRequest? page = null, CancellationToken cancellationToken = default);
}

/// <inheritdoc />
public sealed class SizesApi : BinaryLaneResourceBase, ISizesApi
{
    /// <summary>Initializes the sizes resource.</summary>
    public SizesApi(IBinaryLaneApiExecutor executor, BinaryLaneJsonSerializerOptions json)
        : base(executor, json)
    {
    }

    /// <inheritdoc />
    public Task<Page<Size>> ListAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetPageAsync<Size>("v2/sizes", "sizes", page, null, cancellationToken);

    /// <inheritdoc />
    public IAsyncEnumerable<Size> ListAllAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetAllPagesAsync<Size>("v2/sizes", "sizes", page, null, cancellationToken);
}
