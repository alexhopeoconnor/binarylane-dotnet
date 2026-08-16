using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BinaryLane.Api.V2;
using BinaryLane.Api.V2.Authentication;
using BinaryLane.Api.V2.Models;
using Xunit;

namespace BinaryLane.Api.Tests;

public sealed class ClientResourceTests
{
    [Fact]
    public async Task ServersListAllFollowsTheProviderNextPageLink()
    {
        var requestCount = 0;
        using var httpClient = new HttpClient(new HttpClientTestHandler((request, _) =>
        {
            requestCount++;
            var json = request.RequestUri!.Query.Contains("page=2", StringComparison.Ordinal)
                ? "{\"meta\":{\"total\":2},\"links\":{\"pages\":{}},\"servers\":[{\"id\":2,\"name\":\"two\",\"status\":\"active\"}]}"
                : "{\"meta\":{\"total\":2},\"links\":{\"pages\":{\"next\":\"/v2/servers?page=2\"}},\"servers\":[{\"id\":1,\"name\":\"one\",\"status\":\"active\"}]}";
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(json) });
        }))
        {
            BaseAddress = new Uri("https://api.example.test/"),
        };
        var client = new BinaryLaneClient(httpClient, new StaticBinaryLaneTokenProvider("token"));
        var servers = new List<Server>();

        await foreach (var server in client.Servers.ListAllAsync())
        {
            servers.Add(server);
        }

        Assert.Equal(2, requestCount);
        Assert.Collection(
            servers,
            server => Assert.Equal("one", server.Name),
            server => Assert.Equal("two", server.Name));
    }

    [Fact]
    public async Task ServersListAllStopsWhenTheProviderRepeatsANextPageLink()
    {
        var requestCount = 0;
        using var httpClient = new HttpClient(new HttpClientTestHandler((_, _) =>
        {
            requestCount++;
            const string json = "{\"meta\":{\"total\":1},\"links\":{\"pages\":{\"next\":\"/v2/servers?page=1\"}},\"servers\":[{\"id\":1,\"name\":\"one\",\"status\":\"active\"}]}";
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(json) });
        }))
        {
            BaseAddress = new Uri("https://api.example.test/"),
        };
        var client = new BinaryLaneClient(httpClient, new StaticBinaryLaneTokenProvider("token"));

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await foreach (var _ in client.Servers.ListAllAsync())
            {
            }
        });

        Assert.Contains("repeated pagination link", exception.Message, StringComparison.Ordinal);
        Assert.Equal(2, requestCount);
    }

    [Fact]
    public async Task SubmitActionUsesRealActionsEndpointAndReportsAcceptedStatus()
    {
        HttpRequestMessage? received = null;
        string? body = null;
        using var httpClient = new HttpClient(new HttpClientTestHandler(async (request, cancellationToken) =>
        {
            received = request;
            body = request.Content is null ? null : await request.Content.ReadAsStringAsync(cancellationToken);
            return new HttpResponseMessage(HttpStatusCode.Accepted)
            {
                Content = new StringContent("{\"action\":{\"id\":9,\"type\":\"power_on\",\"status\":\"in-progress\",\"started_at\":\"2026-01-01T00:00:00Z\"}}"),
            };
        }))
        {
            BaseAddress = new Uri("https://api.example.test/"),
        };
        var client = new BinaryLaneClient(httpClient, new StaticBinaryLaneTokenProvider("token"));

        var submission = await client.Servers.SubmitActionAsync(123, new PowerOnServerAction());

        Assert.NotNull(received);
        Assert.Equal("/v2/servers/123/actions", received!.RequestUri!.AbsolutePath);
        Assert.Equal("POST", received.Method.Method);
        Assert.Contains("\"type\":\"power_on\"", body!, StringComparison.Ordinal);
        Assert.True(submission.IsAccepted);
        Assert.Equal(9, submission.Action!.Id);
    }

    [Fact]
    public async Task ListEndpointsRejectAResponseWithoutTheExpectedCollectionEnvelope()
    {
        using var httpClient = new HttpClient(new HttpClientTestHandler((_, _) =>
            Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"meta\":{\"total\":0},\"wrong_collection\":[]}"),
            })))
        {
            BaseAddress = new Uri("https://api.example.test/"),
        };
        var client = new BinaryLaneClient(httpClient, new StaticBinaryLaneTokenProvider("token"));

        var exception = await Assert.ThrowsAsync<JsonException>(() => client.Servers.ListAsync());

        Assert.Contains("servers", exception.Message, StringComparison.Ordinal);
    }

    [Fact]
    public async Task UserDataUsesTheDocumentedDirectResponseBody()
    {
        using var httpClient = new HttpClient(new HttpClientTestHandler((request, _) =>
        {
            Assert.Equal("/v2/servers/123/user_data", request.RequestUri!.AbsolutePath);
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"user_data\":\"#cloud-config\"}"),
            });
        }))
        {
            BaseAddress = new Uri("https://api.example.test/"),
        };
        var client = new BinaryLaneClient(httpClient, new StaticBinaryLaneTokenProvider("token"));

        var userData = await client.Servers.GetUserDataAsync(123);

        Assert.Equal("#cloud-config", userData.Value);
    }

    [Fact]
    public async Task ListEndpointsRejectNullItemsRatherThanSilentlyDroppingThem()
    {
        using var httpClient = new HttpClient(new HttpClientTestHandler((_, _) =>
            Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"meta\":{\"total\":1},\"servers\":[null]}"),
            })))
        {
            BaseAddress = new Uri("https://api.example.test/"),
        };
        var client = new BinaryLaneClient(httpClient, new StaticBinaryLaneTokenProvider("token"));

        var exception = await Assert.ThrowsAsync<JsonException>(() => client.Servers.ListAsync());

        Assert.Contains("servers", exception.Message, StringComparison.Ordinal);
    }
}
