using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BinaryLane.Api.V2.Models;

/// <summary>Changes advanced virtual-machine features.</summary>
public sealed class ChangeAdvancedFeaturesServerAction : ServerAction
{
    public ChangeAdvancedFeaturesServerAction() : base(BinaryLaneValues.ServerActionType.ChangeAdvancedFeatures) { }

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
    public ChangeAdvancedFirewallRulesServerAction() : base(BinaryLaneValues.ServerActionType.ChangeAdvancedFirewallRules) { }

    [JsonPropertyName("firewall_rules")]
    public IReadOnlyList<AdvancedFirewallRuleRequest> FirewallRules { get; init; } = Array.Empty<AdvancedFirewallRuleRequest>();
}

/// <summary>Enables or disables port blocking.</summary>
public sealed class ChangePortBlockingServerAction : ServerAction
{
    public ChangePortBlockingServerAction() : base(BinaryLaneValues.ServerActionType.ChangePortBlocking) { }

    [JsonPropertyName("enabled")]
    public bool Enabled { get; init; }
}

/// <summary>Moves a server to a new region.</summary>
public sealed class ChangeRegionServerAction : ServerAction
{
    public ChangeRegionServerAction() : base(BinaryLaneValues.ServerActionType.ChangeRegion) { }

    [JsonPropertyName("region")]
    public string Region { get; init; } = string.Empty;
}

/// <summary>Changes a server's partner server relationship.</summary>
public sealed class ChangePartnerServerAction : ServerAction
{
    public ChangePartnerServerAction() : base(BinaryLaneValues.ServerActionType.ChangePartner) { }

    [JsonPropertyName("partner_server_id")]
    public long? PartnerServerId { get; init; }
}

/// <summary>Enables or disables source-and-destination checking.</summary>
public sealed class ChangeSourceAndDestinationCheckServerAction : ServerAction
{
    public ChangeSourceAndDestinationCheckServerAction() : base(BinaryLaneValues.ServerActionType.ChangeSourceAndDestinationCheck) { }

    [JsonPropertyName("enabled")]
    public bool Enabled { get; init; }
}

/// <summary>Replaces a server's threshold-alert configuration.</summary>
public sealed class ChangeThresholdAlertsServerAction : ServerAction
{
    public ChangeThresholdAlertsServerAction() : base(BinaryLaneValues.ServerActionType.ChangeThresholdAlerts) { }

    [JsonPropertyName("threshold_alerts")]
    public IReadOnlyList<ThresholdAlertRequest> ThresholdAlerts { get; init; } = Array.Empty<ThresholdAlertRequest>();
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
