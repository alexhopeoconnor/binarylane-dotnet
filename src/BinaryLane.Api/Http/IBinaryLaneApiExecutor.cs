using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BinaryLane.Api.V2.Http;

/// <summary>
/// Low-level request executor used by resource APIs. It is public so consuming applications can
/// compose support for a newly introduced BinaryLane endpoint before a typed resource is released.
/// </summary>
public interface IBinaryLaneApiExecutor
{
    /// <summary>Sends a request and returns response metadata and the deserialized body.</summary>
    Task<BinaryLaneResponse<TResponse>> SendAsync<TResponse>(
        BinaryLaneRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Sends a request which is expected to have no response body.</summary>
    Task<BinaryLaneResponse<object?>> SendAsync(
        BinaryLaneRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>Sends a GET request.</summary>
    Task<TResponse> GetAsync<TResponse>(
        string path,
        IReadOnlyDictionary<string, object?>? query = null,
        CancellationToken cancellationToken = default);

    /// <summary>Sends a POST request.</summary>
    Task<TResponse> PostAsync<TResponse>(
        string path,
        object? body = null,
        IReadOnlyDictionary<string, object?>? query = null,
        CancellationToken cancellationToken = default);

    /// <summary>Sends a PUT request.</summary>
    Task<TResponse> PutAsync<TResponse>(
        string path,
        object? body = null,
        IReadOnlyDictionary<string, object?>? query = null,
        CancellationToken cancellationToken = default);

    /// <summary>Sends a PATCH request.</summary>
    Task<TResponse> PatchAsync<TResponse>(
        string path,
        object? body = null,
        IReadOnlyDictionary<string, object?>? query = null,
        CancellationToken cancellationToken = default);

    /// <summary>Sends a DELETE request.</summary>
    Task DeleteAsync(
        string path,
        object? body = null,
        IReadOnlyDictionary<string, object?>? query = null,
        CancellationToken cancellationToken = default);
}
