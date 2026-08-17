using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BinaryLane.Api.V2.Models;

/// <summary>Creates an SSH key.</summary>
public sealed class SshKeyRequest : BinaryLaneRequestModel
{
    [JsonPropertyName("public_key")]
    public string PublicKey { get; init; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("default")]
    public bool? IsDefault { get; init; }
}

/// <summary>Updates an SSH key.</summary>
public sealed class UpdateSshKeyRequest : BinaryLaneRequestModel
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("default")]
    public bool? IsDefault { get; init; }
}
