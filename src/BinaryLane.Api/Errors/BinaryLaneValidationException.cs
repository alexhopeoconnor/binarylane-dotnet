using System;
using System.Collections.Generic;
using System.Net;

namespace BinaryLane.Api.V2.Errors;

/// <summary>Thrown when BinaryLane rejects a malformed or invalid request.</summary>
public sealed class BinaryLaneValidationException : BinaryLaneApiException
{
    internal BinaryLaneValidationException(string message, HttpStatusCode statusCode, Uri requestUri, BinaryLaneApiProblem? problem, string? responseBody, IReadOnlyDictionary<string, IReadOnlyList<string>> headers)
        : base(message, statusCode, requestUri, problem, responseBody, headers)
    {
    }
}
