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
public sealed class AccountApi : BinaryLaneResourceBase, IAccountApi
{
    /// <summary>Initializes the account resource.</summary>
    public AccountApi(IBinaryLaneApiExecutor executor, BinaryLaneJsonSerializerOptions json)
        : base(executor, json)
    {
    }

    /// <inheritdoc />
    public Task<Account> GetAsync(CancellationToken cancellationToken = default) =>
        GetItemAsync<Account>("v2/account", "account", cancellationToken);

    /// <inheritdoc />
    public Task<JsonElement> GetRawAsync(CancellationToken cancellationToken = default) =>
        base.GetRawAsync("v2/account", null, cancellationToken);
}

/// <inheritdoc />
public sealed class ActionsApi : BinaryLaneResourceBase, IActionsApi
{
    /// <summary>Initializes the actions resource.</summary>
    public ActionsApi(IBinaryLaneApiExecutor executor, BinaryLaneJsonSerializerOptions json)
        : base(executor, json)
    {
    }

    /// <inheritdoc />
    public Task<BinaryLaneAction> GetAsync(long actionId, CancellationToken cancellationToken = default) =>
        GetItemAsync<BinaryLaneAction>($"v2/actions/{actionId}", "action", cancellationToken);

    /// <inheritdoc />
    public Task<Page<BinaryLaneAction>> ListAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetPageAsync<BinaryLaneAction>("v2/actions", "actions", page, null, cancellationToken);

    /// <inheritdoc />
    public IAsyncEnumerable<BinaryLaneAction> ListAllAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetAllPagesAsync<BinaryLaneAction>("v2/actions", "actions", page, null, cancellationToken);

    /// <inheritdoc />
    public Task ProceedAsync(long actionId, ProceedRequest request, CancellationToken cancellationToken = default) =>
        SendNoContentAsync(HttpMethod.Post, $"v2/actions/{actionId}/proceed", request ?? throw new ArgumentNullException(nameof(request)), null, cancellationToken);

