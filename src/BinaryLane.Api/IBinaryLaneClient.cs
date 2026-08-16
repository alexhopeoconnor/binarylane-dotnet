using BinaryLane.Api.V2.Http;
using BinaryLane.Api.V2.Resources;

namespace BinaryLane.Api.V2;

/// <summary>
/// Composable entry point for the BinaryLane v2 API. Each resource is independently represented by
/// an interface so applications can depend on and mock only the portion they use.
/// </summary>
public interface IBinaryLaneClient
{
    /// <summary>Low-level executor for a newly introduced endpoint not yet represented by a resource API.</summary>
    IBinaryLaneApiExecutor Executor { get; }

    /// <summary>Current account information.</summary>
    IAccountApi Account { get; }

    /// <summary>Global asynchronous actions.</summary>
    IActionsApi Actions { get; }

    /// <summary>Billing balance and invoices.</summary>
    IBillingApi Billing { get; }

    /// <summary>Data-transfer usage.</summary>
    IDataUsageApi DataUsage { get; }

    /// <summary>DNS domains and DNS records.</summary>
    IDomainsApi Domains { get; }

    /// <summary>Images and image downloads.</summary>
    IImagesApi Images { get; }

    /// <summary>SSH keys.</summary>
    ISshKeysApi SshKeys { get; }

    /// <summary>Load balancers, their members, and forwarding rules.</summary>
    ILoadBalancersApi LoadBalancers { get; }

    /// <summary>Available regions.</summary>
    IRegionsApi Regions { get; }

    /// <summary>IPv6 reverse nameservers.</summary>
    IReverseNamesApi ReverseNames { get; }

    /// <summary>Monitoring sample sets.</summary>
    ISampleSetsApi SampleSets { get; }

    /// <summary>Servers and server-scoped resources.</summary>
    IServersApi Servers { get; }

    /// <summary>Available server sizes.</summary>
    ISizesApi Sizes { get; }

    /// <summary>Licensed and operating-system software.</summary>
    ISoftwareApi Software { get; }

    /// <summary>Private virtual networks.</summary>
    IVpcsApi Vpcs { get; }
}
