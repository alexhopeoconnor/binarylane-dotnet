using System;
using System.Collections.Generic;
using System.Net;

namespace BinaryLane.Api.V2.Errors;

/// <summary>Thrown when BinaryLane rejects credentials.</summary>
public sealed class BinaryLaneUnauthorizedException : BinaryLaneApiException
{
    internal BinaryLaneUnauthorizedException(string message, Uri requestUri, BinaryLaneApiProblem? problem, string? responseBody, IReadOnlyDictionary<string, IReadOnlyList<string>> headers)
        : base(message, HttpStatusCode.Unauthorized, requestUri, problem, responseBody, headers)
    {
    }
}
