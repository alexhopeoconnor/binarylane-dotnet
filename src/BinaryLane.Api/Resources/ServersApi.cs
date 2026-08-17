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

/// <summary>Manages BinaryLane servers and their server-scoped resources.</summary>
public interface IServersApi
{
    Task<Server> GetAsync(long serverId, CancellationToken cancellationToken = default);
    Task<Page<Server>> ListAsync(PageRequest? page = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<Server> ListAllAsync(PageRequest? page = null, CancellationToken cancellationToken = default);
    Task<Server> CreateAsync(CreateServerRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(long serverId, string? reason = null, CancellationToken cancellationToken = default);
    Task<Page<BinaryLaneAction>> ListActionsAsync(long serverId, PageRequest? page = null, CancellationToken cancellationToken = default);
    Task<BinaryLaneAction> GetActionAsync(long serverId, long actionId, CancellationToken cancellationToken = default);
    Task<ActionSubmission> SubmitActionAsync(long serverId, ServerAction request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<AdvancedFirewallRule>> GetAdvancedFirewallRulesAsync(long serverId, CancellationToken cancellationToken = default);
    Task<AvailableAdvancedServerFeatures> GetAvailableAdvancedFeaturesAsync(long serverId, CancellationToken cancellationToken = default);
    Task<Page<Image>> ListBackupsAsync(long serverId, PageRequest? page = null, CancellationToken cancellationToken = default);
    Task<BinaryLaneAction> CreateBackupAsync(long serverId, UploadImageRequest request, CancellationToken cancellationToken = default);
    Task<Page<Kernel>> ListKernelsAsync(long serverId, PageRequest? page = null, CancellationToken cancellationToken = default);
    Task<Page<Image>> ListSnapshotsAsync(long serverId, PageRequest? page = null, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ThresholdAlert>> GetThresholdAlertsAsync(long serverId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<long>> GetThresholdAlertServerIdsAsync(CancellationToken cancellationToken = default);
    Task<Page<LicensedSoftware>> ListSoftwareAsync(long serverId, PageRequest? page = null, CancellationToken cancellationToken = default);
    Task<UserData> GetUserDataAsync(long serverId, CancellationToken cancellationToken = default);
    Task<ServerConsole> GetConsoleAsync(long serverId, CancellationToken cancellationToken = default);
}

/// <inheritdoc />
public sealed class ServersApi : BinaryLaneResourceBase, IServersApi
{
    /// <summary>Initializes the servers resource.</summary>
    public ServersApi(IBinaryLaneApiExecutor executor, BinaryLaneJsonSerializerOptions json)
        : base(executor, json)
    {
    }

    /// <inheritdoc />
    public Task<Server> GetAsync(long serverId, CancellationToken cancellationToken = default) =>
        GetItemAsync<Server>($"v2/servers/{serverId}", "server", cancellationToken);

    /// <inheritdoc />
    public Task<Page<Server>> ListAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetPageAsync<Server>("v2/servers", "servers", page, null, cancellationToken);

    /// <inheritdoc />
    public IAsyncEnumerable<Server> ListAllAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetAllPagesAsync<Server>("v2/servers", "servers", page, null, cancellationToken);

    /// <inheritdoc />
    public Task<Server> CreateAsync(CreateServerRequest request, CancellationToken cancellationToken = default) =>
        SendItemAsync<Server>(HttpMethod.Post, "v2/servers", request ?? throw new ArgumentNullException(nameof(request)), "server", cancellationToken);

    /// <inheritdoc />
    public Task DeleteAsync(long serverId, string? reason = null, CancellationToken cancellationToken = default) =>
        Executor.DeleteAsync(
            $"v2/servers/{serverId}",
            null,
            string.IsNullOrWhiteSpace(reason) ? null : new Dictionary<string, object?> { ["reason"] = reason },
            cancellationToken);

    /// <inheritdoc />
    public Task<Page<BinaryLaneAction>> ListActionsAsync(long serverId, PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetPageAsync<BinaryLaneAction>($"v2/servers/{serverId}/actions", "actions", page, null, cancellationToken);

    /// <inheritdoc />
    public Task<BinaryLaneAction> GetActionAsync(long serverId, long actionId, CancellationToken cancellationToken = default) =>
        GetItemAsync<BinaryLaneAction>($"v2/servers/{serverId}/actions/{actionId}", "action", cancellationToken);

    /// <inheritdoc />
    public async Task<ActionSubmission> SubmitActionAsync(long serverId, ServerAction request, CancellationToken cancellationToken = default)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(request);
#else
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }
#endif

        var response = await SendResponseAsync(
            HttpMethod.Post,
            $"v2/servers/{serverId}/actions",
            request,
            null,
            cancellationToken).ConfigureAwait(false);

        TryDeserializeEnvelope<BinaryLaneAction>(response.Body, "action", out var action);
        return new ActionSubmission(response.StatusCode, action, response.Body);
    }

    /// <inheritdoc />
    public Task<IReadOnlyList<AdvancedFirewallRule>> GetAdvancedFirewallRulesAsync(long serverId, CancellationToken cancellationToken = default) =>
        GetItemAsync<IReadOnlyList<AdvancedFirewallRule>>(
            $"v2/servers/{serverId}/advanced_firewall_rules",
            "firewall_rules",
            cancellationToken);

    /// <inheritdoc />
    public Task<AvailableAdvancedServerFeatures> GetAvailableAdvancedFeaturesAsync(long serverId, CancellationToken cancellationToken = default) =>
        GetItemAsync<AvailableAdvancedServerFeatures>(
            $"v2/servers/{serverId}/available_advanced_features",
            "available_advanced_server_features",
            cancellationToken);

    /// <inheritdoc />
    public Task<Page<Image>> ListBackupsAsync(long serverId, PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetPageAsync<Image>($"v2/servers/{serverId}/backups", "backups", page, null, cancellationToken);

    /// <inheritdoc />
    public Task<BinaryLaneAction> CreateBackupAsync(long serverId, UploadImageRequest request, CancellationToken cancellationToken = default) =>
        SendItemAsync<BinaryLaneAction>(HttpMethod.Post, $"v2/servers/{serverId}/backups", request ?? throw new ArgumentNullException(nameof(request)), "action", cancellationToken);

    /// <inheritdoc />
    public Task<Page<Kernel>> ListKernelsAsync(long serverId, PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetPageAsync<Kernel>($"v2/servers/{serverId}/kernels", "kernels", page, null, cancellationToken);

    /// <inheritdoc />
    public Task<Page<Image>> ListSnapshotsAsync(long serverId, PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetPageAsync<Image>($"v2/servers/{serverId}/snapshots", "snapshots", page, null, cancellationToken);

    /// <inheritdoc />
    public Task<IReadOnlyList<ThresholdAlert>> GetThresholdAlertsAsync(long serverId, CancellationToken cancellationToken = default) =>
        GetItemAsync<IReadOnlyList<ThresholdAlert>>($"v2/servers/{serverId}/threshold_alerts", "threshold_alerts", cancellationToken);

    /// <inheritdoc />
    public Task<IReadOnlyList<long>> GetThresholdAlertServerIdsAsync(CancellationToken cancellationToken = default) =>
        GetItemAsync<IReadOnlyList<long>>("v2/servers/threshold_alerts", "server_ids", cancellationToken);

    /// <inheritdoc />
    public Task<Page<LicensedSoftware>> ListSoftwareAsync(long serverId, PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetPageAsync<LicensedSoftware>($"v2/servers/{serverId}/software", "licensed_software", page, null, cancellationToken);

    /// <inheritdoc />
    public Task<UserData> GetUserDataAsync(long serverId, CancellationToken cancellationToken = default) =>
        GetDirectItemAsync<UserData>($"v2/servers/{serverId}/user_data", cancellationToken);

    /// <inheritdoc />
    public Task<ServerConsole> GetConsoleAsync(long serverId, CancellationToken cancellationToken = default) =>
        GetItemAsync<ServerConsole>($"v2/servers/{serverId}/console", "console", cancellationToken);
}
