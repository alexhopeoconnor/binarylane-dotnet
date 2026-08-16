using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BinaryLane.Api.V2.Models;

/// <summary>Information about the authenticated BinaryLane account.</summary>
public sealed class Account : BinaryLaneDto
{
    [JsonPropertyName("email")]
    public string Email { get; init; } = string.Empty;

    [JsonPropertyName("email_verified")]
    public bool EmailVerified { get; init; }

    [JsonPropertyName("two_factor_authentication_enabled")]
    public bool TwoFactorAuthenticationEnabled { get; init; }

    /// <summary>Provider status, for example <c>active</c>.</summary>
    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    [JsonPropertyName("tax_code")]
    public TaxCode TaxCode { get; init; } = new();

    /// <summary>Provider payment-method values, for example <c>credit-card</c>.</summary>
    [JsonPropertyName("configured_payment_methods")]
    public IReadOnlyList<string> ConfiguredPaymentMethods { get; init; } = Array.Empty<string>();

    [JsonPropertyName("additional_ipv4_limit")]
    public int AdditionalIpv4Limit { get; init; }
}

/// <summary>A tax code currently applicable to an account or invoice.</summary>
public sealed class TaxCode : BinaryLaneDto
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    /// <summary>Provider tax-code type, for example <c>none</c> or <c>scalar</c>.</summary>
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    [JsonPropertyName("fixed_percent")]
    public double? FixedPercent { get; init; }
}

/// <summary>A provider action, which may still be in progress.</summary>
public sealed class BinaryLaneAction : BinaryLaneDto
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    /// <summary>Provider status, for example <c>in-progress</c>, <c>completed</c>, or <c>errored</c>.</summary>
    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    [JsonPropertyName("started_at")]
    public DateTimeOffset StartedAt { get; init; }

    [JsonPropertyName("completed_at")]
    public DateTimeOffset? CompletedAt { get; init; }

    /// <summary>Provider resource type, when the action is associated with one.</summary>
    [JsonPropertyName("resource_type")]
    public string? ResourceType { get; init; }

    [JsonPropertyName("resource_id")]
    public long? ResourceId { get; init; }

    [JsonPropertyName("region")]
    public Region? Region { get; init; }

    [JsonPropertyName("region_slug")]
    public string? RegionSlug { get; init; }

    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    [JsonPropertyName("reason")]
    public string Reason { get; init; } = string.Empty;

    [JsonPropertyName("progress")]
    public ActionProgress Progress { get; init; } = new();

    [JsonPropertyName("result_data")]
    public string? ResultData { get; init; }

    [JsonPropertyName("blocking_invoice_id")]
    public long? BlockingInvoiceId { get; init; }

    [JsonPropertyName("user_interaction_required")]
    public UserInteractionRequired? UserInteractionRequired { get; init; }
}

/// <summary>Progress information for a long-running provider action.</summary>
public sealed class ActionProgress : BinaryLaneDto
{
    [JsonPropertyName("current_step_detail")]
    public string? CurrentStepDetail { get; init; }

    [JsonPropertyName("percent_complete")]
    public int PercentComplete { get; init; }

    [JsonPropertyName("current_step")]
    public string? CurrentStep { get; init; }

    [JsonPropertyName("completed_steps")]
    public IReadOnlyList<string> CompletedSteps { get; init; } = Array.Empty<string>();
}

/// <summary>A link to an action related to a completed request.</summary>
public sealed class ActionLink : BinaryLaneDto
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("rel")]
    public string Rel { get; init; } = string.Empty;

    [JsonPropertyName("href")]
    public string Href { get; init; } = string.Empty;
}

/// <summary>Indicates that an action needs an explicit user decision.</summary>
public sealed class UserInteractionRequired : BinaryLaneDto
{
    /// <summary>Provider interaction type, such as <c>continue-after-ping-failure</c>.</summary>
    [JsonPropertyName("interaction_type")]
    public string InteractionType { get; init; } = string.Empty;
}

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

