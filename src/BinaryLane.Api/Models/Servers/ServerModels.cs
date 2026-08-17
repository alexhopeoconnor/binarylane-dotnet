using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BinaryLane.Api.V2.Models;

/// <summary>A BinaryLane server.</summary>
public sealed class Server : BinaryLaneDto
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("memory")]
    public int MemoryMegabytes { get; init; }

    [JsonPropertyName("vcpus")]
    public int Vcpus { get; init; }

    [JsonPropertyName("disk")]
    public int DiskGigabytes { get; init; }

    [JsonPropertyName("vpc_id")]
    public long? VpcId { get; init; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; init; }

    /// <summary>Provider server status, for example <c>active</c>.</summary>
    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    [JsonPropertyName("backup_ids")]
    public IReadOnlyList<long> BackupIds { get; init; } = Array.Empty<long>();

    [JsonPropertyName("features")]
    public IReadOnlyList<string> Features { get; init; } = Array.Empty<string>();

    [JsonPropertyName("region")]
    public Region Region { get; init; } = new();

    [JsonPropertyName("image")]
    public Image Image { get; init; } = new();

    [JsonPropertyName("size")]
    public Size Size { get; init; } = new();

    [JsonPropertyName("size_slug")]
    public string SizeSlug { get; init; } = string.Empty;

    [JsonPropertyName("selected_size_options")]
    public SelectedSizeOptions? SelectedSizeOptions { get; init; }

    [JsonPropertyName("networks")]
    public ServerNetworks Networks { get; init; } = new();

    [JsonPropertyName("kernel")]
    public Kernel? Kernel { get; init; }

    [JsonPropertyName("next_backup_window")]
    public BackupWindow? NextBackupWindow { get; init; }

    [JsonPropertyName("disks")]
    public IReadOnlyList<Disk> Disks { get; init; } = Array.Empty<Disk>();

    [JsonPropertyName("backup_settings")]
    public BackupSettings BackupSettings { get; init; } = new();

    [JsonPropertyName("cancelled_at")]
    public DateTimeOffset? CancelledAt { get; init; }

    [JsonPropertyName("failover_ips")]
    public IReadOnlyList<string> FailoverIps { get; init; } = Array.Empty<string>();

    [JsonPropertyName("host")]
    public Host Host { get; init; } = new();

    [JsonPropertyName("partner_id")]
    public long? PartnerId { get; init; }

    [JsonPropertyName("password_change_supported")]
    public bool PasswordChangeSupported { get; init; }

    [JsonPropertyName("permalink")]
    public string? Permalink { get; init; }

    [JsonPropertyName("attached_backup")]
    public AttachedBackup? AttachedBackup { get; init; }

    [JsonPropertyName("advanced_features")]
    public AdvancedServerFeatures AdvancedFeatures { get; init; } = new();

    [JsonPropertyName("is_under_maintenance")]
    public bool? IsUnderMaintenance { get; init; }
}

/// <summary>A server disk.</summary>
public sealed class Disk : BinaryLaneDto
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("size_gigabytes")]
    public double SizeGigabytes { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("primary")]
    public bool Primary { get; init; }
}

/// <summary>A server network assignment.</summary>
public sealed class ServerNetwork : BinaryLaneDto
{
    [JsonPropertyName("ip_address")]
    public string IpAddress { get; init; } = string.Empty;

    /// <summary>May be represented by the provider as either a number or a string.</summary>
    [JsonPropertyName("netmask")]
    public object? Netmask { get; init; }

    [JsonPropertyName("gateway")]
    public string? Gateway { get; init; }

    /// <summary>Provider network type, either <c>public</c> or <c>private</c>.</summary>
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    [JsonPropertyName("reverse_name")]
    public string? ReverseName { get; init; }

    [JsonPropertyName("nat_target")]
    public string? NatTarget { get; init; }
}

/// <summary>All IPv4/IPv6 network information associated with a server.</summary>
public sealed class ServerNetworks : BinaryLaneDto
{
    [JsonPropertyName("v4")]
    public IReadOnlyList<ServerNetwork> V4 { get; init; } = Array.Empty<ServerNetwork>();

    [JsonPropertyName("v6")]
    public IReadOnlyList<ServerNetwork> V6 { get; init; } = Array.Empty<ServerNetwork>();

    [JsonPropertyName("port_blocking")]
    public bool PortBlocking { get; init; }

    [JsonPropertyName("separate_private_network_interface")]
    public bool? SeparatePrivateNetworkInterface { get; init; }

    [JsonPropertyName("source_and_destination_check")]
    public bool? SourceAndDestinationCheck { get; init; }

    [JsonPropertyName("recent_ddos")]
    public bool RecentDdos { get; init; }

    [JsonPropertyName("ipv6_reverse_nameservers")]
    public IReadOnlyList<string>? Ipv6ReverseNameservers { get; init; }

    [JsonPropertyName("mac_address")]
    public string MacAddress { get; init; } = string.Empty;
}

public sealed class ServerResponse : BinaryLaneDto
{
    [JsonPropertyName("server")]
    public Server Server { get; init; } = new();
}

public sealed class ServersResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("servers")]
    public IReadOnlyList<Server> Servers { get; init; } = Array.Empty<Server>();
}

public sealed class CreateServerResponse : BinaryLaneDto
{
    [JsonPropertyName("server")]
    public Server Server { get; init; } = new();

    [JsonPropertyName("links")]
    public ActionsLinks Links { get; init; } = new();
}
