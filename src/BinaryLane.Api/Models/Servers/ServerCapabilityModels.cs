using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BinaryLane.Api.V2.Models;

/// <summary>Advanced virtual-machine features configured for a server.</summary>
public sealed class AdvancedServerFeatures : BinaryLaneDto
{
    [JsonPropertyName("processor_model")]
    public long? ProcessorModel { get; init; }

    /// <summary>Provider virtual machine type.</summary>
    [JsonPropertyName("machine_type")]
    public string? MachineType { get; init; }

    /// <summary>Provider virtual video device.</summary>
    [JsonPropertyName("video_device")]
    public string VideoDevice { get; init; } = string.Empty;

    /// <summary>Provider advanced-feature values.</summary>
    [JsonPropertyName("enabled_advanced_features")]
    public IReadOnlyList<string> EnabledAdvancedFeatures { get; init; } = Array.Empty<string>();
}

/// <summary>Advanced server features currently available to the account.</summary>
public sealed class AvailableAdvancedServerFeatures : BinaryLaneDto
{
    [JsonPropertyName("processor_models")]
    public IReadOnlyList<ProcessorModel> ProcessorModels { get; init; } = Array.Empty<ProcessorModel>();

    [JsonPropertyName("machine_types")]
    public IReadOnlyList<string> MachineTypes { get; init; } = Array.Empty<string>();

    [JsonPropertyName("advanced_features")]
    public IReadOnlyList<string> AdvancedFeatures { get; init; } = Array.Empty<string>();
}

/// <summary>An available processor model.</summary>
public sealed class ProcessorModel : BinaryLaneDto
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; init; }
}

/// <summary>An advanced firewall rule applied to a server.</summary>
public sealed class AdvancedFirewallRule : BinaryLaneDto
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

/// <summary>An available server kernel.</summary>
public sealed class Kernel : BinaryLaneDto
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("version")]
    public string? Version { get; init; }
}

/// <summary>A configured server threshold alert.</summary>
public sealed class ThresholdAlert : BinaryLaneDto
{
    /// <summary>Provider alert type.</summary>
    [JsonPropertyName("alert_type")]
    public string AlertType { get; init; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("unit")]
    public string Unit { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; init; } = string.Empty;

    [JsonPropertyName("enabled")]
    public bool Enabled { get; init; }

    [JsonPropertyName("value")]
    public int Value { get; init; }

    [JsonPropertyName("current_value")]
    public int? CurrentValue { get; init; }

    [JsonPropertyName("last_raised")]
    public DateTimeOffset? LastRaised { get; init; }

    [JsonPropertyName("last_cleared")]
    public DateTimeOffset? LastCleared { get; init; }
}

/// <summary>Software licensed on a server.</summary>
public sealed class LicensedSoftware : BinaryLaneDto
{
    [JsonPropertyName("software")]
    public Software Software { get; init; } = new();

    [JsonPropertyName("licence_count")]
    public int LicenceCount { get; init; }

    [JsonPropertyName("incompatible")]
    public bool Incompatible { get; init; }
}

/// <summary>A browser console session for a server.</summary>
public sealed class ServerConsole : BinaryLaneDto
{
    [JsonPropertyName("iframe")]
    public string Iframe { get; init; } = string.Empty;

    [JsonPropertyName("browser")]
    public string Browser { get; init; } = string.Empty;

    [JsonPropertyName("width")]
    public int Width { get; init; }

    [JsonPropertyName("height")]
    public int Height { get; init; }

    [JsonPropertyName("expiry")]
    public DateTimeOffset Expiry { get; init; }
}

/// <summary>Server user-data retained by BinaryLane.</summary>
public sealed class UserData : BinaryLaneDto
{
    [JsonPropertyName("user_data")]
    public string? Value { get; init; }
}
