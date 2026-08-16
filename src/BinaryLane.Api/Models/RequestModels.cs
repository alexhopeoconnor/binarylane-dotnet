using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BinaryLane.Api.V2.Models;

/// <summary>Options selected while creating or rebuilding a server image.</summary>
public sealed class ImageOptions : BinaryLaneRequestModel
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Each identifier may be either an SSH key ID or a public-key fingerprint.</summary>
    [JsonPropertyName("ssh_keys")]
    public IReadOnlyList<object>? SshKeys { get; init; }

    /// <remarks>Do not log user-data; it can contain secrets.</remarks>
    [JsonPropertyName("user_data")]
    public string? UserData { get; init; }

    /// <remarks>Do not log this value.</remarks>
    [JsonPropertyName("password")]
    public string? Password { get; init; }
}

/// <summary>Customisable options for a server size.</summary>
public sealed class SizeOptionsRequest : BinaryLaneRequestModel
{
    [JsonPropertyName("daily_backups")]
    public int? DailyBackups { get; init; }

    [JsonPropertyName("weekly_backups")]
    public int? WeeklyBackups { get; init; }

    [JsonPropertyName("monthly_backups")]
    public int? MonthlyBackups { get; init; }

    [JsonPropertyName("offsite_backups")]
    public bool? OffsiteBackups { get; init; }

    [JsonPropertyName("ipv4_addresses")]
    public int? Ipv4Addresses { get; init; }

    [JsonPropertyName("memory")]
    public int? MemoryMegabytes { get; init; }

    [JsonPropertyName("disk")]
    public int? DiskGigabytes { get; init; }

    [JsonPropertyName("transfer")]
    public double? TransferGigabytes { get; init; }
}

/// <summary>A requested software licence allocation.</summary>
public sealed class License : BinaryLaneRequestModel
{
    [JsonPropertyName("software_id")]
    public long SoftwareId { get; init; }

    [JsonPropertyName("count")]
    public int Count { get; init; }
}

/// <summary>Creates a new BinaryLane server.</summary>
public sealed class CreateServerRequest : BinaryLaneRequestModel
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("backups")]
    public bool? Backups { get; init; }

    [JsonPropertyName("ipv6")]
    public bool? Ipv6 { get; init; }

    [JsonPropertyName("size")]
    public string Size { get; init; } = string.Empty;

    /// <summary>An image ID or provider image slug.</summary>
    [JsonPropertyName("image")]
    public object Image { get; init; } = string.Empty;

    [JsonPropertyName("region")]
    public string Region { get; init; } = string.Empty;

    [JsonPropertyName("vpc_id")]
    public long? VpcId { get; init; }

    [JsonPropertyName("vpc_ipv4_address")]
    public string? VpcIpv4Address { get; init; }

    /// <summary>Each entry may be an SSH key ID or fingerprint.</summary>
    [JsonPropertyName("ssh_keys")]
    public IReadOnlyList<object>? SshKeys { get; init; }

    [JsonPropertyName("options")]
    public SizeOptionsRequest? Options { get; init; }

    [JsonPropertyName("licenses")]
    public IReadOnlyList<License>? Licenses { get; init; }

    /// <remarks>Do not log user-data; it may contain secrets.</remarks>
    [JsonPropertyName("user_data")]
    public string? UserData { get; init; }

    [JsonPropertyName("port_blocking")]
    public bool? PortBlocking { get; init; }

    [JsonPropertyName("separate_private_network_interface")]
    public bool? SeparatePrivateNetworkInterface { get; init; }

    /// <remarks>Do not log this value.</remarks>
    [JsonPropertyName("password")]
    public string? Password { get; init; }
}

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

/// <summary>Creates a new load balancer.</summary>
public sealed class CreateLoadBalancerRequest : BinaryLaneRequestModel
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("forwarding_rules")]
    public IReadOnlyList<ForwardingRuleRequest>? ForwardingRules { get; init; }

    [JsonPropertyName("health_check")]
    public HealthCheckRequest? HealthCheck { get; init; }

    [JsonPropertyName("server_ids")]
    public IReadOnlyList<long>? ServerIds { get; init; }

    [JsonPropertyName("region")]
    public string? Region { get; init; }
}

