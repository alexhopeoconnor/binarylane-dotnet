using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using BinaryLane.Api.V2.Models;
using BinaryLane.Api.V2.Pagination;

namespace BinaryLane.Api.V2.Resources;

/// <summary>Reads information about the authenticated account.</summary>
public interface IAccountApi
{
    /// <summary>Gets the current account.</summary>
    Task<Account> GetAsync(CancellationToken cancellationToken = default);

    /// <summary>Gets the untyped account response for forward compatibility.</summary>
    Task<JsonElement> GetRawAsync(CancellationToken cancellationToken = default);
}

/// <summary>Reads and waits for asynchronous account and server actions.</summary>
public interface IActionsApi
{
    /// <summary>Gets an action by its global action ID.</summary>
    Task<BinaryLaneAction> GetAsync(long actionId, CancellationToken cancellationToken = default);

    /// <summary>Lists global actions.</summary>
    Task<Page<BinaryLaneAction>> ListAsync(PageRequest? page = null, CancellationToken cancellationToken = default);

    /// <summary>Streams all global actions.</summary>
    IAsyncEnumerable<BinaryLaneAction> ListAllAsync(PageRequest? page = null, CancellationToken cancellationToken = default);

    /// <summary>Supplies a requested user interaction for an action.</summary>
    Task ProceedAsync(long actionId, ProceedRequest request, CancellationToken cancellationToken = default);

    /// <summary>Polls an action until BinaryLane reports a terminal state.</summary>
    Task<BinaryLaneAction> WaitForCompletionAsync(
        long actionId,
        ActionWaitOptions? options = null,
        CancellationToken cancellationToken = default);
}

/// <summary>Reads account billing and invoice information.</summary>
public interface IBillingApi
{
    Task<Balance> GetBalanceAsync(CancellationToken cancellationToken = default);
    Task<Invoice> GetInvoiceAsync(long invoiceId, CancellationToken cancellationToken = default);
    Task<Page<Invoice>> ListInvoicesAsync(PageRequest? page = null, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Invoice>> ListUnpaidPaymentFailedInvoicesAsync(CancellationToken cancellationToken = default);
}

/// <summary>Reads data-transfer usage data.</summary>
public interface IDataUsageApi
{
    Task<Page<DataUsage>> ListCurrentAsync(PageRequest? page = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<DataUsage> ListAllCurrentAsync(PageRequest? page = null, CancellationToken cancellationToken = default);
    Task<DataUsage> GetCurrentAsync(long serverId, CancellationToken cancellationToken = default);
}

/// <summary>Manages DNS domains and DNS records.</summary>
public interface IDomainsApi
{
    Task<IReadOnlyList<string>> GetNameserversAsync(CancellationToken cancellationToken = default);
    Task RefreshNameserverCacheAsync(DomainRefreshRequest request, CancellationToken cancellationToken = default);
    Task<Page<Domain>> ListAsync(PageRequest? page = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<Domain> ListAllAsync(PageRequest? page = null, CancellationToken cancellationToken = default);
    Task<Domain> CreateAsync(DomainRequest request, CancellationToken cancellationToken = default);
    Task<Domain> GetAsync(string domainName, CancellationToken cancellationToken = default);
    Task DeleteAsync(string domainName, CancellationToken cancellationToken = default);
    Task<Page<DomainRecord>> ListRecordsAsync(string domainName, PageRequest? page = null, CancellationToken cancellationToken = default);
    Task<DomainRecord> CreateRecordAsync(string domainName, DomainRecordRequest request, CancellationToken cancellationToken = default);
    Task<DomainRecord> GetRecordAsync(string domainName, long recordId, CancellationToken cancellationToken = default);
    Task<DomainRecord> UpdateRecordAsync(string domainName, long recordId, UpdateDomainRecordRequest request, CancellationToken cancellationToken = default);
    Task DeleteRecordAsync(string domainName, long recordId, CancellationToken cancellationToken = default);
}

/// <summary>Reads and manages account images and backups represented as images.</summary>
public interface IImagesApi
{
    Task<Page<Image>> ListAsync(PageRequest? page = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<Image> ListAllAsync(PageRequest? page = null, CancellationToken cancellationToken = default);
    Task<Image> GetAsync(string imageIdOrSlug, CancellationToken cancellationToken = default);
    Task<Image> UpdateAsync(long imageId, ImageRequest request, CancellationToken cancellationToken = default);
    Task<ImageDownload> GetDownloadAsync(long imageId, CancellationToken cancellationToken = default);
}

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

/// <summary>Lists BinaryLane regions.</summary>
public interface IRegionsApi
{
    Task<Page<Region>> ListAsync(PageRequest? page = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<Region> ListAllAsync(PageRequest? page = null, CancellationToken cancellationToken = default);
}

/// <summary>Manages IPv6 reverse-name configuration.</summary>
public interface IReverseNamesApi
{
    Task<Page<string>> ListIpv6Async(PageRequest? page = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<string> ListAllIpv6Async(PageRequest? page = null, CancellationToken cancellationToken = default);
    Task<ActionSubmission> UpdateIpv6Async(ReverseNameserversRequest request, CancellationToken cancellationToken = default);
}

/// <summary>Reads monitoring samples for servers.</summary>
public interface ISampleSetsApi
{
    Task<SampleSet?> GetLatestAsync(long serverId, string? dataInterval = null, CancellationToken cancellationToken = default);
    Task<Page<SampleSet>> ListAsync(long serverId, PageRequest? page = null, string? dataInterval = null, DateTimeOffset? start = null, DateTimeOffset? endAt = null, CancellationToken cancellationToken = default);
}

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

/// <summary>Lists available server sizes.</summary>
public interface ISizesApi
{
    Task<Page<Size>> ListAsync(PageRequest? page = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<Size> ListAllAsync(PageRequest? page = null, CancellationToken cancellationToken = default);
}

/// <summary>Lists available licensed software and operating-system software.</summary>
public interface ISoftwareApi
{
    Task<Page<Software>> ListAsync(PageRequest? page = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<Software> ListAllAsync(PageRequest? page = null, CancellationToken cancellationToken = default);
    Task<Software> GetAsync(long softwareId, CancellationToken cancellationToken = default);
    Task<Page<Software>> ListOperatingSystemAsync(string operatingSystemIdOrSlug, PageRequest? page = null, CancellationToken cancellationToken = default);
}

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
