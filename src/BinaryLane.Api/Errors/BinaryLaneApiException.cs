using System;
using System.Collections.Generic;
using System.Net;

namespace BinaryLane.Api.V2.Errors;

/// <summary>Base exception for an unsuccessful BinaryLane API response.</summary>
public class BinaryLaneApiException : Exception
{
    internal BinaryLaneApiException(
        string message,
        HttpStatusCode statusCode,
        Uri requestUri,
        BinaryLaneApiProblem? problem,
        string? responseBody,
        IReadOnlyDictionary<string, IReadOnlyList<string>> headers)
        : base(message)
    {
        StatusCode = statusCode;
        RequestUri = requestUri;
        Problem = problem;
        ResponseBody = responseBody;
        Headers = headers;
    }

    /// <summary>HTTP status code returned by BinaryLane.</summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>The request URI which produced the response.</summary>
    public Uri RequestUri { get; }

    /// <summary>Structured problem details, when BinaryLane supplied them.</summary>
    public BinaryLaneApiProblem? Problem { get; }

    /// <summary>
    /// Bounded raw response text for diagnostics. It omits request headers, but can contain
    /// provider-supplied or user-provided data; avoid logging it indiscriminately.
    /// </summary>
    public string? ResponseBody { get; }

    /// <summary>Response and content headers.</summary>
    public IReadOnlyDictionary<string, IReadOnlyList<string>> Headers { get; }
}
