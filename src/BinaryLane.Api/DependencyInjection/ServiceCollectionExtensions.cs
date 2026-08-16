using System;
using BinaryLane.Api.V2.Authentication;
using BinaryLane.Api.V2.Configuration;
using BinaryLane.Api.V2.Http;
using BinaryLane.Api.V2.Resources;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace BinaryLane.Api.V2.DependencyInjection;

/// <summary>Dependency-injection registration for the BinaryLane API client.</summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers a typed BinaryLane client. The returned builder lets an application compose its
    /// own proxy, telemetry, and safe GET-only resilience handlers.
    /// </summary>
    public static IHttpClientBuilder AddBinaryLaneApi(
        this IServiceCollection services,
        Action<BinaryLaneOptions> configure)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configure);
#else
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }
#endif

        services.AddOptions<BinaryLaneOptions>()
            .Configure(configure)
            .ValidateOnStart();
        services.TryAddSingleton<IValidateOptions<BinaryLaneOptions>, BinaryLaneOptionsValidator>();
        services.TryAddSingleton<IBinaryLaneTokenProvider, OptionsBinaryLaneTokenProvider>();
        services.TryAddSingleton<BinaryLaneJsonSerializerOptions>();

        var clientBuilder = services.AddHttpClient<IBinaryLaneClient, BinaryLaneClient>((serviceProvider, client) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<BinaryLaneOptions>>().Value;
            client.BaseAddress = EnsureTrailingSlash(options.BaseUrl);
            client.Timeout = TimeSpan.FromSeconds(options.RequestTimeoutSeconds);
        });

        // Register focused resources as well as the façade so applications can depend on precisely
        // the API area they use. TryAdd preserves a consumer's explicit test double or custom resource.
        services.TryAddTransient<IAccountApi>(serviceProvider => serviceProvider.GetRequiredService<IBinaryLaneClient>().Account);
        services.TryAddTransient<IActionsApi>(serviceProvider => serviceProvider.GetRequiredService<IBinaryLaneClient>().Actions);
        services.TryAddTransient<IBillingApi>(serviceProvider => serviceProvider.GetRequiredService<IBinaryLaneClient>().Billing);
        services.TryAddTransient<IDataUsageApi>(serviceProvider => serviceProvider.GetRequiredService<IBinaryLaneClient>().DataUsage);
        services.TryAddTransient<IDomainsApi>(serviceProvider => serviceProvider.GetRequiredService<IBinaryLaneClient>().Domains);
        services.TryAddTransient<IImagesApi>(serviceProvider => serviceProvider.GetRequiredService<IBinaryLaneClient>().Images);
        services.TryAddTransient<ISshKeysApi>(serviceProvider => serviceProvider.GetRequiredService<IBinaryLaneClient>().SshKeys);
        services.TryAddTransient<ILoadBalancersApi>(serviceProvider => serviceProvider.GetRequiredService<IBinaryLaneClient>().LoadBalancers);
        services.TryAddTransient<IRegionsApi>(serviceProvider => serviceProvider.GetRequiredService<IBinaryLaneClient>().Regions);
        services.TryAddTransient<IReverseNamesApi>(serviceProvider => serviceProvider.GetRequiredService<IBinaryLaneClient>().ReverseNames);
        services.TryAddTransient<ISampleSetsApi>(serviceProvider => serviceProvider.GetRequiredService<IBinaryLaneClient>().SampleSets);
        services.TryAddTransient<IServersApi>(serviceProvider => serviceProvider.GetRequiredService<IBinaryLaneClient>().Servers);
        services.TryAddTransient<ISizesApi>(serviceProvider => serviceProvider.GetRequiredService<IBinaryLaneClient>().Sizes);
        services.TryAddTransient<ISoftwareApi>(serviceProvider => serviceProvider.GetRequiredService<IBinaryLaneClient>().Software);
        services.TryAddTransient<IVpcsApi>(serviceProvider => serviceProvider.GetRequiredService<IBinaryLaneClient>().Vpcs);

        return clientBuilder;
    }

    private static Uri EnsureTrailingSlash(string baseUrl)
    {
        var uri = new Uri(baseUrl, UriKind.Absolute);
        var absoluteUri = uri.AbsoluteUri;
        return absoluteUri.Length > 0 && absoluteUri[absoluteUri.Length - 1] == '/'
            ? uri
            : new Uri(absoluteUri + "/", UriKind.Absolute);
    }
}