/// <summary>A BinaryLane image, including operating-system and backup images.</summary>
public sealed class Image : BinaryLaneDto
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    /// <summary>Provider image type, for example <c>custom</c>, <c>snapshot</c>, or <c>backup</c>.</summary>
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    [JsonPropertyName("distribution")]
    public string? Distribution { get; init; }

    [JsonPropertyName("full_name")]
    public string? FullName { get; init; }

    [JsonPropertyName("slug")]
    public string? Slug { get; init; }

    [JsonPropertyName("public")]
    public bool IsPublic { get; init; }

    [JsonPropertyName("regions")]
    public IReadOnlyList<string> Regions { get; init; } = Array.Empty<string>();

    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; init; }

    [JsonPropertyName("min_disk_size")]
    public int MinDiskSizeGigabytes { get; init; }

    [JsonPropertyName("size_gigabytes")]
    public double SizeGigabytes { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>Provider image status, for example <c>available</c>.</summary>
    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    [JsonPropertyName("error_message")]
    public string? ErrorMessage { get; init; }

    [JsonPropertyName("min_memory_megabytes")]
    public int? MinMemoryMegabytes { get; init; }

    [JsonPropertyName("distribution_surcharges")]
    public DistributionSurcharges? DistributionSurcharges { get; init; }

    [JsonPropertyName("distribution_info")]
    public DistributionInfo DistributionInfo { get; init; } = new();

    [JsonPropertyName("backup_info")]
    public BackupInfo? BackupInfo { get; init; }
}

/// <summary>Distribution-specific image installation capabilities.</summary>
public sealed class DistributionInfo : BinaryLaneDto
{
    [JsonPropertyName("image_id")]
    public long ImageId { get; init; }

    /// <summary>Provider password recovery mode.</summary>
    [JsonPropertyName("password_recovery")]
    public string PasswordRecovery { get; init; } = string.Empty;

    [JsonPropertyName("remote_access_user")]
    public string? RemoteAccessUser { get; init; }

    [JsonPropertyName("features")]
    public IReadOnlyList<string> Features { get; init; } = Array.Empty<string>();
}

/// <summary>Additional provider charges for a distribution image.</summary>
public sealed class DistributionSurcharges : BinaryLaneDto
{
    [JsonPropertyName("surcharge_base_cost")]
    public double? SurchargeBaseCost { get; init; }

    [JsonPropertyName("surcharge_per_memory_megabyte")]
    public double? SurchargePerMemoryMegabyte { get; init; }

    [JsonPropertyName("surcharge_per_memory_max_megabytes")]
    public int? SurchargePerMemoryMaxMegabytes { get; init; }

    [JsonPropertyName("surcharge_per_vcpu")]
    public double? SurchargePerVcpu { get; init; }

    [JsonPropertyName("surcharge_min_vcpu")]
    public int? SurchargeMinVcpu { get; init; }
}

/// <summary>Backup-specific metadata for an image.</summary>
public sealed class BackupInfo : BinaryLaneDto
{
    /// <summary>Provider backup slot, such as <c>daily</c> or <c>temporary</c>.</summary>
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    [JsonPropertyName("server_id")]
    public long ServerId { get; init; }

    [JsonPropertyName("offsite")]
    public bool Offsite { get; init; }

    [JsonPropertyName("locked")]
    public bool Locked { get; init; }

    [JsonPropertyName("iso")]
    public bool Iso { get; init; }

    [JsonPropertyName("backup_disks")]
    public IReadOnlyList<BackupDisk> BackupDisks { get; init; } = Array.Empty<BackupDisk>();
}

/// <summary>A disk contained in a backup image.</summary>
public sealed class BackupDisk : BinaryLaneDto
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("size_gigabytes")]
    public double SizeGigabytes { get; init; }

    [JsonPropertyName("min_disk_size")]
    public int MinDiskSizeGigabytes { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }
}

/// <summary>A provider server size and its available customisation options.</summary>
public sealed class Size : BinaryLaneDto
{
    [JsonPropertyName("slug")]
    public string Slug { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("cpu_description")]
    public string? CpuDescription { get; init; }

    [JsonPropertyName("storage_description")]
    public string? StorageDescription { get; init; }

    [JsonPropertyName("size_type")]
    public SizeType SizeType { get; init; } = new();

    [JsonPropertyName("available")]
    public bool Available { get; init; }

    [JsonPropertyName("regions")]
    public IReadOnlyList<string> Regions { get; init; } = Array.Empty<string>();

    [JsonPropertyName("regions_out_of_stock")]
    public IReadOnlyList<string>? RegionsOutOfStock { get; init; }

    [JsonPropertyName("price_monthly")]
    public double PriceMonthly { get; init; }

    [JsonPropertyName("price_hourly")]
    public double PriceHourly { get; init; }

    [JsonPropertyName("disk")]
    public int DiskGigabytes { get; init; }

