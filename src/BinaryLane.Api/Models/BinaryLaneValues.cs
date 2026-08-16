namespace BinaryLane.Api.V2.Models;

/// <summary>
/// Known string values in the BinaryLane v2 contract.
/// </summary>
/// <remarks>
/// The API remains a developer preview. Public DTOs use strings rather than
/// closed C# enums so an upstream value added after an SDK release remains
/// readable. These constants make documented values discoverable without
/// removing that forward compatibility.
/// </remarks>
public static class BinaryLaneValues
{
    public static class AccountStatus
    {
        public const string Incomplete = "incomplete";
        public const string Active = "active";
        public const string Warning = "warning";
        public const string Locked = "locked";
    }

    public static class ActionStatus
    {
        public const string InProgress = "in-progress";
        public const string Completed = "completed";
        public const string Errored = "errored";
    }

    public static class ServerStatus
    {
        public const string New = "new";
        public const string Active = "active";
        public const string Archive = "archive";

        // This unusual provider value is present in the 0.39.1 OpenAPI contract.
        public const string ProviderFalse = "False";
    }

    public static class ResourceType
    {
        public const string Server = "server";
        public const string LoadBalancer = "load-balancer";
        public const string SshKey = "ssh-key";
        public const string Vpc = "vpc";
        public const string Image = "image";
        public const string RegisteredDomainName = "registered-domain-name";
    }

    public static class ImageType
    {
        public const string Custom = "custom";
        public const string Snapshot = "snapshot";
        public const string Backup = "backup";
    }

    public static class ImageStatus
    {
        public const string New = "NEW";
        public const string Available = "available";
        public const string Pending = "pending";
        public const string Deleted = "deleted";
    }

    public static class ImageQueryType
    {
        public const string Distribution = "distribution";
        public const string Backup = "backup";
    }

    public static class BackupSlot
    {
        public const string Daily = "daily";
        public const string Weekly = "weekly";
        public const string Monthly = "monthly";
        public const string Temporary = "temporary";
    }

    public static class BackupReplacementStrategy
    {
        public const string None = "none";
        public const string Specified = "specified";
        public const string Oldest = "oldest";
        public const string Newest = "newest";
    }

    public static class NetworkType
    {
        public const string Private = "private";
        public const string Public = "public";
    }

    public static class DomainRecordType
    {
        public const string A = "A";
        public const string Aaaa = "AAAA";
        public const string Caa = "CAA";
        public const string Cname = "CNAME";
        public const string Mx = "MX";
        public const string Ns = "NS";
        public const string Soa = "SOA";
        public const string Srv = "SRV";
        public const string Txt = "TXT";
    }

    public static class LoadBalancerStatus
    {
        public const string New = "new";
        public const string Active = "active";
        public const string Errored = "errored";
    }

    public static class LoadBalancerRuleProtocol
    {
        public const string Http = "http";
        public const string Https = "https";
    }

    public static class HealthCheckProtocol
    {
        public const string Http = "http";
        public const string Https = "https";
        public const string Both = "both";
    }

    public static class AdvancedFirewallRuleProtocol
    {
        public const string All = "all";
        public const string Icmp = "icmp";
        public const string Tcp = "tcp";
        public const string Udp = "udp";
    }

    public static class AdvancedFirewallRuleAction
    {
        public const string Drop = "drop";
        public const string Accept = "accept";
    }

    public static class PasswordRecoveryType
    {
        public const string Manual = "manual";
        public const string OfflineClear = "offline-clear";
        public const string OfflineChange = "offline-change";
        public const string OnlineChange = "online-change";
    }

    public static class DistributionFeature
    {
        public const string Ssh = "ssh";
        public const string RemoteDesktop = "remote-desktop";
        public const string UserData = "user-data";
    }

    public static class DataInterval
    {
        public const string FiveMinute = "five-minute";
        public const string HalfHour = "half-hour";
        public const string FourHour = "four-hour";
        public const string Day = "day";
        public const string Week = "week";
        public const string Month = "month";
    }

    public static class ThresholdAlertType
    {
        public const string Cpu = "cpu";
        public const string StorageRequests = "storage-requests";
        public const string NetworkIncoming = "network-incoming";
        public const string NetworkOutgoing = "network-outgoing";
        public const string DataTransferUsed = "data-transfer-used";
        public const string StorageUsed = "storage-used";
        public const string MemoryUsed = "memory-used";
        public const string LockedBackupSlots = "locked-backup-slots";
    }

