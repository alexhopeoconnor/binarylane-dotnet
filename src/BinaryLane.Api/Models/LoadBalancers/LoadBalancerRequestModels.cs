using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BinaryLane.Api.V2.Models;

/// <summary>Creates a new load balancer.</summary>
public sealed class CreateLoadBalancerRequest : BinaryLaneRequestModel
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("forwarding_rules")]
    public IReadOnlyList<ForwardingRuleRequest>? ForwardingRules { get; init; }

    [JsonPropertyName("health_check")]
    public HealthCheckRequest? HealthCheck { get; init; }

    [JsonPropertyName("server_ids")]
    public IReadOnlyList<long>? ServerIds { get; init; }

    [JsonPropertyName("region")]
    public string? Region { get; init; }
}

/// <summary>Updates a load balancer.</summary>
public sealed class UpdateLoadBalancerRequest : BinaryLaneRequestModel
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("forwarding_rules")]
    public IReadOnlyList<ForwardingRuleRequest>? ForwardingRules { get; init; }

    [JsonPropertyName("health_check")]
    public HealthCheckRequest? HealthCheck { get; init; }

    [JsonPropertyName("server_ids")]
    public IReadOnlyList<long>? ServerIds { get; init; }
}

/// <summary>A load-balancer forwarding rule supplied in a request.</summary>
public sealed class ForwardingRuleRequest : BinaryLaneRequestModel
{
    /// <summary>Either <c>http</c> or <c>https</c>.</summary>
    [JsonPropertyName("entry_protocol")]
    public string EntryProtocol { get; init; } = string.Empty;
}

/// <summary>A collection of load-balancer forwarding rules.</summary>
public sealed class ForwardingRulesRequest : BinaryLaneRequestModel
{
    [JsonPropertyName("forwarding_rules")]
    public IReadOnlyList<ForwardingRuleRequest> ForwardingRules { get; init; } = Array.Empty<ForwardingRuleRequest>();
}

/// <summary>A load-balancer health check supplied in a request.</summary>
public sealed class HealthCheckRequest : BinaryLaneRequestModel
{
    /// <summary>One of <c>http</c>, <c>https</c>, or <c>both</c>.</summary>
    [JsonPropertyName("protocol")]
    public string? Protocol { get; init; }

    [JsonPropertyName("path")]
    public string? Path { get; init; }
}

/// <summary>A collection of server IDs supplied to a load-balancer operation.</summary>
public sealed class ServerIdsRequest : BinaryLaneRequestModel
{
    [JsonPropertyName("server_ids")]
    public IReadOnlyList<long> ServerIds { get; init; } = Array.Empty<long>();
}
