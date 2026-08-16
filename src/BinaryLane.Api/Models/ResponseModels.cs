using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BinaryLane.Api.V2.Models;

/// <summary>Pagination metadata returned by BinaryLane list endpoints.</summary>
public sealed class PageMeta : BinaryLaneDto
{
    [JsonPropertyName("total")]
    public int Total { get; init; }
}

/// <summary>Pagination links returned by BinaryLane list endpoints.</summary>
public sealed class PageLinks : BinaryLaneDto
{
    [JsonPropertyName("pages")]
    public PageNavigation Pages { get; init; } = new();
}

/// <summary>URLs for neighbouring pages in a BinaryLane list response.</summary>
public sealed class PageNavigation : BinaryLaneDto
{
    [JsonPropertyName("first")]
    public string? First { get; init; }

    [JsonPropertyName("prev")]
    public string? Previous { get; init; }

    [JsonPropertyName("next")]
    public string? Next { get; init; }

    [JsonPropertyName("last")]
    public string? Last { get; init; }
}

/// <summary>An RFC 7807-style problem returned by BinaryLane.</summary>
public class ProblemDetails : BinaryLaneDto
{
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    [JsonPropertyName("title")]
    public string? Title { get; init; }

    [JsonPropertyName("status")]
    public int? Status { get; init; }

    [JsonPropertyName("detail")]
    public string? Detail { get; init; }

    [JsonPropertyName("instance")]
    public string? Instance { get; init; }
}

/// <summary>A validation problem returned by BinaryLane.</summary>
public sealed class ValidationProblemDetails : ProblemDetails
{
    [JsonPropertyName("errors")]
    public IReadOnlyDictionary<string, IReadOnlyList<string>>? Errors { get; init; }
}

public sealed class AccountResponse : BinaryLaneDto
{
    [JsonPropertyName("account")]
    public Account Account { get; init; } = new();
}

public sealed class ActionResponse : BinaryLaneDto
{
    [JsonPropertyName("action")]
    public BinaryLaneAction Action { get; init; } = new();
}

public sealed class ActionsLinks : BinaryLaneDto
{
    [JsonPropertyName("actions")]
    public IReadOnlyList<ActionLink> Actions { get; init; } = Array.Empty<ActionLink>();
}

public sealed class ActionsResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("actions")]
    public IReadOnlyList<BinaryLaneAction> Actions { get; init; } = Array.Empty<BinaryLaneAction>();
}

public sealed class BalanceResponse : BinaryLaneDto
{
    [JsonPropertyName("balance")]
    public Balance Balance { get; init; } = new();
}

public sealed class InvoiceResponse : BinaryLaneDto
{
    [JsonPropertyName("invoice")]
    public Invoice Invoice { get; init; } = new();
}

public sealed class InvoicesResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("invoices")]
    public IReadOnlyList<Invoice> Invoices { get; init; } = Array.Empty<Invoice>();
}

public sealed class UnpaidFailedInvoicesResponse : BinaryLaneDto
{
    [JsonPropertyName("invoices")]
    public IReadOnlyList<Invoice> Invoices { get; init; } = Array.Empty<Invoice>();
}

public sealed class DataUsageResponse : BinaryLaneDto
{
    [JsonPropertyName("data_usage")]
    public DataUsage DataUsage { get; init; } = new();
}

public sealed class DataUsagesResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("data_usages")]
    public IReadOnlyList<DataUsage> DataUsages { get; init; } = Array.Empty<DataUsage>();
}

public sealed class LocalNameserversResponse : BinaryLaneDto
{
    [JsonPropertyName("local_nameservers")]
    public IReadOnlyList<string> LocalNameservers { get; init; } = Array.Empty<string>();
}

public sealed class DomainResponse : BinaryLaneDto
{
    [JsonPropertyName("domain")]
    public Domain Domain { get; init; } = new();
}

public sealed class DomainsResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("domains")]
    public IReadOnlyList<Domain> Domains { get; init; } = Array.Empty<Domain>();
}

public sealed class DomainRecordResponse : BinaryLaneDto
{
    [JsonPropertyName("domain_record")]
    public DomainRecord DomainRecord { get; init; } = new();
}

public sealed class DomainRecordsResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("domain_records")]
    public IReadOnlyList<DomainRecord> DomainRecords { get; init; } = Array.Empty<DomainRecord>();
}

public sealed class ImageResponse : BinaryLaneDto
{
    [JsonPropertyName("image")]
    public Image Image { get; init; } = new();
}

