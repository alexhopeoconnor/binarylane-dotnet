namespace BinaryLane.Api.V2.Models;

public static partial class BinaryLaneValues
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

    public static class ResourceType
    {
        public const string Server = "server";
        public const string LoadBalancer = "load-balancer";
        public const string SshKey = "ssh-key";
        public const string Vpc = "vpc";
        public const string Image = "image";
        public const string RegisteredDomainName = "registered-domain-name";
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