    public static class PaymentMethod
    {
        public const string CreditCard = "credit-card";
        public const string Paypal = "paypal";
    }

    public static class TaxCodeType
    {
        public const string None = "none";
        public const string Scalar = "scalar";
    }

    public static class UserInteractionType
    {
        public const string ContinueAfterPingFailure = "continue-after-ping-failure";
        public const string AllowUncleanPowerOff = "allow-unclean-power-off";
    }

    public static class AdvancedFeature
    {
        public const string EmulatedHyperV = "emulated-hyperv";
        public const string EmulatedDevices = "emulated-devices";
        public const string NestedVirt = "nested-virt";
        public const string DriverDisk = "driver-disk";
        public const string UnsetUuid = "unset-uuid";
        public const string LocalRtc = "local-rtc";
        public const string EmulatedTpm = "emulated-tpm";
        public const string CloudInit = "cloud-init";
        public const string QemuGuestAgent = "qemu-guest-agent";
        public const string UefiBoot = "uefi-boot";
    }

    public static class VideoDevice
    {
        public const string CirrusLogic = "cirrus-logic";
        public const string Standard = "standard";
        public const string Virtio = "virtio";
        public const string VirtioWide = "virtio-wide";
    }

    public static class VmMachineType
    {
        public const string PcI440Fx1Point5 = "pc_i440fx_1point5";
        public const string PcI440Fx2Point11 = "pc_i440fx_2point11";
        public const string PcI440Fx4Point1 = "pc_i440fx_4point1";
        public const string PcI440Fx4Point2 = "pc_i440fx_4point2";
        public const string PcI440Fx5Point0 = "pc_i440fx_5point0";
        public const string PcI440Fx5Point1 = "pc_i440fx_5point1";
        public const string PcI440Fx7Point2 = "pc_i440fx_7point2";
        public const string PcI440Fx7Point2Point1 = "pc_i440fx_7point2point1";
        public const string PcI440Fx8Point2 = "pc_i440fx_8point2";
    }

    /// <summary>Discriminator values for <see cref="ServerAction"/> payloads.</summary>
    public static class ServerActionType
    {
        public const string AddDisk = "add_disk";
        public const string AttachBackup = "attach_backup";
        public const string ChangeAdvancedFeatures = "change_advanced_features";
        public const string ChangeAdvancedFirewallRules = "change_advanced_firewall_rules";
        public const string ChangeBackupSchedule = "change_backup_schedule";
        public const string ChangeIpv6 = "change_ipv6";
        public const string ChangeIpv6ReverseNameservers = "change_ipv6_reverse_nameservers";
        public const string ChangeKernel = "change_kernel";
        public const string ChangeManageOffsiteBackupCopies = "change_manage_offsite_backup_copies";
        public const string ChangeNetwork = "change_network";
        public const string ChangeOffsiteBackupLocation = "change_offsite_backup_location";
        public const string ChangePartner = "change_partner";
        public const string ChangePortBlocking = "change_port_blocking";
        public const string ChangeRegion = "change_region";
        public const string ChangeReverseName = "change_reverse_name";
        public const string ChangeSeparatePrivateNetworkInterface = "change_separate_private_network_interface";
        public const string ChangeSourceAndDestinationCheck = "change_source_and_destination_check";
        public const string ChangeThresholdAlerts = "change_threshold_alerts";
        public const string ChangeVpcIpv4 = "change_vpc_ipv4";
        public const string CloneUsingBackup = "clone_using_backup";
        public const string DeleteDisk = "delete_disk";
        public const string DetachBackup = "detach_backup";
        public const string DisableBackups = "disable_backups";
        public const string DisableSelinux = "disable_selinux";
        public const string EnableBackups = "enable_backups";
        public const string EnableIpv6 = "enable_ipv6";
        public const string IsRunning = "is_running";
        public const string PasswordReset = "password_reset";
        public const string Ping = "ping";
        public const string PowerCycle = "power_cycle";
        public const string PowerOff = "power_off";
        public const string PowerOn = "power_on";
        public const string Reboot = "reboot";
        public const string Rebuild = "rebuild";
        public const string Rename = "rename";
        public const string Resize = "resize";
        public const string ResizeDisk = "resize_disk";
        public const string Restore = "restore";
        public const string Shutdown = "shutdown";
        public const string TakeBackup = "take_backup";
        public const string Uncancel = "uncancel";
        public const string Uptime = "uptime";
    }
}
