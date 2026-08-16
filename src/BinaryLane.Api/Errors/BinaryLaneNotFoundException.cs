using System;
using System.Collections.Generic;
using System.Net;

namespace BinaryLane.Api.V2.Errors;

/// <summary>Thrown when BinaryLane cannot find a requested resource.</summary>
public sealed class BinaryLaneNotFoundException : BinaryLaneApiException
{
    internal BinaryLaneNotFoundException(string message, Uri requestUri, BinaryLaneApiProblem? problem, string? responseBody, IReadOnlyDictionary<string, IReadOnlyList<string>> headers)
        : base(message, HttpStatusCode.NotFound, requestUri, problem, responseBody, headers)
    {
    }
}
