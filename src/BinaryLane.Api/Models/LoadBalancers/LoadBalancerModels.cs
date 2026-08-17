using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BinaryLane.Api.V2.Models;

/// <summary>A BinaryLane load balancer.</summary>
public sealed class LoadBalancer : BinaryLaneDto
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("ip")]
    public string Ip { get; init; } = string.Empty;

    /// <summary>Provider load balancer status, such as <c>active</c>.</summary>
    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; init; }

    [JsonPropertyName("forwarding_rules")]
    public IReadOnlyList<ForwardingRule> ForwardingRules { get; init; } = Array.Empty<ForwardingRule>();

    [JsonPropertyName("health_check")]
    public HealthCheck HealthCheck { get; init; } = new();

    [JsonPropertyName("region")]
    public Region? Region { get; init; }

    [JsonPropertyName("server_ids")]
    public IReadOnlyList<long> ServerIds { get; init; } = Array.Empty<long>();
}

/// <summary>A load-balancer forwarding rule.</summary>
public sealed class ForwardingRule : BinaryLaneDto
{
    /// <summary>Provider rule protocol, either <c>http</c> or <c>https</c>.</summary>
    [JsonPropertyName("entry_protocol")]
    public string EntryProtocol { get; init; } = string.Empty;
}

/// <summary>A load-balancer health check.</summary>
public sealed class HealthCheck : BinaryLaneDto
{
    /// <summary>Provider health check protocol, such as <c>http</c>, <c>https</c>, or <c>both</c>.</summary>
    [JsonPropertyName("protocol")]
    public string Protocol { get; init; } = string.Empty;

    [JsonPropertyName("path")]
    public string Path { get; init; } = string.Empty;
}

/// <summary>A load-balancer configuration option available to the account.</summary>
public sealed class LoadBalancerAvailabilityOption : BinaryLaneDto
{
    [JsonPropertyName("regions")]
    public IReadOnlyList<string>? Regions { get; init; }

    [JsonPropertyName("anycast")]
    public bool Anycast { get; init; }

    [JsonPropertyName("price_monthly")]
    public double PriceMonthly { get; init; }

    [JsonPropertyName("price_hourly")]
    public double PriceHourly { get; init; }
}

public sealed class LoadBalancerResponse : BinaryLaneDto
{
    [JsonPropertyName("load_balancer")]
    public LoadBalancer LoadBalancer { get; init; } = new();
}

public sealed class LoadBalancersResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("load_balancers")]
    public IReadOnlyList<LoadBalancer> LoadBalancers { get; init; } = Array.Empty<LoadBalancer>();
}

public sealed class CreateLoadBalancerResponse : BinaryLaneDto
{
    [JsonPropertyName("load_balancer")]
    public LoadBalancer LoadBalancer { get; init; } = new();

    [JsonPropertyName("links")]
    public ActionsLinks Links { get; init; } = new();
}

public sealed class UpdateLoadBalancerResponse : BinaryLaneDto
{
    [JsonPropertyName("load_balancer")]
    public LoadBalancer LoadBalancer { get; init; } = new();

    [JsonPropertyName("links")]
    public ActionsLinks? Links { get; init; }
}

public sealed class LoadBalancerAvailabilityResponse : BinaryLaneDto
{
    [JsonPropertyName("load_balancer_availability_options")]
    public IReadOnlyList<LoadBalancerAvailabilityOption> LoadBalancerAvailabilityOptions { get; init; } = Array.Empty<LoadBalancerAvailabilityOption>();
}
