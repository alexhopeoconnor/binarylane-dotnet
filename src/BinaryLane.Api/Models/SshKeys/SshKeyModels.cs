using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BinaryLane.Api.V2.Models;

/// <summary>An SSH public key owned by the authenticated account.</summary>
public sealed class SshKey : BinaryLaneDto
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("fingerprint")]
    public string Fingerprint { get; init; } = string.Empty;

    [JsonPropertyName("public_key")]
    public string PublicKey { get; init; } = string.Empty;

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("default")]
    public bool IsDefault { get; init; }
}

public sealed class SshKeyResponse : BinaryLaneDto
{
    [JsonPropertyName("ssh_key")]
    public SshKey SshKey { get; init; } = new();
}

public sealed class SshKeysResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("ssh_keys")]
    public IReadOnlyList<SshKey> SshKeys { get; init; } = Array.Empty<SshKey>();
}
