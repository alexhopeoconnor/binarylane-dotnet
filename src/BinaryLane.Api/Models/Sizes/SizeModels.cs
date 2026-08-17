using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BinaryLane.Api.V2.Models;

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

public sealed class SizesResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("sizes")]
    public IReadOnlyList<Size> Sizes { get; init; } = Array.Empty<Size>();
}
