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

/// <summary>Manages private virtual networks and their members.</summary>
public interface IVpcsApi
{
    Task<Page<Vpc>> ListAsync(PageRequest? page = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<Vpc> ListAllAsync(PageRequest? page = null, CancellationToken cancellationToken = default);
    Task<Vpc> GetAsync(long vpcId, CancellationToken cancellationToken = default);
    Task<Vpc> CreateAsync(CreateVpcRequest request, CancellationToken cancellationToken = default);
    Task<Vpc> ReplaceAsync(long vpcId, UpdateVpcRequest request, CancellationToken cancellationToken = default);
    Task<Vpc> UpdateAsync(long vpcId, PatchVpcRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(long vpcId, CancellationToken cancellationToken = default);
    Task<Page<VpcMember>> ListMembersAsync(long vpcId, PageRequest? page = null, CancellationToken cancellationToken = default);
}

/// <inheritdoc />
public sealed class VpcsApi : BinaryLaneResourceBase, IVpcsApi
{
    /// <summary>Initializes the VPC resource.</summary>
    public VpcsApi(IBinaryLaneApiExecutor executor, BinaryLaneJsonSerializerOptions json)
        : base(executor, json)
    {
    }

    /// <inheritdoc />
    public Task<Page<Vpc>> ListAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetPageAsync<Vpc>("v2/vpcs", "vpcs", page, null, cancellationToken);

    /// <inheritdoc />
    public IAsyncEnumerable<Vpc> ListAllAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetAllPagesAsync<Vpc>("v2/vpcs", "vpcs", page, null, cancellationToken);

    /// <inheritdoc />
    public Task<Vpc> GetAsync(long vpcId, CancellationToken cancellationToken = default) =>
        GetItemAsync<Vpc>($"v2/vpcs/{vpcId}", "vpc", cancellationToken);

    /// <inheritdoc />
    public Task<Vpc> CreateAsync(CreateVpcRequest request, CancellationToken cancellationToken = default) =>
        SendItemAsync<Vpc>(HttpMethod.Post, "v2/vpcs", request ?? throw new ArgumentNullException(nameof(request)), "vpc", cancellationToken);

    /// <inheritdoc />
    public Task<Vpc> ReplaceAsync(long vpcId, UpdateVpcRequest request, CancellationToken cancellationToken = default) =>
        SendItemAsync<Vpc>(HttpMethod.Put, $"v2/vpcs/{vpcId}", request ?? throw new ArgumentNullException(nameof(request)), "vpc", cancellationToken);

    /// <inheritdoc />
    public Task<Vpc> UpdateAsync(long vpcId, PatchVpcRequest request, CancellationToken cancellationToken = default) =>
        SendItemAsync<Vpc>(BinaryLaneHttpMethods.Patch, $"v2/vpcs/{vpcId}", request ?? throw new ArgumentNullException(nameof(request)), "vpc", cancellationToken);

    /// <inheritdoc />
    public Task DeleteAsync(long vpcId, CancellationToken cancellationToken = default) =>
        Executor.DeleteAsync($"v2/vpcs/{vpcId}", null, null, cancellationToken);

    /// <inheritdoc />
    public Task<Page<VpcMember>> ListMembersAsync(long vpcId, PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetPageAsync<VpcMember>($"v2/vpcs/{vpcId}/members", "members", page, null, cancellationToken);
}
