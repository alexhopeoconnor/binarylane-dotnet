using System;
using System.Net.Http;
using BinaryLane.Api.V2.Authentication;
using BinaryLane.Api.V2.Http;
using BinaryLane.Api.V2.Resources;
using Microsoft.Extensions.DependencyInjection;

namespace BinaryLane.Api.V2;

/// <summary>Default composable BinaryLane v2 client.</summary>
public sealed class BinaryLaneClient : IBinaryLaneClient
{
    /// <summary>
    /// Creates a client using the supplied HTTP client and token provider. The HTTP client should
    /// normally be obtained through <c>IHttpClientFactory</c> or registered with
    /// <c>AddBinaryLaneApi</c>.
    /// </summary>
    public BinaryLaneClient(HttpClient httpClient, IBinaryLaneTokenProvider tokenProvider)
        : this(httpClient, tokenProvider, new BinaryLaneJsonSerializerOptions())
    {
    }

    /// <summary>Creates a client using supplied HTTP, authentication, and JSON services.</summary>
    [ActivatorUtilitiesConstructor]
    public BinaryLaneClient(
        HttpClient httpClient,
        IBinaryLaneTokenProvider tokenProvider,
        BinaryLaneJsonSerializerOptions json)
        : this(new BinaryLaneHttpExecutor(httpClient, tokenProvider, json), json)
    {
    }

    /// <summary>Creates a client over a custom request executor.</summary>
    public BinaryLaneClient(IBinaryLaneApiExecutor executor, BinaryLaneJsonSerializerOptions json)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(executor);
        ArgumentNullException.ThrowIfNull(json);
#else
        if (executor is null)
        {
            throw new ArgumentNullException(nameof(executor));
        }

        if (json is null)
        {
            throw new ArgumentNullException(nameof(json));
        }
#endif

        Executor = executor;

        Account = new AccountApi(executor, json);
        Actions = new ActionsApi(executor, json);
        Billing = new BillingApi(executor, json);
        DataUsage = new DataUsageApi(executor, json);
        Domains = new DomainsApi(executor, json);
        Images = new ImagesApi(executor, json);
        SshKeys = new SshKeysApi(executor, json);
        LoadBalancers = new LoadBalancersApi(executor, json);
        Regions = new RegionsApi(executor, json);
        ReverseNames = new ReverseNamesApi(executor, json);
        SampleSets = new SampleSetsApi(executor, json);
        Servers = new ServersApi(executor, json);
        Sizes = new SizesApi(executor, json);
        Software = new SoftwareApi(executor, json);
        Vpcs = new VpcsApi(executor, json);
    }

    /// <inheritdoc />
    public IBinaryLaneApiExecutor Executor { get; }

    /// <inheritdoc />
    public IAccountApi Account { get; }

    /// <inheritdoc />
    public IActionsApi Actions { get; }

    /// <inheritdoc />
    public IBillingApi Billing { get; }

    /// <inheritdoc />
    public IDataUsageApi DataUsage { get; }

    /// <inheritdoc />
    public IDomainsApi Domains { get; }

    /// <inheritdoc />
    public IImagesApi Images { get; }

    /// <inheritdoc />
    public ISshKeysApi SshKeys { get; }

    /// <inheritdoc />
    public ILoadBalancersApi LoadBalancers { get; }

    /// <inheritdoc />
    public IRegionsApi Regions { get; }

    /// <inheritdoc />
    public IReverseNamesApi ReverseNames { get; }

    /// <inheritdoc />
    public ISampleSetsApi SampleSets { get; }

    /// <inheritdoc />
    public IServersApi Servers { get; }

    /// <inheritdoc />
    public ISizesApi Sizes { get; }

    /// <inheritdoc />
    public ISoftwareApi Software { get; }

    /// <inheritdoc />
    public IVpcsApi Vpcs { get; }
}