public sealed class ImagesResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("images")]
    public IReadOnlyList<Image> Images { get; init; } = Array.Empty<Image>();
}

public sealed class ImageDownloadResponse : BinaryLaneDto
{
    [JsonPropertyName("link")]
    public ImageDownload Link { get; init; } = new();
}

public sealed class SshKeyResponse : BinaryLaneDto
{
    [JsonPropertyName("ssh_key")]
    public SshKey SshKey { get; init; } = new();
}

public sealed class SshKeysResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("ssh_keys")]
    public IReadOnlyList<SshKey> SshKeys { get; init; } = Array.Empty<SshKey>();
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

public sealed class RegionsResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("regions")]
    public IReadOnlyList<Region> Regions { get; init; } = Array.Empty<Region>();
}

public sealed class ReverseNameServersResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("reverse_nameservers")]
    public IReadOnlyList<string> ReverseNameservers { get; init; } = Array.Empty<string>();
}

public sealed class SampleSetResponse : BinaryLaneDto
{
    [JsonPropertyName("sample_set")]
    public SampleSet? SampleSet { get; init; }
}

public sealed class SampleSetsResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("sample_sets")]
    public IReadOnlyList<SampleSet> SampleSets { get; init; } = Array.Empty<SampleSet>();
}

public sealed class ServerResponse : BinaryLaneDto
{
    [JsonPropertyName("server")]
    public Server Server { get; init; } = new();
}

public sealed class ServersResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("servers")]
    public IReadOnlyList<Server> Servers { get; init; } = Array.Empty<Server>();
}

public sealed class CreateServerResponse : BinaryLaneDto
{
    [JsonPropertyName("server")]
    public Server Server { get; init; } = new();

    [JsonPropertyName("links")]
    public ActionsLinks Links { get; init; } = new();
}

public sealed class AdvancedFirewallRulesResponse : BinaryLaneDto
{
    [JsonPropertyName("firewall_rules")]
    public IReadOnlyList<AdvancedFirewallRule> FirewallRules { get; init; } = Array.Empty<AdvancedFirewallRule>();
}

public sealed class AvailableAdvancedServerFeaturesResponse : BinaryLaneDto
{
    [JsonPropertyName("available_advanced_server_features")]
    public AvailableAdvancedServerFeatures AvailableAdvancedServerFeatures { get; init; } = new();
}

public sealed class BackupsResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("backups")]
    public IReadOnlyList<Image> Backups { get; init; } = Array.Empty<Image>();
}

public sealed class KernelsResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("kernels")]
    public IReadOnlyList<Kernel> Kernels { get; init; } = Array.Empty<Kernel>();
}

public sealed class SnapshotsResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("snapshots")]
    public IReadOnlyList<Image> Snapshots { get; init; } = Array.Empty<Image>();
}

public sealed class ThresholdAlertsResponse : BinaryLaneDto
{
    [JsonPropertyName("threshold_alerts")]
    public IReadOnlyList<ThresholdAlert> ThresholdAlerts { get; init; } = Array.Empty<ThresholdAlert>();
}

public sealed class CurrentServerAlertsResponse : BinaryLaneDto
{
    [JsonPropertyName("server_ids")]
    public IReadOnlyList<long> ServerIds { get; init; } = Array.Empty<long>();
}

public sealed class LicensedSoftwaresResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("licensed_software")]
    public IReadOnlyList<LicensedSoftware> LicensedSoftware { get; init; } = Array.Empty<LicensedSoftware>();
}

public sealed class ServerConsoleResponse : BinaryLaneDto
{
    [JsonPropertyName("console")]
    public ServerConsole Console { get; init; } = new();
}

public sealed class SizesResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("sizes")]
    public IReadOnlyList<Size> Sizes { get; init; } = Array.Empty<Size>();
}

public sealed class SoftwareResponse : BinaryLaneDto
{
    [JsonPropertyName("software")]
    public Software Software { get; init; } = new();
}

public sealed class SoftwaresResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("software")]
    public IReadOnlyList<Software> Software { get; init; } = Array.Empty<Software>();
}

public sealed class VpcResponse : BinaryLaneDto
{
    [JsonPropertyName("vpc")]
    public Vpc Vpc { get; init; } = new();
}

public sealed class VpcsResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("vpcs")]
    public IReadOnlyList<Vpc> Vpcs { get; init; } = Array.Empty<Vpc>();
}

public sealed class VpcMembersResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("members")]
    public IReadOnlyList<VpcMember> Members { get; init; } = Array.Empty<VpcMember>();
}
