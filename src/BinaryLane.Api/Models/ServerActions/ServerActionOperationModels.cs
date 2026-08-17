using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BinaryLane.Api.V2.Models;

/// <summary>Disables SELinux in a compatible installed distribution.</summary>
public sealed class DisableSelinuxServerAction : ServerAction
{
    public DisableSelinuxServerAction() : base(BinaryLaneValues.ServerActionType.DisableSelinux) { }
}

/// <summary>Checks whether a server is running.</summary>
public sealed class IsRunningServerAction : ServerAction
{
    public IsRunningServerAction() : base(BinaryLaneValues.ServerActionType.IsRunning) { }
}

/// <summary>Resets a server password.</summary>
public sealed class PasswordResetServerAction : ServerAction
{
    public PasswordResetServerAction() : base(BinaryLaneValues.ServerActionType.PasswordReset) { }

    [JsonPropertyName("username")]
    public string? Username { get; init; }

    /// <remarks>Do not log this value.</remarks>
    [JsonPropertyName("password")]
    public string? Password { get; init; }
}

/// <summary>Pings a server.</summary>
public sealed class PingServerAction : ServerAction
{
    public PingServerAction() : base(BinaryLaneValues.ServerActionType.Ping) { }
}

/// <summary>Power cycles a server.</summary>
public sealed class PowerCycleServerAction : ServerAction
{
    public PowerCycleServerAction() : base(BinaryLaneValues.ServerActionType.PowerCycle) { }
}

/// <summary>Powers a server off.</summary>
public sealed class PowerOffServerAction : ServerAction
{
    public PowerOffServerAction() : base(BinaryLaneValues.ServerActionType.PowerOff) { }
}

/// <summary>Powers a server on.</summary>
public sealed class PowerOnServerAction : ServerAction
{
    public PowerOnServerAction() : base(BinaryLaneValues.ServerActionType.PowerOn) { }
}

/// <summary>Reboots a server.</summary>
public sealed class RebootServerAction : ServerAction
{
    public RebootServerAction() : base(BinaryLaneValues.ServerActionType.Reboot) { }
}

/// <summary>Rebuilds a server from an image.</summary>
public sealed class RebuildServerAction : ServerAction
{
    public RebuildServerAction() : base(BinaryLaneValues.ServerActionType.Rebuild) { }

    /// <summary>An image ID or image slug. Omit to reuse the existing image.</summary>
    [JsonPropertyName("image")]
    public object? Image { get; init; }

    [JsonPropertyName("options")]
    public ImageOptions? Options { get; init; }
}

/// <summary>Renames a server.</summary>
public sealed class RenameServerAction : ServerAction
{
    public RenameServerAction() : base(BinaryLaneValues.ServerActionType.Rename) { }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;
}

/// <summary>Changes selected size options during a resize action.</summary>
public sealed class ChangeSizeOptionsRequest : BinaryLaneRequestModel
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

    [JsonPropertyName("ipv4_addresses_to_remove")]
    public IReadOnlyList<string>? Ipv4AddressesToRemove { get; init; }
}

/// <summary>Changes the source image selected by a resize operation.</summary>
public sealed class ChangeImage : BinaryLaneRequestModel
{
    /// <summary>An image ID or image slug.</summary>
    [JsonPropertyName("image")]
    public object? Image { get; init; }

    [JsonPropertyName("options")]
    public ImageOptions? Options { get; init; }
}

/// <summary>Changes licences selected by a resize operation.</summary>
public sealed class ChangeLicenses : BinaryLaneRequestModel
{
    [JsonPropertyName("licenses")]
    public IReadOnlyList<License> Licenses { get; init; } = Array.Empty<License>();
}

/// <summary>Resizes a server.</summary>
public sealed class ResizeServerAction : ServerAction
{
    public ResizeServerAction() : base(BinaryLaneValues.ServerActionType.Resize) { }

    [JsonPropertyName("size")]
    public string? Size { get; init; }

    [JsonPropertyName("options")]
    public ChangeSizeOptionsRequest? Options { get; init; }

    [JsonPropertyName("change_image")]
    public ChangeImage? ChangeImage { get; init; }

    [JsonPropertyName("change_licenses")]
    public ChangeLicenses? ChangeLicenses { get; init; }

    [JsonPropertyName("pre_action_backup")]
    public TakeBackupServerAction? PreActionBackup { get; init; }
}

/// <summary>Requests a graceful shutdown.</summary>
public sealed class ShutdownServerAction : ServerAction
{
    public ShutdownServerAction() : base(BinaryLaneValues.ServerActionType.Shutdown) { }
}

/// <summary>Reverses a scheduled server cancellation.</summary>
public sealed class UncancelServerAction : ServerAction
{
    public UncancelServerAction() : base(BinaryLaneValues.ServerActionType.Uncancel) { }
}

/// <summary>Checks server uptime.</summary>
public sealed class UptimeServerAction : ServerAction
{
    public UptimeServerAction() : base(BinaryLaneValues.ServerActionType.Uptime) { }
}