    /// <inheritdoc />
    public async Task<BinaryLaneAction> WaitForCompletionAsync(
        long actionId,
        ActionWaitOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        options ??= new ActionWaitOptions();
        options.Validate();
        var startedAt = DateTimeOffset.UtcNow;

        while (true)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var action = await GetAsync(actionId, cancellationToken).ConfigureAwait(false);
            if (IsTerminal(action.Status))
            {
                return action;
            }

            if (DateTimeOffset.UtcNow - startedAt >= options.Timeout)
            {
                throw new TimeoutException(
                    $"Timed out waiting for BinaryLane action {actionId} after {options.Timeout}.");
            }

            await Task.Delay(options.PollInterval, cancellationToken).ConfigureAwait(false);
        }
    }

    private static bool IsTerminal(string status) =>
        string.Equals(status, "completed", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(status, "errored", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(status, "failed", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(status, "cancelled", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(status, "canceled", StringComparison.OrdinalIgnoreCase);
}

/// <inheritdoc />
public sealed class BillingApi : BinaryLaneResourceBase, IBillingApi
{
    /// <summary>Initializes the billing resource.</summary>
    public BillingApi(IBinaryLaneApiExecutor executor, BinaryLaneJsonSerializerOptions json)
        : base(executor, json)
    {
    }

    /// <inheritdoc />
    public Task<Balance> GetBalanceAsync(CancellationToken cancellationToken = default) =>
        GetItemAsync<Balance>("v2/customers/my/balance", "balance", cancellationToken);

    /// <inheritdoc />
    public Task<Invoice> GetInvoiceAsync(long invoiceId, CancellationToken cancellationToken = default) =>
        GetItemAsync<Invoice>($"v2/customers/my/invoices/{invoiceId}", "invoice", cancellationToken);

    /// <inheritdoc />
    public Task<Page<Invoice>> ListInvoicesAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetPageAsync<Invoice>("v2/customers/my/invoices", "invoices", page, null, cancellationToken);

    /// <inheritdoc />
    public Task<IReadOnlyList<Invoice>> ListUnpaidPaymentFailedInvoicesAsync(CancellationToken cancellationToken = default) =>
        GetItemAsync<IReadOnlyList<Invoice>>("v2/customers/my/unpaid-payment-failed-invoices", "invoices", cancellationToken);
}

/// <inheritdoc />
public sealed class DataUsageApi : BinaryLaneResourceBase, IDataUsageApi
{
    /// <summary>Initializes the data-usage resource.</summary>
    public DataUsageApi(IBinaryLaneApiExecutor executor, BinaryLaneJsonSerializerOptions json)
        : base(executor, json)
    {
    }

    /// <inheritdoc />
    public Task<Page<DataUsage>> ListCurrentAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetPageAsync<DataUsage>("v2/data_usages/current", "data_usages", page, null, cancellationToken);

    /// <inheritdoc />
    public IAsyncEnumerable<DataUsage> ListAllCurrentAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetAllPagesAsync<DataUsage>("v2/data_usages/current", "data_usages", page, null, cancellationToken);

    /// <inheritdoc />
    public Task<DataUsage> GetCurrentAsync(long serverId, CancellationToken cancellationToken = default) =>
        GetItemAsync<DataUsage>($"v2/data_usages/{serverId}/current", "data_usage", cancellationToken);
}

/// <inheritdoc />
public sealed class DomainsApi : BinaryLaneResourceBase, IDomainsApi
{
    /// <summary>Initializes the domains resource.</summary>
    public DomainsApi(IBinaryLaneApiExecutor executor, BinaryLaneJsonSerializerOptions json)
        : base(executor, json)
    {
    }

    /// <inheritdoc />
    public Task<IReadOnlyList<string>> GetNameserversAsync(CancellationToken cancellationToken = default) =>
        GetItemAsync<IReadOnlyList<string>>("v2/domains/nameservers", "local_nameservers", cancellationToken);

    /// <inheritdoc />
    public Task RefreshNameserverCacheAsync(DomainRefreshRequest request, CancellationToken cancellationToken = default) =>
        SendNoContentAsync(HttpMethod.Post, "v2/domains/refresh_nameserver_cache", request ?? throw new ArgumentNullException(nameof(request)), null, cancellationToken);

    /// <inheritdoc />
    public Task<Page<Domain>> ListAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetPageAsync<Domain>("v2/domains", "domains", page, null, cancellationToken);

    /// <inheritdoc />
    public IAsyncEnumerable<Domain> ListAllAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetAllPagesAsync<Domain>("v2/domains", "domains", page, null, cancellationToken);

    /// <inheritdoc />
    public Task<Domain> CreateAsync(DomainRequest request, CancellationToken cancellationToken = default) =>
        SendItemAsync<Domain>(HttpMethod.Post, "v2/domains", request ?? throw new ArgumentNullException(nameof(request)), "domain", cancellationToken);

    /// <inheritdoc />
    public Task<Domain> GetAsync(string domainName, CancellationToken cancellationToken = default) =>
        GetItemAsync<Domain>($"v2/domains/{EscapePathSegment(domainName)}", "domain", cancellationToken);

    /// <inheritdoc />
    public Task DeleteAsync(string domainName, CancellationToken cancellationToken = default) =>
        Executor.DeleteAsync($"v2/domains/{EscapePathSegment(domainName)}", null, null, cancellationToken);

    /// <inheritdoc />
    public Task<Page<DomainRecord>> ListRecordsAsync(string domainName, PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetPageAsync<DomainRecord>($"v2/domains/{EscapePathSegment(domainName)}/records", "domain_records", page, null, cancellationToken);

    /// <inheritdoc />
    public Task<DomainRecord> CreateRecordAsync(string domainName, DomainRecordRequest request, CancellationToken cancellationToken = default) =>
        SendItemAsync<DomainRecord>(HttpMethod.Post, $"v2/domains/{EscapePathSegment(domainName)}/records", request ?? throw new ArgumentNullException(nameof(request)), "domain_record", cancellationToken);

    /// <inheritdoc />
    public Task<DomainRecord> GetRecordAsync(string domainName, long recordId, CancellationToken cancellationToken = default) =>
        GetItemAsync<DomainRecord>($"v2/domains/{EscapePathSegment(domainName)}/records/{recordId}", "domain_record", cancellationToken);

    /// <inheritdoc />
    public Task<DomainRecord> UpdateRecordAsync(string domainName, long recordId, UpdateDomainRecordRequest request, CancellationToken cancellationToken = default) =>
        SendItemAsync<DomainRecord>(HttpMethod.Put, $"v2/domains/{EscapePathSegment(domainName)}/records/{recordId}", request ?? throw new ArgumentNullException(nameof(request)), "domain_record", cancellationToken);

    /// <inheritdoc />
    public Task DeleteRecordAsync(string domainName, long recordId, CancellationToken cancellationToken = default) =>
        Executor.DeleteAsync($"v2/domains/{EscapePathSegment(domainName)}/records/{recordId}", null, null, cancellationToken);

}

/// <inheritdoc />
public sealed class ImagesApi : BinaryLaneResourceBase, IImagesApi
{
    /// <summary>Initializes the images resource.</summary>
    public ImagesApi(IBinaryLaneApiExecutor executor, BinaryLaneJsonSerializerOptions json)
        : base(executor, json)
    {
    }

    /// <inheritdoc />
    public Task<Page<Image>> ListAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetPageAsync<Image>("v2/images", "images", page, null, cancellationToken);

    /// <inheritdoc />
    public IAsyncEnumerable<Image> ListAllAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetAllPagesAsync<Image>("v2/images", "images", page, null, cancellationToken);

    /// <inheritdoc />
    public Task<Image> GetAsync(string imageIdOrSlug, CancellationToken cancellationToken = default) =>
        GetItemAsync<Image>($"v2/images/{EscapePathSegment(imageIdOrSlug)}", "image", cancellationToken);

    /// <inheritdoc />
    public Task<Image> UpdateAsync(long imageId, ImageRequest request, CancellationToken cancellationToken = default) =>
        SendItemAsync<Image>(HttpMethod.Put, $"v2/images/{imageId}", request ?? throw new ArgumentNullException(nameof(request)), "image", cancellationToken);

    /// <inheritdoc />
    public Task<ImageDownload> GetDownloadAsync(long imageId, CancellationToken cancellationToken = default) =>
        GetItemAsync<ImageDownload>($"v2/images/{imageId}/download", "link", cancellationToken);
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

/// <inheritdoc />
public sealed class RegionsApi : BinaryLaneResourceBase, IRegionsApi
{
    /// <summary>Initializes the regions resource.</summary>
    public RegionsApi(IBinaryLaneApiExecutor executor, BinaryLaneJsonSerializerOptions json)
        : base(executor, json)
    {
    }

    /// <inheritdoc />
    public Task<Page<Region>> ListAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetPageAsync<Region>("v2/regions", "regions", page, null, cancellationToken);

    /// <inheritdoc />
    public IAsyncEnumerable<Region> ListAllAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetAllPagesAsync<Region>("v2/regions", "regions", page, null, cancellationToken);
}
