using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BinaryLane.Api.V2.Models;

/// <summary>
/// Base payload for <c>POST /v2/servers/{server_id}/actions</c>.
/// </summary>
/// <remarks>
/// The upstream contract is a discriminated union on <c>type</c>. Its public
/// documentation also creates fragment-bearing pseudo-paths for each variant;
/// those are not separate HTTP routes. Prefer a concrete derived type for a
/// documented action. <see cref="UnknownServerAction"/> permits new provider
/// action types without waiting for an SDK release.
/// </remarks>
public class ServerAction : BinaryLaneRequestModel
{
    /// <summary>Creates an empty action for serializers and advanced callers.</summary>
    public ServerAction()
    {
    }

    /// <summary>Creates a typed action with the provider discriminator value.</summary>
    protected ServerAction(string type)
    {
        Type = type;
    }

    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;
}

/// <summary>An action type not yet represented by a concrete SDK class.</summary>
public sealed class UnknownServerAction : ServerAction
{
    public UnknownServerAction(string type)
        : base(type)
    {
    }
}

/// <summary>Adds an additional disk to a server.</summary>
public sealed class AddDiskServerAction : ServerAction
{
    public AddDiskServerAction() : base("add_disk") { }

    [JsonPropertyName("size_gigabytes")]
    public int SizeGigabytes { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }
}

/// <summary>Attaches a backup image to a server.</summary>
public sealed class AttachBackupServerAction : ServerAction
{
    public AttachBackupServerAction() : base("attach_backup") { }

    [JsonPropertyName("image")]
    public long ImageId { get; init; }
}

/// <summary>Changes advanced virtual-machine features.</summary>
public sealed class ChangeAdvancedFeaturesServerAction : ServerAction
{
    public ChangeAdvancedFeaturesServerAction() : base("change_advanced_features") { }

    [JsonPropertyName("enabled_advanced_features")]
    public IReadOnlyList<string>? EnabledAdvancedFeatures { get; init; }

    [JsonPropertyName("processor_model")]
    public long? ProcessorModel { get; init; }

    [JsonPropertyName("automatic_processor_model")]
    public bool? AutomaticProcessorModel { get; init; }

    [JsonPropertyName("machine_type")]
    public string? MachineType { get; init; }

    [JsonPropertyName("automatic_machine_type")]
    public bool? AutomaticMachineType { get; init; }

    [JsonPropertyName("video_device")]
    public string? VideoDevice { get; init; }
}

/// <summary>Replaces the advanced firewall rules on a server.</summary>
public sealed class ChangeAdvancedFirewallRulesServerAction : ServerAction
{
    public ChangeAdvancedFirewallRulesServerAction() : base("change_advanced_firewall_rules") { }

    [JsonPropertyName("firewall_rules")]
    public IReadOnlyList<AdvancedFirewallRuleRequest> FirewallRules { get; init; } = Array.Empty<AdvancedFirewallRuleRequest>();
}

/// <summary>Changes a server backup schedule.</summary>
public sealed class ChangeBackupScheduleServerAction : ServerAction
{
    public ChangeBackupScheduleServerAction() : base("change_backup_schedule") { }

    [JsonPropertyName("backup_hour_of_day")]
    public int? BackupHourOfDay { get; init; }

    [JsonPropertyName("backup_day_of_week")]
    public int? BackupDayOfWeek { get; init; }

    [JsonPropertyName("backup_day_of_month")]
    public int? BackupDayOfMonth { get; init; }
}

/// <summary>Enables or disables IPv6 for a server.</summary>
public sealed class ChangeIpv6ServerAction : ServerAction
{
    public ChangeIpv6ServerAction() : base("change_ipv6") { }

    [JsonPropertyName("enabled")]
    public bool Enabled { get; init; }
}

/// <summary>Changes a server's IPv6 reverse nameservers.</summary>
public sealed class ChangeIpv6ReverseNameserversServerAction : ServerAction
{
    public ChangeIpv6ReverseNameserversServerAction() : base("change_ipv6_reverse_nameservers") { }

    [JsonPropertyName("ipv6_reverse_nameservers")]
    public IReadOnlyList<string> Ipv6ReverseNameservers { get; init; } = Array.Empty<string>();
}

/// <summary>Changes a server kernel.</summary>
public sealed class ChangeKernelServerAction : ServerAction
{
    public ChangeKernelServerAction() : base("change_kernel") { }

