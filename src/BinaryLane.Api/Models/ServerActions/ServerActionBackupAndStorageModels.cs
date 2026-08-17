using System.Text.Json.Serialization;

namespace BinaryLane.Api.V2.Models;

/// <summary>Adds an additional disk to a server.</summary>
public sealed class AddDiskServerAction : ServerAction
{
    public AddDiskServerAction() : base(BinaryLaneValues.ServerActionType.AddDisk) { }

    [JsonPropertyName("size_gigabytes")]
    public int SizeGigabytes { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }
}

/// <summary>Attaches a backup image to a server.</summary>
public sealed class AttachBackupServerAction : ServerAction
{
    public AttachBackupServerAction() : base(BinaryLaneValues.ServerActionType.AttachBackup) { }

    [JsonPropertyName("image")]
    public long ImageId { get; init; }
}

/// <summary>Changes a server backup schedule.</summary>
public sealed class ChangeBackupScheduleServerAction : ServerAction
{
    public ChangeBackupScheduleServerAction() : base(BinaryLaneValues.ServerActionType.ChangeBackupSchedule) { }

    [JsonPropertyName("backup_hour_of_day")]
    public int? BackupHourOfDay { get; init; }

    [JsonPropertyName("backup_day_of_week")]
    public int? BackupDayOfWeek { get; init; }

    [JsonPropertyName("backup_day_of_month")]
    public int? BackupDayOfMonth { get; init; }
}

/// <summary>Changes a server kernel.</summary>
public sealed class ChangeKernelServerAction : ServerAction
{
    public ChangeKernelServerAction() : base(BinaryLaneValues.ServerActionType.ChangeKernel) { }

    [JsonPropertyName("kernel")]
    public long KernelId { get; init; }
}

/// <summary>Changes whether offsite backup copies are managed automatically.</summary>
public sealed class ChangeManageOffsiteBackupCopiesServerAction : ServerAction
{
    public ChangeManageOffsiteBackupCopiesServerAction() : base(BinaryLaneValues.ServerActionType.ChangeManageOffsiteBackupCopies) { }

    [JsonPropertyName("manage_offsite_backup_copies")]
    public bool ManageOffsiteBackupCopies { get; init; }
}

/// <summary>Changes the server's offsite backup location.</summary>
public sealed class ChangeOffsiteBackupLocationServerAction : ServerAction
{
    public ChangeOffsiteBackupLocationServerAction() : base(BinaryLaneValues.ServerActionType.ChangeOffsiteBackupLocation) { }

    [JsonPropertyName("offsite_backup_location")]
    public string? OffsiteBackupLocation { get; init; }
}

/// <summary>Clones a backup image onto another server.</summary>
public sealed class CloneUsingBackupServerAction : ServerAction
{
    public CloneUsingBackupServerAction() : base(BinaryLaneValues.ServerActionType.CloneUsingBackup) { }

    [JsonPropertyName("image_id")]
    public long ImageId { get; init; }

    [JsonPropertyName("target_server_id")]
    public long TargetServerId { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>Deletes a non-primary disk.</summary>
public sealed class DeleteDiskServerAction : ServerAction
{
    public DeleteDiskServerAction() : base(BinaryLaneValues.ServerActionType.DeleteDisk) { }

    [JsonPropertyName("disk_id")]
    public long DiskId { get; init; }
}

/// <summary>Detaches an attached backup from a server.</summary>
public sealed class DetachBackupServerAction : ServerAction
{
    public DetachBackupServerAction() : base(BinaryLaneValues.ServerActionType.DetachBackup) { }
}

/// <summary>Disables automatic backups.</summary>
public sealed class DisableBackupsServerAction : ServerAction
{
    public DisableBackupsServerAction() : base(BinaryLaneValues.ServerActionType.DisableBackups) { }
}

/// <summary>Enables automatic backups.</summary>
public sealed class EnableBackupsServerAction : ServerAction
{
    public EnableBackupsServerAction() : base(BinaryLaneValues.ServerActionType.EnableBackups) { }
}

/// <summary>Resizes an individual server disk.</summary>
public sealed class ResizeDiskServerAction : ServerAction
{
    public ResizeDiskServerAction() : base(BinaryLaneValues.ServerActionType.ResizeDisk) { }

    [JsonPropertyName("disk_id")]
    public long DiskId { get; init; }

    [JsonPropertyName("size_gigabytes")]
    public int SizeGigabytes { get; init; }
}

/// <summary>Restores a server from an image.</summary>
public sealed class RestoreServerAction : ServerAction
{
    public RestoreServerAction() : base(BinaryLaneValues.ServerActionType.Restore) { }

    /// <summary>An image ID or image slug.</summary>
    [JsonPropertyName("image")]
    public object Image { get; init; } = string.Empty;
}

/// <summary>Creates a backup of a server.</summary>
public sealed class TakeBackupServerAction : ServerAction
{
    public TakeBackupServerAction() : base(BinaryLaneValues.ServerActionType.TakeBackup) { }

    /// <summary>Provider backup slot, such as <c>daily</c> or <c>temporary</c>.</summary>
    [JsonPropertyName("backup_type")]
    public string? BackupType { get; init; }

    /// <summary>Provider replacement strategy.</summary>
    [JsonPropertyName("replacement_strategy")]
    public string ReplacementStrategy { get; init; } = string.Empty;

    [JsonPropertyName("backup_id_to_replace")]
    public long? BackupIdToReplace { get; init; }

    [JsonPropertyName("label")]
    public string? Label { get; init; }
}
