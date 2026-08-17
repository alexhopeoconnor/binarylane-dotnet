using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BinaryLane.Api.V2.Models;

/// <summary>Enables or disables IPv6 for a server.</summary>
public sealed class ChangeIpv6ServerAction : ServerAction
{
    public ChangeIpv6ServerAction() : base(BinaryLaneValues.ServerActionType.ChangeIpv6) { }

    [JsonPropertyName("enabled")]
    public bool Enabled { get; init; }
}

/// <summary>Changes a server's IPv6 reverse nameservers.</summary>
public sealed class ChangeIpv6ReverseNameserversServerAction : ServerAction
{
    public ChangeIpv6ReverseNameserversServerAction() : base(BinaryLaneValues.ServerActionType.ChangeIpv6ReverseNameservers) { }

    [JsonPropertyName("ipv6_reverse_nameservers")]
    public IReadOnlyList<string> Ipv6ReverseNameservers { get; init; } = Array.Empty<string>();
}

/// <summary>Moves a server onto or off a VPC network.</summary>
public sealed class ChangeNetworkServerAction : ServerAction
{
    public ChangeNetworkServerAction() : base(BinaryLaneValues.ServerActionType.ChangeNetwork) { }

    [JsonPropertyName("vpc_id")]
    public long? VpcId { get; init; }
}

/// <summary>Changes an IPv4 reverse name.</summary>
public sealed class ChangeReverseNameServerAction : ServerAction
{
    public ChangeReverseNameServerAction() : base(BinaryLaneValues.ServerActionType.ChangeReverseName) { }

    [JsonPropertyName("ipv4_address")]
    public string Ipv4Address { get; init; } = string.Empty;

    [JsonPropertyName("reverse_name")]
    public string? ReverseName { get; init; }
}

/// <summary>Enables or disables a separate private network interface.</summary>
public sealed class ChangeSeparatePrivateNetworkInterfaceServerAction : ServerAction
{
    public ChangeSeparatePrivateNetworkInterfaceServerAction() : base(BinaryLaneValues.ServerActionType.ChangeSeparatePrivateNetworkInterface) { }

    [JsonPropertyName("enabled")]
    public bool Enabled { get; init; }
}

/// <summary>Changes a VPC IPv4 address on a server.</summary>
public sealed class ChangeVpcIpv4ServerAction : ServerAction
{
    public ChangeVpcIpv4ServerAction() : base(BinaryLaneValues.ServerActionType.ChangeVpcIpv4) { }

    [JsonPropertyName("current_ipv4_address")]
    public string CurrentIpv4Address { get; init; } = string.Empty;

    [JsonPropertyName("new_ipv4_address")]
    public string NewIpv4Address { get; init; } = string.Empty;
}

/// <summary>Enables IPv6 on a server.</summary>
public sealed class EnableIpv6ServerAction : ServerAction
{
    public EnableIpv6ServerAction() : base(BinaryLaneValues.ServerActionType.EnableIpv6) { }
}
