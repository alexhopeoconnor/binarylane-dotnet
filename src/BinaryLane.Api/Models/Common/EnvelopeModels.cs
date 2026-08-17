using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BinaryLane.Api.V2.Models;

/// <summary>Pagination metadata returned by BinaryLane list endpoints.</summary>
public sealed class PageMeta : BinaryLaneDto
{
    [JsonPropertyName("total")]
    public int Total { get; init; }
}

/// <summary>Pagination links returned by BinaryLane list endpoints.</summary>
public sealed class PageLinks : BinaryLaneDto
{
    [JsonPropertyName("pages")]
    public PageNavigation Pages { get; init; } = new();
}

/// <summary>URLs for neighbouring pages in a BinaryLane list response.</summary>
public sealed class PageNavigation : BinaryLaneDto
{
    [JsonPropertyName("first")]
    public string? First { get; init; }

    [JsonPropertyName("prev")]
    public string? Previous { get; init; }

    [JsonPropertyName("next")]
    public string? Next { get; init; }

    [JsonPropertyName("last")]
    public string? Last { get; init; }
}

/// <summary>An RFC 7807-style problem returned by BinaryLane.</summary>
public class ProblemDetails : BinaryLaneDto
{
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    [JsonPropertyName("title")]
    public string? Title { get; init; }

    [JsonPropertyName("status")]
    public int? Status { get; init; }

    [JsonPropertyName("detail")]
    public string? Detail { get; init; }

    [JsonPropertyName("instance")]
    public string? Instance { get; init; }
}

/// <summary>A validation problem returned by BinaryLane.</summary>
public sealed class ValidationProblemDetails : ProblemDetails
{
    [JsonPropertyName("errors")]
    public IReadOnlyDictionary<string, IReadOnlyList<string>>? Errors { get; init; }
}