    [JsonPropertyName("kernel")]
    public long KernelId { get; init; }
}

/// <summary>Changes whether offsite backup copies are managed automatically.</summary>
public sealed class ChangeManageOffsiteBackupCopiesServerAction : ServerAction
{
    public ChangeManageOffsiteBackupCopiesServerAction() : base("change_manage_offsite_backup_copies") { }

    [JsonPropertyName("manage_offsite_backup_copies")]
    public bool ManageOffsiteBackupCopies { get; init; }
}

/// <summary>Moves a server onto or off a VPC network.</summary>
public sealed class ChangeNetworkServerAction : ServerAction
{
    public ChangeNetworkServerAction() : base("change_network") { }

    [JsonPropertyName("vpc_id")]
    public long? VpcId { get; init; }
}

/// <summary>Changes the server's offsite backup location.</summary>
public sealed class ChangeOffsiteBackupLocationServerAction : ServerAction
{
    public ChangeOffsiteBackupLocationServerAction() : base("change_offsite_backup_location") { }

    [JsonPropertyName("offsite_backup_location")]
    public string? OffsiteBackupLocation { get; init; }
}

/// <summary>Changes the server's partner server relationship.</summary>
public sealed class ChangePartnerServerAction : ServerAction
{
    public ChangePartnerServerAction() : base("change_partner") { }

    [JsonPropertyName("partner_server_id")]
    public long? PartnerServerId { get; init; }
}

/// <summary>Enables or disables port blocking.</summary>
public sealed class ChangePortBlockingServerAction : ServerAction
{
    public ChangePortBlockingServerAction() : base("change_port_blocking") { }

    [JsonPropertyName("enabled")]
    public bool Enabled { get; init; }
}

/// <summary>Moves a server to a new region.</summary>
public sealed class ChangeRegionServerAction : ServerAction
{
    public ChangeRegionServerAction() : base("change_region") { }

    [JsonPropertyName("region")]
    public string Region { get; init; } = string.Empty;
}

/// <summary>Changes an IPv4 reverse name.</summary>
public sealed class ChangeReverseNameServerAction : ServerAction
{
    public ChangeReverseNameServerAction() : base("change_reverse_name") { }

    [JsonPropertyName("ipv4_address")]
    public string Ipv4Address { get; init; } = string.Empty;

    [JsonPropertyName("reverse_name")]
    public string? ReverseName { get; init; }
}

/// <summary>Enables or disables a separate private network interface.</summary>
public sealed class ChangeSeparatePrivateNetworkInterfaceServerAction : ServerAction
{
    public ChangeSeparatePrivateNetworkInterfaceServerAction() : base("change_separate_private_network_interface") { }

    [JsonPropertyName("enabled")]
    public bool Enabled { get; init; }
}

/// <summary>Enables or disables source-and-destination checking.</summary>
public sealed class ChangeSourceAndDestinationCheckServerAction : ServerAction
{
    public ChangeSourceAndDestinationCheckServerAction() : base("change_source_and_destination_check") { }

    [JsonPropertyName("enabled")]
    public bool Enabled { get; init; }
}

/// <summary>Replaces a server's threshold-alert configuration.</summary>
public sealed class ChangeThresholdAlertsServerAction : ServerAction
{
    public ChangeThresholdAlertsServerAction() : base("change_threshold_alerts") { }

    [JsonPropertyName("threshold_alerts")]
    public IReadOnlyList<ThresholdAlertRequest> ThresholdAlerts { get; init; } = Array.Empty<ThresholdAlertRequest>();
}

/// <summary>Changes a VPC IPv4 address on a server.</summary>
public sealed class ChangeVpcIpv4ServerAction : ServerAction
{
    public ChangeVpcIpv4ServerAction() : base("change_vpc_ipv4") { }

    [JsonPropertyName("current_ipv4_address")]
    public string CurrentIpv4Address { get; init; } = string.Empty;

    [JsonPropertyName("new_ipv4_address")]
    public string NewIpv4Address { get; init; } = string.Empty;
}

/// <summary>Clones a backup image onto another server.</summary>
public sealed class CloneUsingBackupServerAction : ServerAction
{
    public CloneUsingBackupServerAction() : base("clone_using_backup") { }

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
    public DeleteDiskServerAction() : base("delete_disk") { }

    [JsonPropertyName("disk_id")]
    public long DiskId { get; init; }
}

