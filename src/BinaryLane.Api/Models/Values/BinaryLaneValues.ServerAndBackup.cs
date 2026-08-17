namespace BinaryLane.Api.V2.Models;

public static partial class BinaryLaneValues
{
    public static class ServerStatus
    {
        public const string New = "new";
        public const string Active = "active";
        public const string Archive = "archive";

        public const string ProviderFalse = "False";
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

    public static class PasswordRecoveryType
    {
        public const string Manual = "manual";
        public const string OfflineClear = "offline-clear";
        public const string OfflineChange = "offline-change";
        public const string OnlineChange = "online-change";
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
}
