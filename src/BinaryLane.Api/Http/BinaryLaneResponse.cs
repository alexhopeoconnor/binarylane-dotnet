using System;
using System.Collections.Generic;
using System.Net;

namespace BinaryLane.Api.V2.Http;

/// <summary>Response metadata and a deserialized BinaryLane API body.</summary>
/// <typeparam name="T">The deserialized response type.</typeparam>
public sealed class BinaryLaneResponse<T>
{
    internal BinaryLaneResponse(
        T body,
        HttpStatusCode statusCode,
        Uri requestUri,
        IReadOnlyDictionary<string, IReadOnlyList<string>> headers)
    {
        Body = body;
        StatusCode = statusCode;
        RequestUri = requestUri;
        Headers = headers;
    }

    /// <summary>Deserialized response body.</summary>
    public T Body { get; }

    /// <summary>HTTP status code returned by BinaryLane.</summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>Final request URI.</summary>
    public Uri RequestUri { get; }

    /// <summary>Response and content headers.</summary>
    public IReadOnlyDictionary<string, IReadOnlyList<string>> Headers { get; }
}
