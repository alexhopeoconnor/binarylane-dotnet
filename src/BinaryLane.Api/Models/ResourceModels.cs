using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BinaryLane.Api.V2.Models;

/// <summary>The current billing balance for the authenticated account.</summary>
public sealed class Balance : BinaryLaneDto
{
    [JsonPropertyName("unbilled_total")]
    public double UnbilledTotal { get; init; }

    [JsonPropertyName("available_credit")]
    public double AvailableCredit { get; init; }

    [JsonPropertyName("charges")]
    public IReadOnlyList<ChargeInformation> Charges { get; init; } = Array.Empty<ChargeInformation>();

    [JsonPropertyName("generated_at")]
    public DateTimeOffset? GeneratedAt { get; init; }
}

/// <summary>A charge included in a balance response.</summary>
public sealed class ChargeInformation : BinaryLaneDto
{
    [JsonPropertyName("created")]
    public DateTimeOffset Created { get; init; }

    [JsonPropertyName("description")]
    public string Description { get; init; } = string.Empty;

    [JsonPropertyName("total")]
    public double Total { get; init; }

    [JsonPropertyName("ongoing")]
    public bool Ongoing { get; init; }
}

/// <summary>A BinaryLane invoice.</summary>
public sealed class Invoice : BinaryLaneDto
{
    [JsonPropertyName("invoice_id")]
    public long InvoiceId { get; init; }

    [JsonPropertyName("reference")]
    public string? Reference { get; init; }

    [JsonPropertyName("invoice_number")]
    public string InvoiceNumber { get; init; } = string.Empty;

    [JsonPropertyName("amount")]
    public double Amount { get; init; }

    [JsonPropertyName("tax_code")]
    public TaxCode TaxCode { get; init; } = new();

    [JsonPropertyName("tax")]
    public double Tax { get; init; }

    [JsonPropertyName("created")]
    public DateTimeOffset Created { get; init; }

    [JsonPropertyName("date_due")]
    public DateTimeOffset DateDue { get; init; }

    [JsonPropertyName("date_overdue")]
    public DateTimeOffset DateOverdue { get; init; }

    [JsonPropertyName("paid")]
    public bool Paid { get; init; }

    [JsonPropertyName("refunded")]
    public bool Refunded { get; init; }

    [JsonPropertyName("payment_failure_count")]
    public int? PaymentFailureCount { get; init; }

    [JsonPropertyName("invoice_items")]
    public IReadOnlyList<InvoiceLineItem> InvoiceItems { get; init; } = Array.Empty<InvoiceLineItem>();

    [JsonPropertyName("invoice_download_url")]
    public string? InvoiceDownloadUrl { get; init; }

    [JsonPropertyName("invoice_view_url")]
    public string? InvoiceViewUrl { get; init; }
}

/// <summary>A line item in a BinaryLane invoice.</summary>
public sealed class InvoiceLineItem : BinaryLaneDto
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("amount")]
    public double Amount { get; init; }

    [JsonPropertyName("amount_includes_tax")]
    public bool AmountIncludesTax { get; init; }
}

/// <summary>Current data-transfer use for a server.</summary>
public sealed class DataUsage : BinaryLaneDto
{
    [JsonPropertyName("server_id")]
    public long ServerId { get; init; }

    [JsonPropertyName("expires")]
    public DateTimeOffset Expires { get; init; }

    [JsonPropertyName("transfer_gigabytes")]
    public long TransferGigabytes { get; init; }

    [JsonPropertyName("current_transfer_usage_gigabytes")]
    public double CurrentTransferUsageGigabytes { get; init; }

    [JsonPropertyName("transfer_period_end")]
    public DateTimeOffset TransferPeriodEnd { get; init; }
}

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

/// <summary>A temporary download link for an image.</summary>
public sealed class ImageDownload : BinaryLaneDto
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("expiry")]
    public DateTimeOffset Expiry { get; init; }

    [JsonPropertyName("disks")]
    public IReadOnlyList<ImageDiskDownload> Disks { get; init; } = Array.Empty<ImageDiskDownload>();
}

