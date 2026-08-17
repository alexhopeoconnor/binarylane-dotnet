using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BinaryLane.Api.V2.Models;

/// <summary>Creates a new VPC.</summary>
public sealed class CreateVpcRequest : BinaryLaneRequestModel
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("route_entries")]
    public IReadOnlyList<RouteEntryRequest>? RouteEntries { get; init; }

    [JsonPropertyName("ip_range")]
    public string? IpRange { get; init; }
}

/// <summary>Replaces VPC configuration. Omitted properties are cleared by the provider.</summary>
public sealed class UpdateVpcRequest : BinaryLaneRequestModel
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("route_entries")]
    public IReadOnlyList<RouteEntryRequest>? RouteEntries { get; init; }
}

/// <summary>Partially updates VPC configuration.</summary>
public sealed class PatchVpcRequest : BinaryLaneRequestModel
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("route_entries")]
    public IReadOnlyList<RouteEntryRequest>? RouteEntries { get; init; }
}

/// <summary>A VPC route entry supplied in a create or update request.</summary>
public sealed class RouteEntryRequest : BinaryLaneRequestModel
{
    [JsonPropertyName("router")]
    public string Router { get; init; } = string.Empty;

    [JsonPropertyName("destination")]
    public string Destination { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; init; }
}
