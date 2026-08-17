using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BinaryLane.Api.V2.Models;

/// <summary>A BinaryLane region in which a resource can be created.</summary>
public sealed class Region : BinaryLaneDto
{
    [JsonPropertyName("slug")]
    public string Slug { get; init; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("sizes")]
    public IReadOnlyList<string> Sizes { get; init; } = Array.Empty<string>();

    [JsonPropertyName("available")]
    public bool Available { get; init; }

    [JsonPropertyName("features")]
    public IReadOnlyList<string> Features { get; init; } = Array.Empty<string>();

    [JsonPropertyName("name_servers")]
    public IReadOnlyList<string> NameServers { get; init; } = Array.Empty<string>();
}

public sealed class RegionsResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("regions")]
    public IReadOnlyList<Region> Regions { get; init; } = Array.Empty<Region>();
}