/// <summary>Download URLs for one disk in an image download.</summary>
public sealed class ImageDiskDownload : BinaryLaneDto
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("compressed_url")]
    public string CompressedUrl { get; init; } = string.Empty;

    [JsonPropertyName("raw_url")]
    public string RawUrl { get; init; } = string.Empty;
}

/// <summary>An SSH public key owned by the authenticated account.</summary>
public sealed class SshKey : BinaryLaneDto
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("fingerprint")]
    public string Fingerprint { get; init; } = string.Empty;

    [JsonPropertyName("public_key")]
    public string PublicKey { get; init; } = string.Empty;

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("default")]
    public bool IsDefault { get; init; }
}

/// <summary>A BinaryLane load balancer.</summary>
public sealed class LoadBalancer : BinaryLaneDto
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("ip")]
    public string Ip { get; init; } = string.Empty;

    /// <summary>Provider load balancer status, such as <c>active</c>.</summary>
    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; init; }

    [JsonPropertyName("forwarding_rules")]
    public IReadOnlyList<ForwardingRule> ForwardingRules { get; init; } = Array.Empty<ForwardingRule>();

    [JsonPropertyName("health_check")]
    public HealthCheck HealthCheck { get; init; } = new();

    [JsonPropertyName("region")]
    public Region? Region { get; init; }

    [JsonPropertyName("server_ids")]
    public IReadOnlyList<long> ServerIds { get; init; } = Array.Empty<long>();
}

/// <summary>A load-balancer forwarding rule.</summary>
public sealed class ForwardingRule : BinaryLaneDto
{
    /// <summary>Provider rule protocol, either <c>http</c> or <c>https</c>.</summary>
    [JsonPropertyName("entry_protocol")]
    public string EntryProtocol { get; init; } = string.Empty;
}

/// <summary>A load-balancer health check.</summary>
public sealed class HealthCheck : BinaryLaneDto
{
    /// <summary>Provider health check protocol, such as <c>http</c>, <c>https</c>, or <c>both</c>.</summary>
    [JsonPropertyName("protocol")]
    public string Protocol { get; init; } = string.Empty;

    [JsonPropertyName("path")]
    public string Path { get; init; } = string.Empty;
}

/// <summary>A load-balancer configuration option available to the account.</summary>
public sealed class LoadBalancerAvailabilityOption : BinaryLaneDto
{
    [JsonPropertyName("regions")]
    public IReadOnlyList<string>? Regions { get; init; }

    [JsonPropertyName("anycast")]
    public bool Anycast { get; init; }

    [JsonPropertyName("price_monthly")]
    public double PriceMonthly { get; init; }

    [JsonPropertyName("price_hourly")]
    public double PriceHourly { get; init; }
}

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

/// <summary>A scheduled backup window.</summary>
public sealed class BackupWindow : BinaryLaneDto
{
    [JsonPropertyName("start")]
    public DateTimeOffset Start { get; init; }

    [JsonPropertyName("end")]
    public DateTimeOffset End { get; init; }
}

/// <summary>A server's configured backup schedule.</summary>
public sealed class BackupSettings : BinaryLaneDto
{
    [JsonPropertyName("backup_hour_of_day")]
    public int BackupHourOfDay { get; init; }

    [JsonPropertyName("backup_day_of_week")]
    public int BackupDayOfWeek { get; init; }

    [JsonPropertyName("backup_day_of_month")]
    public int BackupDayOfMonth { get; init; }

    [JsonPropertyName("offsite_backup_settings")]
    public OffsiteBackupSettings? OffsiteBackupSettings { get; init; }
}

/// <summary>Offsite-backup configuration for a server.</summary>
public sealed class OffsiteBackupSettings : BinaryLaneDto
{
    [JsonPropertyName("use_custom_backup_location")]
    public bool UseCustomBackupLocation { get; init; }

    [JsonPropertyName("offsite_backup_location")]
    public string? OffsiteBackupLocation { get; init; }

    [JsonPropertyName("manage_offsite_copies")]
    public bool? ManageOffsiteCopies { get; init; }
}

/// <summary>The host backing a server.</summary>
public sealed class Host : BinaryLaneDto
{
    [JsonPropertyName("display_name")]
    public string DisplayName { get; init; } = string.Empty;

