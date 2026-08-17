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

/// <summary>Lists BinaryLane regions.</summary>
public interface IRegionsApi
{
    Task<Page<Region>> ListAsync(PageRequest? page = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<Region> ListAllAsync(PageRequest? page = null, CancellationToken cancellationToken = default);
}

/// <inheritdoc />
public sealed class RegionsApi : BinaryLaneResourceBase, IRegionsApi
{
    /// <summary>Initializes the regions resource.</summary>
    public RegionsApi(IBinaryLaneApiExecutor executor, BinaryLaneJsonSerializerOptions json)
        : base(executor, json)
    {
    }

    /// <inheritdoc />
    public Task<Page<Region>> ListAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetPageAsync<Region>("v2/regions", "regions", page, null, cancellationToken);

    /// <inheritdoc />
    public IAsyncEnumerable<Region> ListAllAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetAllPagesAsync<Region>("v2/regions", "regions", page, null, cancellationToken);
}

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