/// <summary>Detaches an attached backup from a server.</summary>
public sealed class DetachBackupServerAction : ServerAction
{
    public DetachBackupServerAction() : base("detach_backup") { }
}

/// <summary>Disables automatic backups.</summary>
public sealed class DisableBackupsServerAction : ServerAction
{
    public DisableBackupsServerAction() : base("disable_backups") { }
}

/// <summary>Disables SELinux in a compatible installed distribution.</summary>
public sealed class DisableSelinuxServerAction : ServerAction
{
    public DisableSelinuxServerAction() : base("disable_selinux") { }
}

/// <summary>Enables automatic backups.</summary>
public sealed class EnableBackupsServerAction : ServerAction
{
    public EnableBackupsServerAction() : base("enable_backups") { }
}

/// <summary>Enables IPv6 on a server.</summary>
public sealed class EnableIpv6ServerAction : ServerAction
{
    public EnableIpv6ServerAction() : base("enable_ipv6") { }
}

/// <summary>Checks whether a server is running.</summary>
public sealed class IsRunningServerAction : ServerAction
{
    public IsRunningServerAction() : base("is_running") { }
}

/// <summary>Resets a server password.</summary>
public sealed class PasswordResetServerAction : ServerAction
{
    public PasswordResetServerAction() : base("password_reset") { }

    [JsonPropertyName("username")]
    public string? Username { get; init; }

    /// <remarks>Do not log this value.</remarks>
    [JsonPropertyName("password")]
    public string? Password { get; init; }
}

/// <summary>Pings a server.</summary>
public sealed class PingServerAction : ServerAction
{
    public PingServerAction() : base("ping") { }
}

/// <summary>Power cycles a server.</summary>
public sealed class PowerCycleServerAction : ServerAction
{
    public PowerCycleServerAction() : base("power_cycle") { }
}

/// <summary>Powers a server off.</summary>
public sealed class PowerOffServerAction : ServerAction
{
    public PowerOffServerAction() : base("power_off") { }
}

/// <summary>Powers a server on.</summary>
public sealed class PowerOnServerAction : ServerAction
{
    public PowerOnServerAction() : base("power_on") { }
}

/// <summary>Reboots a server.</summary>
public sealed class RebootServerAction : ServerAction
{
    public RebootServerAction() : base("reboot") { }
}

/// <summary>Rebuilds a server from an image.</summary>
public sealed class RebuildServerAction : ServerAction
{
    public RebuildServerAction() : base("rebuild") { }

    /// <summary>An image ID or image slug. Omit to reuse the existing image.</summary>
    [JsonPropertyName("image")]
    public object? Image { get; init; }

    [JsonPropertyName("options")]
    public ImageOptions? Options { get; init; }
}

/// <summary>Renames a server.</summary>
public sealed class RenameServerAction : ServerAction
{
    public RenameServerAction() : base("rename") { }

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
    public ResizeServerAction() : base("resize") { }

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

/// <summary>Resizes an individual server disk.</summary>
public sealed class ResizeDiskServerAction : ServerAction
{
    public ResizeDiskServerAction() : base("resize_disk") { }

    [JsonPropertyName("disk_id")]
    public long DiskId { get; init; }

    [JsonPropertyName("size_gigabytes")]
    public int SizeGigabytes { get; init; }
}

/// <summary>Restores a server from an image.</summary>
public sealed class RestoreServerAction : ServerAction
{
    public RestoreServerAction() : base("restore") { }

    /// <summary>An image ID or image slug.</summary>
    [JsonPropertyName("image")]
    public object Image { get; init; } = string.Empty;
}

/// <summary>Requests a graceful shutdown.</summary>
public sealed class ShutdownServerAction : ServerAction
{
    public ShutdownServerAction() : base("shutdown") { }
}

/// <summary>Creates a backup of a server.</summary>
public sealed class TakeBackupServerAction : ServerAction
{
    public TakeBackupServerAction() : base("take_backup") { }

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

/// <summary>Reverses a scheduled server cancellation.</summary>
public sealed class UncancelServerAction : ServerAction
{
    public UncancelServerAction() : base("uncancel") { }
}

/// <summary>Checks server uptime.</summary>
public sealed class UptimeServerAction : ServerAction
{
    public UptimeServerAction() : base("uptime") { }
}

/// <summary>An advanced firewall rule supplied in a server-action request.</summary>
public sealed class AdvancedFirewallRuleRequest : BinaryLaneRequestModel
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
