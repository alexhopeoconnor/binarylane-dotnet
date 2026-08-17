using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using BinaryLane.Api.V2.Http;
using BinaryLane.Api.V2.Models;
using BinaryLane.Api.V2.Pagination;

namespace BinaryLane.Api.V2.Resources;

/// <summary>Manages load balancers, their members, and forwarding rules.</summary>
public interface ILoadBalancersApi
{
    Task<Page<LoadBalancer>> ListAsync(PageRequest? page = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<LoadBalancer> ListAllAsync(PageRequest? page = null, CancellationToken cancellationToken = default);
    Task<LoadBalancer> GetAsync(long loadBalancerId, CancellationToken cancellationToken = default);
    Task<LoadBalancer> CreateAsync(CreateLoadBalancerRequest request, CancellationToken cancellationToken = default);
    Task<LoadBalancer> UpdateAsync(long loadBalancerId, UpdateLoadBalancerRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(long loadBalancerId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<LoadBalancerAvailabilityOption>> GetAvailabilityAsync(CancellationToken cancellationToken = default);
    Task AddServersAsync(long loadBalancerId, ServerIdsRequest request, CancellationToken cancellationToken = default);
    Task RemoveServersAsync(long loadBalancerId, ServerIdsRequest request, CancellationToken cancellationToken = default);
    Task AddForwardingRulesAsync(long loadBalancerId, ForwardingRulesRequest request, CancellationToken cancellationToken = default);
    Task RemoveForwardingRulesAsync(long loadBalancerId, ForwardingRulesRequest request, CancellationToken cancellationToken = default);
}

/// <inheritdoc />
public sealed class LoadBalancersApi : BinaryLaneResourceBase, ILoadBalancersApi
{
    /// <summary>Initializes the load-balancer resource.</summary>
    public LoadBalancersApi(IBinaryLaneApiExecutor executor, BinaryLaneJsonSerializerOptions json)
        : base(executor, json)
    {
    }

    /// <inheritdoc />
    public Task<Page<LoadBalancer>> ListAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetPageAsync<LoadBalancer>("v2/load_balancers", "load_balancers", page, null, cancellationToken);

    /// <inheritdoc />
    public IAsyncEnumerable<LoadBalancer> ListAllAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetAllPagesAsync<LoadBalancer>("v2/load_balancers", "load_balancers", page, null, cancellationToken);

    /// <inheritdoc />
    public Task<LoadBalancer> GetAsync(long loadBalancerId, CancellationToken cancellationToken = default) =>
        GetItemAsync<LoadBalancer>($"v2/load_balancers/{loadBalancerId}", "load_balancer", cancellationToken);

    /// <inheritdoc />
    public Task<LoadBalancer> CreateAsync(CreateLoadBalancerRequest request, CancellationToken cancellationToken = default) =>
        SendItemAsync<LoadBalancer>(HttpMethod.Post, "v2/load_balancers", request ?? throw new ArgumentNullException(nameof(request)), "load_balancer", cancellationToken);

    /// <inheritdoc />
    public Task<LoadBalancer> UpdateAsync(long loadBalancerId, UpdateLoadBalancerRequest request, CancellationToken cancellationToken = default) =>
        SendItemAsync<LoadBalancer>(HttpMethod.Put, $"v2/load_balancers/{loadBalancerId}", request ?? throw new ArgumentNullException(nameof(request)), "load_balancer", cancellationToken);

    /// <inheritdoc />
    public Task DeleteAsync(long loadBalancerId, CancellationToken cancellationToken = default) =>
        Executor.DeleteAsync($"v2/load_balancers/{loadBalancerId}", null, null, cancellationToken);

    /// <inheritdoc />
    public Task<IReadOnlyList<LoadBalancerAvailabilityOption>> GetAvailabilityAsync(CancellationToken cancellationToken = default) =>
        GetItemAsync<IReadOnlyList<LoadBalancerAvailabilityOption>>(
            "v2/load_balancers/availability",
            "load_balancer_availability_options",
            cancellationToken);

    /// <inheritdoc />
    public Task AddServersAsync(long loadBalancerId, ServerIdsRequest request, CancellationToken cancellationToken = default) =>
        SendNoContentAsync(HttpMethod.Post, $"v2/load_balancers/{loadBalancerId}/servers", request ?? throw new ArgumentNullException(nameof(request)), null, cancellationToken);

    /// <inheritdoc />
    public Task RemoveServersAsync(long loadBalancerId, ServerIdsRequest request, CancellationToken cancellationToken = default) =>
        Executor.DeleteAsync($"v2/load_balancers/{loadBalancerId}/servers", request ?? throw new ArgumentNullException(nameof(request)), null, cancellationToken);

    /// <inheritdoc />
    public Task AddForwardingRulesAsync(long loadBalancerId, ForwardingRulesRequest request, CancellationToken cancellationToken = default) =>
        SendNoContentAsync(HttpMethod.Post, $"v2/load_balancers/{loadBalancerId}/forwarding_rules", request ?? throw new ArgumentNullException(nameof(request)), null, cancellationToken);

    /// <inheritdoc />
    public Task RemoveForwardingRulesAsync(long loadBalancerId, ForwardingRulesRequest request, CancellationToken cancellationToken = default) =>
        Executor.DeleteAsync($"v2/load_balancers/{loadBalancerId}/forwarding_rules", request ?? throw new ArgumentNullException(nameof(request)), null, cancellationToken);
}
