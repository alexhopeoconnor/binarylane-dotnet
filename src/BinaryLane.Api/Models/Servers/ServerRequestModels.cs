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
