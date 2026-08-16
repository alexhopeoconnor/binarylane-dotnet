using System;
using System.Collections.Generic;
using System.Net.Http;

namespace BinaryLane.Api.V2.Http;

/// <summary>Represents one request to the BinaryLane v2 API.</summary>
public sealed class BinaryLaneRequest
{
    /// <summary>Initializes a request.</summary>
    public BinaryLaneRequest(HttpMethod method, string path)
    {
        Method = method ?? throw new ArgumentNullException(nameof(method));
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("An API path is required.", nameof(path));
        }

        Path = path;
    }

    /// <summary>HTTP method.</summary>
    public HttpMethod Method { get; }

    /// <summary>
    /// Relative v2 path (for example <c>v2/servers</c>) or an absolute HTTPS URL on the
    /// configured API origin. Relative paths are resolved against the configured
    /// <see cref="System.Net.Http.HttpClient.BaseAddress"/>. Other origins are rejected so a
    /// provider link cannot receive the configured bearer token.
    /// </summary>
    public string Path { get; }

    /// <summary>Optional JSON request body.</summary>
    public object? Body { get; set; }

    /// <summary>Optional query-string values. Null values are omitted.</summary>
    public IReadOnlyDictionary<string, object?>? Query { get; set; }

    /// <summary>
    /// Optional request headers. Authorization is supplied by the configured token provider and
    /// cannot be overridden here.
    /// </summary>
    public IReadOnlyDictionary<string, string>? Headers { get; set; }
}
