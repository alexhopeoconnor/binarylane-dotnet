using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BinaryLane.Api.V2.Models;

/// <summary>Updates the IPv6 reverse nameservers configured for the account.</summary>
public sealed class ReverseNameserversRequest : BinaryLaneRequestModel
{
    [JsonPropertyName("reverse_nameservers")]
    public IReadOnlyList<string> ReverseNameservers { get; init; } = Array.Empty<string>();
}

public sealed class ReverseNameServersResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("reverse_nameservers")]
    public IReadOnlyList<string> ReverseNameservers { get; init; } = Array.Empty<string>();
}
