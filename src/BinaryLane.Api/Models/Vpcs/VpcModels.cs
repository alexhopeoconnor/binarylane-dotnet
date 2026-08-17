using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BinaryLane.Api.V2.Models;

/// <summary>A virtual private cloud.</summary>
public sealed class Vpc : BinaryLaneDto
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("ip_range")]
    public string IpRange { get; init; } = string.Empty;

    [JsonPropertyName("route_entries")]
    public IReadOnlyList<RouteEntry> RouteEntries { get; init; } = Array.Empty<RouteEntry>();
}

/// <summary>A route within a VPC.</summary>
public sealed class RouteEntry : BinaryLaneDto
{
    [JsonPropertyName("router")]
    public string Router { get; init; } = string.Empty;

    [JsonPropertyName("destination")]
    public string Destination { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; init; }
}

/// <summary>A resource connected to a VPC.</summary>
public sealed class VpcMember : BinaryLaneDto
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    /// <summary>Provider resource type.</summary>
    [JsonPropertyName("resource_type")]
    public string ResourceType { get; init; } = string.Empty;

    /// <remarks>The provider documents this as a string, unlike action resource identifiers.</remarks>
    [JsonPropertyName("resource_id")]
    public string ResourceId { get; init; } = string.Empty;

    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; init; }
}

public sealed class VpcResponse : BinaryLaneDto
{
    [JsonPropertyName("vpc")]
    public Vpc Vpc { get; init; } = new();
}

public sealed class VpcsResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("vpcs")]
    public IReadOnlyList<Vpc> Vpcs { get; init; } = Array.Empty<Vpc>();
}

public sealed class VpcMembersResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("members")]
    public IReadOnlyList<VpcMember> Members { get; init; } = Array.Empty<VpcMember>();
}
