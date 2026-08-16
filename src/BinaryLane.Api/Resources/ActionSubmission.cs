using System.Net;
using System.Text.Json;
using BinaryLane.Api.V2.Models;

namespace BinaryLane.Api.V2.Resources;

/// <summary>Result of submitting an asynchronous server action.</summary>
public sealed class ActionSubmission
{
    internal ActionSubmission(HttpStatusCode statusCode, BinaryLaneAction? action, JsonElement response)
    {
        StatusCode = statusCode;
        Action = action;
        Response = response;
    }

    /// <summary>HTTP status returned by BinaryLane, commonly 200 or 202.</summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>The action supplied by BinaryLane, when present in the response.</summary>
    public BinaryLaneAction? Action { get; }

    /// <summary>Raw response for forward-compatible access to provider fields.</summary>
    public JsonElement Response { get; }

    /// <summary>Whether BinaryLane accepted the request for asynchronous completion.</summary>
    public bool IsAccepted => StatusCode == HttpStatusCode.Accepted;
}
