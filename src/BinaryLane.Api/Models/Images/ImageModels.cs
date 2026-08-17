using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BinaryLane.Api.V2.Models;

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

public sealed class ImageResponse : BinaryLaneDto
{
    [JsonPropertyName("image")]
    public Image Image { get; init; } = new();
}

public sealed class ImagesResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("images")]
    public IReadOnlyList<Image> Images { get; init; } = Array.Empty<Image>();
}

public sealed class ImageDownloadResponse : BinaryLaneDto
{
    [JsonPropertyName("link")]
    public ImageDownload Link { get; init; } = new();
}
