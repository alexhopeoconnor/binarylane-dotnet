using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BinaryLane.Api.V2.Models;

/// <summary>A DNS domain managed through the BinaryLane API.</summary>
public sealed class Domain : BinaryLaneDto
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("current_nameservers")]
    public IReadOnlyList<string> CurrentNameservers { get; init; } = Array.Empty<string>();

    [JsonPropertyName("ttl")]
    public int? Ttl { get; init; }

    [JsonPropertyName("zone_file")]
    public string ZoneFile { get; init; } = string.Empty;
}

/// <summary>A DNS record in a BinaryLane-managed domain.</summary>
public sealed class DomainRecord : BinaryLaneDto
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    /// <summary>Provider DNS record type, such as <c>A</c>, <c>AAAA</c>, or <c>TXT</c>.</summary>
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("data")]
    public string? Data { get; init; }

    [JsonPropertyName("priority")]
    public int? Priority { get; init; }

    [JsonPropertyName("port")]
    public int? Port { get; init; }

    [JsonPropertyName("ttl")]
    public int Ttl { get; init; }

    [JsonPropertyName("weight")]
    public int? Weight { get; init; }

    [JsonPropertyName("flags")]
    public int? Flags { get; init; }

    [JsonPropertyName("tag")]
    public string? Tag { get; init; }
}

public sealed class LocalNameserversResponse : BinaryLaneDto
{
    [JsonPropertyName("local_nameservers")]
    public IReadOnlyList<string> LocalNameservers { get; init; } = Array.Empty<string>();
}

public sealed class DomainResponse : BinaryLaneDto
{
    [JsonPropertyName("domain")]
    public Domain Domain { get; init; } = new();
}

public sealed class DomainsResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("domains")]
    public IReadOnlyList<Domain> Domains { get; init; } = Array.Empty<Domain>();
}

public sealed class DomainRecordResponse : BinaryLaneDto
{
    [JsonPropertyName("domain_record")]
    public DomainRecord DomainRecord { get; init; } = new();
}

public sealed class DomainRecordsResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("domain_records")]
    public IReadOnlyList<DomainRecord> DomainRecords { get; init; } = Array.Empty<DomainRecord>();
}
