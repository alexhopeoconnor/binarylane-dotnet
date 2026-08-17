namespace BinaryLane.Api.V2.Models;

public static partial class BinaryLaneValues
{
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
}
