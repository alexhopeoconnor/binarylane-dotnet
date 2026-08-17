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

/// <summary>Manages account SSH keys.</summary>
public interface ISshKeysApi
{
    Task<Page<SshKey>> ListAsync(PageRequest? page = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<SshKey> ListAllAsync(PageRequest? page = null, CancellationToken cancellationToken = default);
    Task<SshKey> GetAsync(long keyId, CancellationToken cancellationToken = default);
    Task<SshKey> CreateAsync(SshKeyRequest request, CancellationToken cancellationToken = default);
    Task<SshKey> UpdateAsync(long keyId, UpdateSshKeyRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(long keyId, CancellationToken cancellationToken = default);
}

/// <inheritdoc />
public sealed class SshKeysApi : BinaryLaneResourceBase, ISshKeysApi
{
    /// <summary>Initializes the SSH-key resource.</summary>
    public SshKeysApi(IBinaryLaneApiExecutor executor, BinaryLaneJsonSerializerOptions json)
        : base(executor, json)
    {
    }

    /// <inheritdoc />
    public Task<Page<SshKey>> ListAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetPageAsync<SshKey>("v2/account/keys", "ssh_keys", page, null, cancellationToken);

    /// <inheritdoc />
    public IAsyncEnumerable<SshKey> ListAllAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetAllPagesAsync<SshKey>("v2/account/keys", "ssh_keys", page, null, cancellationToken);

    /// <inheritdoc />
    public Task<SshKey> GetAsync(long keyId, CancellationToken cancellationToken = default) =>
        GetItemAsync<SshKey>($"v2/account/keys/{keyId}", "ssh_key", cancellationToken);

    /// <inheritdoc />
    public Task<SshKey> CreateAsync(SshKeyRequest request, CancellationToken cancellationToken = default) =>
        SendItemAsync<SshKey>(HttpMethod.Post, "v2/account/keys", request ?? throw new ArgumentNullException(nameof(request)), "ssh_key", cancellationToken);

    /// <inheritdoc />
    public Task<SshKey> UpdateAsync(long keyId, UpdateSshKeyRequest request, CancellationToken cancellationToken = default) =>
        SendItemAsync<SshKey>(HttpMethod.Put, $"v2/account/keys/{keyId}", request ?? throw new ArgumentNullException(nameof(request)), "ssh_key", cancellationToken);

    /// <inheritdoc />
    public Task DeleteAsync(long keyId, CancellationToken cancellationToken = default) =>
        Executor.DeleteAsync($"v2/account/keys/{keyId}", null, null, cancellationToken);
}