    [JsonPropertyName("uptime_ms")]
    public long? UptimeMilliseconds { get; init; }

    [JsonPropertyName("status_page")]
    public string? StatusPage { get; init; }
}

/// <summary>Metadata about a backup currently attached to a server.</summary>
public sealed class AttachedBackup : BinaryLaneDto
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("disk_identifiers")]
    public IReadOnlyList<string> DiskIdentifiers { get; init; } = Array.Empty<string>();

    [JsonPropertyName("attached_at")]
    public DateTimeOffset? AttachedAt { get; init; }

    [JsonPropertyName("attachment_expires")]
    public DateTimeOffset? AttachmentExpires { get; init; }
}

/// <summary>Advanced virtual-machine features configured for a server.</summary>
public sealed class AdvancedServerFeatures : BinaryLaneDto
{
    [JsonPropertyName("processor_model")]
    public long? ProcessorModel { get; init; }

    /// <summary>Provider virtual machine type.</summary>
    [JsonPropertyName("machine_type")]
    public string? MachineType { get; init; }

    /// <summary>Provider virtual video device.</summary>
    [JsonPropertyName("video_device")]
    public string VideoDevice { get; init; } = string.Empty;

    /// <summary>Provider advanced-feature values.</summary>
    [JsonPropertyName("enabled_advanced_features")]
    public IReadOnlyList<string> EnabledAdvancedFeatures { get; init; } = Array.Empty<string>();
}

/// <summary>Advanced server features currently available to the account.</summary>
public sealed class AvailableAdvancedServerFeatures : BinaryLaneDto
{
    [JsonPropertyName("processor_models")]
    public IReadOnlyList<ProcessorModel> ProcessorModels { get; init; } = Array.Empty<ProcessorModel>();

    [JsonPropertyName("machine_types")]
    public IReadOnlyList<string> MachineTypes { get; init; } = Array.Empty<string>();

    [JsonPropertyName("advanced_features")]
    public IReadOnlyList<string> AdvancedFeatures { get; init; } = Array.Empty<string>();
}

/// <summary>An available processor model.</summary>
public sealed class ProcessorModel : BinaryLaneDto
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; init; }
}

/// <summary>An advanced firewall rule applied to a server.</summary>
public sealed class AdvancedFirewallRule : BinaryLaneDto
{
    [JsonPropertyName("source_addresses")]
    public IReadOnlyList<string> SourceAddresses { get; init; } = Array.Empty<string>();

    [JsonPropertyName("destination_addresses")]
    public IReadOnlyList<string> DestinationAddresses { get; init; } = Array.Empty<string>();

    [JsonPropertyName("destination_ports")]
    public IReadOnlyList<string>? DestinationPorts { get; init; }

    /// <summary>Provider protocol value, such as <c>tcp</c>.</summary>
    [JsonPropertyName("protocol")]
    public string Protocol { get; init; } = string.Empty;

    /// <summary>Provider action value, such as <c>accept</c>.</summary>
    [JsonPropertyName("action")]
    public string Action { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; init; }
}

/// <summary>An available server kernel.</summary>
public sealed class Kernel : BinaryLaneDto
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("version")]
    public string? Version { get; init; }
}

/// <summary>A configured server threshold alert.</summary>
public sealed class ThresholdAlert : BinaryLaneDto
{
    /// <summary>Provider alert type.</summary>
    [JsonPropertyName("alert_type")]
    public string AlertType { get; init; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("unit")]
    public string Unit { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; init; } = string.Empty;

    [JsonPropertyName("enabled")]
    public bool Enabled { get; init; }

    [JsonPropertyName("value")]
    public int Value { get; init; }

    [JsonPropertyName("current_value")]
    public int? CurrentValue { get; init; }

    [JsonPropertyName("last_raised")]
    public DateTimeOffset? LastRaised { get; init; }

    [JsonPropertyName("last_cleared")]
    public DateTimeOffset? LastCleared { get; init; }
}

/// <summary>Software licensed on a server.</summary>
public sealed class LicensedSoftware : BinaryLaneDto
{
    [JsonPropertyName("software")]
    public Software Software { get; init; } = new();

