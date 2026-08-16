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

/// <inheritdoc />
public sealed class ReverseNamesApi : BinaryLaneResourceBase, IReverseNamesApi
{
    /// <summary>Initializes the reverse-names resource.</summary>
    public ReverseNamesApi(IBinaryLaneApiExecutor executor, BinaryLaneJsonSerializerOptions json)
        : base(executor, json)
    {
    }

    /// <inheritdoc />
    public Task<Page<string>> ListIpv6Async(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetPageAsync<string>("v2/reverse_names/ipv6", "reverse_nameservers", page, null, cancellationToken);

    /// <inheritdoc />
    public IAsyncEnumerable<string> ListAllIpv6Async(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetAllPagesAsync<string>("v2/reverse_names/ipv6", "reverse_nameservers", page, null, cancellationToken);

    /// <inheritdoc />
    public async Task<ActionSubmission> UpdateIpv6Async(ReverseNameserversRequest request, CancellationToken cancellationToken = default)
    {
        var response = await SendResponseAsync(
            HttpMethod.Post,
            "v2/reverse_names/ipv6",
            request ?? throw new ArgumentNullException(nameof(request)),
            null,
            cancellationToken).ConfigureAwait(false);
        TryDeserializeEnvelope<BinaryLaneAction>(response.Body, "action", out var action);
        return new ActionSubmission(response.StatusCode, action, response.Body);
    }
}

/// <inheritdoc />
public sealed class SampleSetsApi : BinaryLaneResourceBase, ISampleSetsApi
{
    /// <summary>Initializes the monitoring sample-set resource.</summary>
    public SampleSetsApi(IBinaryLaneApiExecutor executor, BinaryLaneJsonSerializerOptions json)
        : base(executor, json)
    {
    }

    /// <inheritdoc />
    public async Task<SampleSet?> GetLatestAsync(long serverId, string? dataInterval = null, CancellationToken cancellationToken = default)
    {
        IReadOnlyDictionary<string, object?>? query = string.IsNullOrWhiteSpace(dataInterval)
            ? null
            : new Dictionary<string, object?> { ["data_interval"] = dataInterval };
        var response = await GetRawAsync($"v2/samplesets/{serverId}/latest", query, cancellationToken).ConfigureAwait(false);
        return TryDeserializeEnvelope<SampleSet>(response, "sample_set", out var sampleSet) ? sampleSet : null;
    }

    /// <inheritdoc />
    public Task<Page<SampleSet>> ListAsync(
        long serverId,
        PageRequest? page = null,
        string? dataInterval = null,
        DateTimeOffset? start = null,
        DateTimeOffset? endAt = null,
        CancellationToken cancellationToken = default)
    {
        var query = new Dictionary<string, object?>
        {
            ["data_interval"] = dataInterval,
            ["start"] = start,
            ["end"] = endAt,
        };
        return GetPageAsync<SampleSet>($"v2/samplesets/{serverId}", "sample_sets", page, query, cancellationToken);
    }
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

/// <inheritdoc />
public sealed class SizesApi : BinaryLaneResourceBase, ISizesApi
{
    /// <summary>Initializes the sizes resource.</summary>
    public SizesApi(IBinaryLaneApiExecutor executor, BinaryLaneJsonSerializerOptions json)
        : base(executor, json)
    {
    }

    /// <inheritdoc />
    public Task<Page<Size>> ListAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetPageAsync<Size>("v2/sizes", "sizes", page, null, cancellationToken);

    /// <inheritdoc />
    public IAsyncEnumerable<Size> ListAllAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetAllPagesAsync<Size>("v2/sizes", "sizes", page, null, cancellationToken);
}

/// <inheritdoc />
public sealed class SoftwareApi : BinaryLaneResourceBase, ISoftwareApi
{
    /// <summary>Initializes the software resource.</summary>
    public SoftwareApi(IBinaryLaneApiExecutor executor, BinaryLaneJsonSerializerOptions json)
        : base(executor, json)
    {
    }

    /// <inheritdoc />
    public Task<Page<Software>> ListAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetPageAsync<Software>("v2/software", "software", page, null, cancellationToken);

    /// <inheritdoc />
    public IAsyncEnumerable<Software> ListAllAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetAllPagesAsync<Software>("v2/software", "software", page, null, cancellationToken);

    /// <inheritdoc />
    public Task<Software> GetAsync(long softwareId, CancellationToken cancellationToken = default) =>
        GetItemAsync<Software>($"v2/software/{softwareId}", "software", cancellationToken);

    /// <inheritdoc />
    public Task<Page<Software>> ListOperatingSystemAsync(string operatingSystemIdOrSlug, PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetPageAsync<Software>(
            $"v2/software/operating_system/{EscapePathSegment(operatingSystemIdOrSlug)}",
            "software",
            page,
            null,
            cancellationToken);
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
        SendItemAsync<Vpc>(new HttpMethod("PATCH"), $"v2/vpcs/{vpcId}", request ?? throw new ArgumentNullException(nameof(request)), "vpc", cancellationToken);

    /// <inheritdoc />
    public Task DeleteAsync(long vpcId, CancellationToken cancellationToken = default) =>
        Executor.DeleteAsync($"v2/vpcs/{vpcId}", null, null, cancellationToken);

    /// <inheritdoc />
    public Task<Page<VpcMember>> ListMembersAsync(long vpcId, PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetPageAsync<VpcMember>($"v2/vpcs/{vpcId}/members", "members", page, null, cancellationToken);
}
