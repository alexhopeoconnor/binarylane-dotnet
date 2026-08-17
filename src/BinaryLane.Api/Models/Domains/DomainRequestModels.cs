using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BinaryLane.Api.V2.Models;

/// <summary>Creates a DNS domain.</summary>
public sealed class DomainRequest : BinaryLaneRequestModel
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("ip_address")]
    public string? IpAddress { get; init; }
}

/// <summary>Requests refresh of the nameserver cache for domains.</summary>
public sealed class DomainRefreshRequest : BinaryLaneRequestModel
{
    [JsonPropertyName("domain_names")]
    public IReadOnlyList<string> DomainNames { get; init; } = Array.Empty<string>();
}

/// <summary>Creates a DNS record.</summary>
public sealed class DomainRecordRequest : BinaryLaneRequestModel
{
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("data")]
    public string Data { get; init; } = string.Empty;

    [JsonPropertyName("priority")]
    public int? Priority { get; init; }

    [JsonPropertyName("port")]
    public int? Port { get; init; }

    [JsonPropertyName("ttl")]
    public int? Ttl { get; init; }

    [JsonPropertyName("weight")]
    public int? Weight { get; init; }

    [JsonPropertyName("flags")]
    public int? Flags { get; init; }

    [JsonPropertyName("tag")]
    public string? Tag { get; init; }
}

/// <summary>Updates only the supplied parts of a DNS record.</summary>
public sealed class UpdateDomainRecordRequest : BinaryLaneRequestModel
{
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("data")]
    public string? Data { get; init; }

    [JsonPropertyName("priority")]
    public int? Priority { get; init; }

    [JsonPropertyName("port")]
    public int? Port { get; init; }

    [JsonPropertyName("ttl")]
    public int? Ttl { get; init; }

    [JsonPropertyName("weight")]
    public int? Weight { get; init; }

    [JsonPropertyName("flags")]
    public int? Flags { get; init; }

    [JsonPropertyName("tag")]
    public string? Tag { get; init; }
}
