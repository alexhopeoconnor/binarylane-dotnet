using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BinaryLane.Api.V2.Models;

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
