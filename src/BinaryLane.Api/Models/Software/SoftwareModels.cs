using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BinaryLane.Api.V2.Models;

/// <summary>A software product that can be licensed on BinaryLane servers.</summary>
public sealed class Software : BinaryLaneDto
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("enabled")]
    public bool Enabled { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; init; } = string.Empty;

    [JsonPropertyName("cost_per_licence_per_month")]
    public double CostPerLicencePerMonth { get; init; }

    [JsonPropertyName("minimum_licence_count")]
    public int MinimumLicenceCount { get; init; }

    [JsonPropertyName("maximum_licence_count")]
    public int MaximumLicenceCount { get; init; }

    [JsonPropertyName("licence_step_count")]
    public int LicenceStepCount { get; init; }

    [JsonPropertyName("group")]
    public string? Group { get; init; }

    [JsonPropertyName("supported_operating_systems")]
    public IReadOnlyList<string> SupportedOperatingSystems { get; init; } = Array.Empty<string>();
}

public sealed class SoftwareResponse : BinaryLaneDto
{
    [JsonPropertyName("software")]
    public Software Software { get; init; } = new();
}

public sealed class SoftwaresResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("software")]
    public IReadOnlyList<Software> Software { get; init; } = Array.Empty<Software>();
}
