using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BinaryLane.Api.V2.Models;

/// <summary>Current data-transfer use for a server.</summary>
public sealed class DataUsage : BinaryLaneDto
{
    [JsonPropertyName("server_id")]
    public long ServerId { get; init; }

    [JsonPropertyName("expires")]
    public DateTimeOffset Expires { get; init; }

    [JsonPropertyName("transfer_gigabytes")]
    public long TransferGigabytes { get; init; }

    [JsonPropertyName("current_transfer_usage_gigabytes")]
    public double CurrentTransferUsageGigabytes { get; init; }

    [JsonPropertyName("transfer_period_end")]
    public DateTimeOffset TransferPeriodEnd { get; init; }
}

/// <summary>A sample interval for server monitoring data.</summary>
public sealed class Period : BinaryLaneDto
{
    [JsonPropertyName("start")]
    public DateTimeOffset Start { get; init; }

    [JsonPropertyName("end")]
    public DateTimeOffset End { get; init; }

    /// <summary>Provider interval, such as <c>five-minute</c> or <c>day</c>.</summary>
    [JsonPropertyName("data_interval")]
    public string DataInterval { get; init; } = string.Empty;
}

/// <summary>One monitoring aggregate sample.</summary>
public sealed class Sample : BinaryLaneDto
{
    [JsonPropertyName("cpu_usage_percent")]
    public double CpuUsagePercent { get; init; }

    [JsonPropertyName("cpu_usage_detailed")]
    public IReadOnlyList<double> CpuUsageDetailed { get; init; } = Array.Empty<double>();

    [JsonPropertyName("memory_usage_bytes")]
    public double MemoryUsageBytes { get; init; }

    [JsonPropertyName("network_incoming_kbps")]
    public double NetworkIncomingKbps { get; init; }

    [JsonPropertyName("network_outgoing_kbps")]
    public double NetworkOutgoingKbps { get; init; }

    [JsonPropertyName("storage_usage_megabytes")]
    public double StorageUsageMegabytes { get; init; }

    [JsonPropertyName("storage_read_kbps")]
    public double StorageReadKbps { get; init; }

    [JsonPropertyName("storage_write_kbps")]
    public double StorageWriteKbps { get; init; }

    [JsonPropertyName("storage_read_requests_per_second")]
    public double StorageReadRequestsPerSecond { get; init; }

    [JsonPropertyName("storage_write_requests_per_second")]
    public double StorageWriteRequestsPerSecond { get; init; }
}

/// <summary>A set of monitoring samples for a server.</summary>
public sealed class SampleSet : BinaryLaneDto
{
    [JsonPropertyName("server_id")]
    public long ServerId { get; init; }

    [JsonPropertyName("period")]
    public Period Period { get; init; } = new();

    [JsonPropertyName("average")]
    public Sample Average { get; init; } = new();

    [JsonPropertyName("maximum_memory_megabytes")]
    public double MaximumMemoryMegabytes { get; init; }

    [JsonPropertyName("maximum_storage_gigabytes")]
    public double MaximumStorageGigabytes { get; init; }
}

public sealed class DataUsageResponse : BinaryLaneDto
{
    [JsonPropertyName("data_usage")]
    public DataUsage DataUsage { get; init; } = new();
}

public sealed class DataUsagesResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("data_usages")]
    public IReadOnlyList<DataUsage> DataUsages { get; init; } = Array.Empty<DataUsage>();
}

public sealed class SampleSetResponse : BinaryLaneDto
{
    [JsonPropertyName("sample_set")]
    public SampleSet? SampleSet { get; init; }
}

public sealed class SampleSetsResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("sample_sets")]
    public IReadOnlyList<SampleSet> SampleSets { get; init; } = Array.Empty<SampleSet>();
}
