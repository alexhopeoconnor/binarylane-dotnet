namespace BinaryLane.Api.V2.Errors;

/// <summary>A structured problem response returned by BinaryLane.</summary>
public sealed class BinaryLaneApiProblem
{
    /// <summary>Problem type URI, if supplied.</summary>
    public string? Type { get; internal set; }

    /// <summary>Short problem title, if supplied.</summary>
    public string? Title { get; internal set; }

    /// <summary>Human-readable problem detail, if supplied.</summary>
    public string? Detail { get; internal set; }

    /// <summary>Provider-supplied status code, if supplied.</summary>
    public int? Status { get; internal set; }
}
