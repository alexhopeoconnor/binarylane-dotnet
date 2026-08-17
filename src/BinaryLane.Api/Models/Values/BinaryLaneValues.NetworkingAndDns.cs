namespace BinaryLane.Api.V2.Models;

public static partial class BinaryLaneValues
{
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
}
