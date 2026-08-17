using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BinaryLane.Api.V2.Models;

/// <summary>Changes mutable metadata on an image.</summary>
public sealed class ImageRequest : BinaryLaneRequestModel
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("locked")]
    public bool? Locked { get; init; }
}

/// <summary>Uploads an image into a backup slot.</summary>
public sealed class UploadImageRequest : BinaryLaneRequestModel
{
    /// <summary>Provider backup slot, required unless the replacement strategy is <c>specified</c>.</summary>
    [JsonPropertyName("backup_type")]
    public string? BackupType { get; init; }

    /// <summary>Provider replacement strategy: <c>none</c>, <c>specified</c>, <c>oldest</c>, or <c>newest</c>.</summary>
    [JsonPropertyName("replacement_strategy")]
    public string ReplacementStrategy { get; init; } = string.Empty;

    [JsonPropertyName("backup_id_to_replace")]
    public long? BackupIdToReplace { get; init; }

    [JsonPropertyName("label")]
    public string? Label { get; init; }

    [JsonPropertyName("url")]
    public string Url { get; init; } = string.Empty;
}
