using System;
using BinaryLane.Api.V2;
using BinaryLane.Api.V2.Configuration;
using BinaryLane.Api.V2.DependencyInjection;
using BinaryLane.Api.V2.Resources;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace BinaryLane.Api.Tests;

public sealed class DependencyInjectionTests
{
    [Fact]
    public void AddBinaryLaneApiRegistersAComposableClient()
    {
        var services = new ServiceCollection();
        services.AddBinaryLaneApi(options => options.ApiToken = "test-token");

        using var provider = services.BuildServiceProvider();
        var client = provider.GetRequiredService<IBinaryLaneClient>();

        Assert.NotNull(client.Account);
        Assert.NotNull(client.Actions);
        Assert.NotNull(client.Servers);
        Assert.NotNull(client.Regions);
        Assert.NotNull(client.Vpcs);
        Assert.NotNull(provider.GetRequiredService<IServersApi>());
        Assert.NotNull(provider.GetRequiredService<IDomainsApi>());
    }

    [Fact]
    public void InvalidOptionsAreRejectedWhenResolved()
    {
        var services = new ServiceCollection();
        services.AddBinaryLaneApi(options =>
        {
            options.BaseUrl = "not-a-url";
            options.ApiToken = "test-token";
        });

        using var provider = services.BuildServiceProvider();

        Assert.Throws<OptionsValidationException>(
            () => provider.GetRequiredService<IOptions<BinaryLaneOptions>>().Value);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(301)]
    public void OutOfRangeTimeoutIsRejectedWhenResolved(int timeoutSeconds)
    {
        var services = new ServiceCollection();
        services.AddBinaryLaneApi(options =>
        {
            options.ApiToken = "test-token";
            options.RequestTimeoutSeconds = timeoutSeconds;
        });

        using var provider = services.BuildServiceProvider();

        Assert.Throws<OptionsValidationException>(
            () => provider.GetRequiredService<IOptions<BinaryLaneOptions>>().Value);
    }

    [Theory]
    [InlineData("http://api.example.test/")]
    [InlineData("https://user:password@api.example.test/")]
    [InlineData("https://api.example.test/?unexpected=value")]
    [InlineData("https://api.example.test/#fragment")]
    public void BaseUrlWithCredentialsQueryOrFragmentIsRejected(string baseUrl)
    {
        var services = new ServiceCollection();
        services.AddBinaryLaneApi(options =>
        {
            options.BaseUrl = baseUrl;
            options.ApiToken = "test-token";
        });

        using var provider = services.BuildServiceProvider();

        Assert.Throws<OptionsValidationException>(
            () => provider.GetRequiredService<IOptions<BinaryLaneOptions>>().Value);
    }
}