    [JsonPropertyName("licence_count")]
    public int LicenceCount { get; init; }

    [JsonPropertyName("incompatible")]
    public bool Incompatible { get; init; }
}

/// <summary>A software product that can be licensed on BinaryLane servers.</summary>
public sealed class Software : BinaryLaneDto
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("enabled")]
    public bool Enabled { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; init; } = string.Empty;

    [JsonPropertyName("cost_per_licence_per_month")]
    public double CostPerLicencePerMonth { get; init; }

    [JsonPropertyName("minimum_licence_count")]
    public int MinimumLicenceCount { get; init; }

    [JsonPropertyName("maximum_licence_count")]
    public int MaximumLicenceCount { get; init; }

    [JsonPropertyName("licence_step_count")]
    public int LicenceStepCount { get; init; }

    [JsonPropertyName("group")]
    public string? Group { get; init; }

    [JsonPropertyName("supported_operating_systems")]
    public IReadOnlyList<string> SupportedOperatingSystems { get; init; } = Array.Empty<string>();
}

/// <summary>A browser console session for a server.</summary>
public sealed class ServerConsole : BinaryLaneDto
{
    [JsonPropertyName("iframe")]
    public string Iframe { get; init; } = string.Empty;

    [JsonPropertyName("browser")]
    public string Browser { get; init; } = string.Empty;

    [JsonPropertyName("width")]
    public int Width { get; init; }

    [JsonPropertyName("height")]
    public int Height { get; init; }

    [JsonPropertyName("expiry")]
    public DateTimeOffset Expiry { get; init; }
}

/// <summary>Server user-data retained by BinaryLane.</summary>
public sealed class UserData : BinaryLaneDto
{
    [JsonPropertyName("user_data")]
    public string? Value { get; init; }
}

/// <summary>A sample interval for server monitoring data.</summary>
public sealed class Period : BinaryLaneDto
{
    [JsonPropertyName("start")]
    public DateTimeOffset Start { get; init; }

    [JsonPropertyName("end")]
    public DateTimeOffset End { get; init; }

    /// <summary>Provider interval, such as <c>five-minute</c> or <c>day</c>.</summary>
    [JsonPropertyName("data_interval")]
    public string DataInterval { get; init; } = string.Empty;
}

/// <summary>One monitoring aggregate sample.</summary>
public sealed class Sample : BinaryLaneDto
{
    [JsonPropertyName("cpu_usage_percent")]
    public double CpuUsagePercent { get; init; }

    [JsonPropertyName("cpu_usage_detailed")]
    public IReadOnlyList<double> CpuUsageDetailed { get; init; } = Array.Empty<double>();

    [JsonPropertyName("memory_usage_bytes")]
    public double MemoryUsageBytes { get; init; }

    [JsonPropertyName("network_incoming_kbps")]
    public double NetworkIncomingKbps { get; init; }

    [JsonPropertyName("network_outgoing_kbps")]
    public double NetworkOutgoingKbps { get; init; }

    [JsonPropertyName("storage_usage_megabytes")]
    public double StorageUsageMegabytes { get; init; }

    [JsonPropertyName("storage_read_kbps")]
    public double StorageReadKbps { get; init; }

    [JsonPropertyName("storage_write_kbps")]
    public double StorageWriteKbps { get; init; }

    [JsonPropertyName("storage_read_requests_per_second")]
    public double StorageReadRequestsPerSecond { get; init; }

    [JsonPropertyName("storage_write_requests_per_second")]
    public double StorageWriteRequestsPerSecond { get; init; }
}

/// <summary>A set of monitoring samples for a server.</summary>
public sealed class SampleSet : BinaryLaneDto
{
    [JsonPropertyName("server_id")]
    public long ServerId { get; init; }

    [JsonPropertyName("period")]
    public Period Period { get; init; } = new();

    [JsonPropertyName("average")]
    public Sample Average { get; init; } = new();

    [JsonPropertyName("maximum_memory_megabytes")]
    public double MaximumMemoryMegabytes { get; init; }

    [JsonPropertyName("maximum_storage_gigabytes")]
    public double MaximumStorageGigabytes { get; init; }
}