/// <summary>Updates a load balancer.</summary>
public sealed class UpdateLoadBalancerRequest : BinaryLaneRequestModel
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("forwarding_rules")]
    public IReadOnlyList<ForwardingRuleRequest>? ForwardingRules { get; init; }

    [JsonPropertyName("health_check")]
    public HealthCheckRequest? HealthCheck { get; init; }

    [JsonPropertyName("server_ids")]
    public IReadOnlyList<long>? ServerIds { get; init; }
}

/// <summary>A load-balancer forwarding rule supplied in a request.</summary>
public sealed class ForwardingRuleRequest : BinaryLaneRequestModel
{
    /// <summary>Either <c>http</c> or <c>https</c>.</summary>
    [JsonPropertyName("entry_protocol")]
    public string EntryProtocol { get; init; } = string.Empty;
}

/// <summary>A collection of load-balancer forwarding rules.</summary>
public sealed class ForwardingRulesRequest : BinaryLaneRequestModel
{
    [JsonPropertyName("forwarding_rules")]
    public IReadOnlyList<ForwardingRuleRequest> ForwardingRules { get; init; } = Array.Empty<ForwardingRuleRequest>();
}

/// <summary>A load-balancer health check supplied in a request.</summary>
public sealed class HealthCheckRequest : BinaryLaneRequestModel
{
    /// <summary>One of <c>http</c>, <c>https</c>, or <c>both</c>.</summary>
    [JsonPropertyName("protocol")]
    public string? Protocol { get; init; }

    [JsonPropertyName("path")]
    public string? Path { get; init; }
}

/// <summary>A collection of server IDs supplied to a load-balancer operation.</summary>
public sealed class ServerIdsRequest : BinaryLaneRequestModel
{
    [JsonPropertyName("server_ids")]
    public IReadOnlyList<long> ServerIds { get; init; } = Array.Empty<long>();
}

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

/// <summary>Creates an SSH key.</summary>
public sealed class SshKeyRequest : BinaryLaneRequestModel
{
    [JsonPropertyName("public_key")]
    public string PublicKey { get; init; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("default")]
    public bool? IsDefault { get; init; }
}

/// <summary>Updates an SSH key.</summary>
public sealed class UpdateSshKeyRequest : BinaryLaneRequestModel
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("default")]
    public bool? IsDefault { get; init; }
}

/// <summary>Changes mutable metadata on an image.</summary>
public sealed class ImageRequest : BinaryLaneRequestModel
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("locked")]
    public bool? Locked { get; init; }
}

/// <summary>Uploads an image into a backup slot.</summary>
public sealed class UploadImageRequest : BinaryLaneRequestModel
{
    /// <summary>Provider backup slot, required unless the replacement strategy is <c>specified</c>.</summary>
    [JsonPropertyName("backup_type")]
    public string? BackupType { get; init; }

    /// <summary>Provider replacement strategy: <c>none</c>, <c>specified</c>, <c>oldest</c>, or <c>newest</c>.</summary>
    [JsonPropertyName("replacement_strategy")]
    public string ReplacementStrategy { get; init; } = string.Empty;

    [JsonPropertyName("backup_id_to_replace")]
    public long? BackupIdToReplace { get; init; }

    [JsonPropertyName("label")]
    public string? Label { get; init; }

    [JsonPropertyName("url")]
    public string Url { get; init; } = string.Empty;
}

/// <summary>Updates the IPv6 reverse nameservers configured for the account.</summary>
public sealed class ReverseNameserversRequest : BinaryLaneRequestModel
{
    [JsonPropertyName("reverse_nameservers")]
    public IReadOnlyList<string> ReverseNameservers { get; init; } = Array.Empty<string>();
}

/// <summary>Allows or declines the interaction requested by an action.</summary>
public sealed class ProceedRequest : BinaryLaneRequestModel
{
    [JsonPropertyName("proceed")]
    public bool Proceed { get; init; }
}

/// <summary>Updates threshold-alert configuration.</summary>
public sealed class ThresholdAlertRequest : BinaryLaneRequestModel
{
    /// <summary>Provider alert type.</summary>
    [JsonPropertyName("alert_type")]
    public string AlertType { get; init; } = string.Empty;

    [JsonPropertyName("enabled")]
    public bool? Enabled { get; init; }

    [JsonPropertyName("value")]
    public int? Value { get; init; }
}
