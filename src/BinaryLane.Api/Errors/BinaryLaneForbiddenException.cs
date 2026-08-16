using System;
using System.Collections.Generic;
using System.Net;

namespace BinaryLane.Api.V2.Errors;

/// <summary>Thrown when the current account is not authorized for an operation.</summary>
public sealed class BinaryLaneForbiddenException : BinaryLaneApiException
{
    internal BinaryLaneForbiddenException(string message, Uri requestUri, BinaryLaneApiProblem? problem, string? responseBody, IReadOnlyDictionary<string, IReadOnlyList<string>> headers)
        : base(message, HttpStatusCode.Forbidden, requestUri, problem, responseBody, headers)
    {
    }
}
