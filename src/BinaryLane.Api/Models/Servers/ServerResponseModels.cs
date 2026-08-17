using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BinaryLane.Api.V2.Models;

public sealed class AdvancedFirewallRulesResponse : BinaryLaneDto
{
    [JsonPropertyName("firewall_rules")]
    public IReadOnlyList<AdvancedFirewallRule> FirewallRules { get; init; } = Array.Empty<AdvancedFirewallRule>();
}

public sealed class AvailableAdvancedServerFeaturesResponse : BinaryLaneDto
{
    [JsonPropertyName("available_advanced_server_features")]
    public AvailableAdvancedServerFeatures AvailableAdvancedServerFeatures { get; init; } = new();
}

public sealed class BackupsResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("backups")]
    public IReadOnlyList<Image> Backups { get; init; } = Array.Empty<Image>();
}

public sealed class KernelsResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("kernels")]
    public IReadOnlyList<Kernel> Kernels { get; init; } = Array.Empty<Kernel>();
}

public sealed class SnapshotsResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("snapshots")]
    public IReadOnlyList<Image> Snapshots { get; init; } = Array.Empty<Image>();
}

public sealed class ThresholdAlertsResponse : BinaryLaneDto
{
    [JsonPropertyName("threshold_alerts")]
    public IReadOnlyList<ThresholdAlert> ThresholdAlerts { get; init; } = Array.Empty<ThresholdAlert>();
}

public sealed class CurrentServerAlertsResponse : BinaryLaneDto
{
    [JsonPropertyName("server_ids")]
    public IReadOnlyList<long> ServerIds { get; init; } = Array.Empty<long>();
}

public sealed class LicensedSoftwaresResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("licensed_software")]
    public IReadOnlyList<LicensedSoftware> LicensedSoftware { get; init; } = Array.Empty<LicensedSoftware>();
}

public sealed class ServerConsoleResponse : BinaryLaneDto
{
    [JsonPropertyName("console")]
    public ServerConsole Console { get; init; } = new();
}