    [JsonPropertyName("memory")]
    public int MemoryMegabytes { get; init; }

    [JsonPropertyName("transfer")]
    public double TransferGigabytes { get; init; }

    [JsonPropertyName("excess_transfer_cost_per_gigabyte")]
    public double ExcessTransferCostPerGigabyte { get; init; }

    [JsonPropertyName("vcpus")]
    public int Vcpus { get; init; }

    [JsonPropertyName("vcpu_units")]
    public string VcpuUnits { get; init; } = string.Empty;

    [JsonPropertyName("options")]
    public SizeOptions Options { get; init; } = new();
}

/// <summary>The provider's category for a server size.</summary>
public sealed class SizeType : BinaryLaneDto
{
    [JsonPropertyName("slug")]
    public string Slug { get; init; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; init; }
}

/// <summary>Configurable bounds and costs associated with a server size.</summary>
public sealed class SizeOptions : BinaryLaneDto
{
    [JsonPropertyName("disk_min")]
    public int DiskMinGigabytes { get; init; }

    [JsonPropertyName("disk_max")]
    public int DiskMaxGigabytes { get; init; }

    [JsonPropertyName("disk_cost_per_additional_gigabyte")]
    public double DiskCostPerAdditionalGigabyte { get; init; }

    [JsonPropertyName("restricted_disk_values")]
    public IReadOnlyList<int>? RestrictedDiskValues { get; init; }

    [JsonPropertyName("memory_max")]
    public int MemoryMaxMegabytes { get; init; }

    [JsonPropertyName("memory_cost_per_additional_megabyte")]
    public double MemoryCostPerAdditionalMegabyte { get; init; }

    [JsonPropertyName("transfer_max")]
    public double TransferMaxGigabytes { get; init; }

    [JsonPropertyName("transfer_cost_per_additional_gigabyte")]
    public double TransferCostPerAdditionalGigabyte { get; init; }

    [JsonPropertyName("ipv4_addresses_max")]
    public int Ipv4AddressesMax { get; init; }

    [JsonPropertyName("ipv4_addresses_cost_per_address")]
    public double Ipv4AddressesCostPerAddress { get; init; }

    [JsonPropertyName("discount_for_no_public_ipv4")]
    public double DiscountForNoPublicIpv4 { get; init; }

    [JsonPropertyName("daily_backups")]
    public int DailyBackups { get; init; }

    [JsonPropertyName("weekly_backups")]
    public int WeeklyBackups { get; init; }

    [JsonPropertyName("monthly_backups")]
    public int MonthlyBackups { get; init; }

    [JsonPropertyName("backups_cost_per_backup_per_gigabyte")]
    public double BackupsCostPerBackupPerGigabyte { get; init; }

    [JsonPropertyName("offsite_backups_cost_per_gigabyte")]
    public double OffsiteBackupsCostPerGigabyte { get; init; }

    [JsonPropertyName("offsite_backup_frequency_cost")]
    public OffsiteBackupFrequencyCost OffsiteBackupFrequencyCost { get; init; } = new();
}

/// <summary>The backup frequency costs published for a size.</summary>
public sealed class OffsiteBackupFrequencyCost : BinaryLaneDto
{
    [JsonPropertyName("daily_per_gigabyte")]
    public double DailyPerGigabyte { get; init; }

    [JsonPropertyName("weekly_per_gigabyte")]
    public double WeeklyPerGigabyte { get; init; }

    [JsonPropertyName("monthly_per_gigabyte")]
    public double MonthlyPerGigabyte { get; init; }
}

/// <summary>The options selected for an individual server.</summary>
public sealed class SelectedSizeOptions : BinaryLaneDto
{
    [JsonPropertyName("daily_backups")]
    public int DailyBackups { get; init; }

    [JsonPropertyName("weekly_backups")]
    public int WeeklyBackups { get; init; }

    [JsonPropertyName("monthly_backups")]
    public int MonthlyBackups { get; init; }

    [JsonPropertyName("offsite_backups")]
    public bool OffsiteBackups { get; init; }

    [JsonPropertyName("ipv4_addresses")]
    public int Ipv4Addresses { get; init; }

    [JsonPropertyName("memory")]
    public int MemoryMegabytes { get; init; }

    [JsonPropertyName("disk")]
    public int DiskGigabytes { get; init; }

    [JsonPropertyName("transfer")]
    public double TransferGigabytes { get; init; }
}
