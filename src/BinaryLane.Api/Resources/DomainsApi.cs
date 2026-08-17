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
