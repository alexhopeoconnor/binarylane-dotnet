using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using BinaryLane.Api.V2.Authentication;
using BinaryLane.Api.V2.Errors;
using BinaryLane.Api.V2.Http;
using Xunit;

namespace BinaryLane.Api.Tests;

public sealed class BinaryLaneHttpExecutorTests
{
    [Fact]
    public async Task GetAsyncAddsBearerHeaderAndSerializesQueryValues()
    {
        HttpRequestMessage? received = null;
        using var httpClient = new HttpClient(new HttpClientTestHandler((request, _) =>
        {
            received = request;
            return Task.FromResult(JsonResponse(HttpStatusCode.OK, "{\"value\":\"ok\"}"));
        }))
        {
            BaseAddress = new Uri("https://api.example.test/"),
        };
        var executor = new BinaryLaneHttpExecutor(httpClient, new StaticBinaryLaneTokenProvider("test-token"));

        var response = await executor.GetAsync<JsonElement>(
            "v2/servers",
            new System.Collections.Generic.Dictionary<string, object?>
            {
                ["page"] = 2,
                ["include_archived"] = false,
            });

        Assert.Equal("ok", response.GetProperty("value").GetString());
        Assert.NotNull(received);
        Assert.Equal("Bearer", received!.Headers.Authorization!.Scheme);
        Assert.Equal("test-token", received.Headers.Authorization.Parameter);
        Assert.Equal("?page=2&include_archived=false", received.RequestUri!.Query);
    }

    [Fact]
    public async Task GetAsyncMapsUnauthorizedResponseWithoutLeakingBearerToken()
    {
        using var httpClient = new HttpClient(new HttpClientTestHandler((_, _) =>
            Task.FromResult(JsonResponse(HttpStatusCode.Unauthorized, "{\"title\":\"Unauthorized\",\"detail\":\"Invalid token\"}"))))
        {
            BaseAddress = new Uri("https://api.example.test/"),
        };
        var executor = new BinaryLaneHttpExecutor(httpClient, new StaticBinaryLaneTokenProvider("super-secret-token"));

        var exception = await Assert.ThrowsAsync<BinaryLaneUnauthorizedException>(
            () => executor.GetAsync<JsonElement>("v2/account"));

        Assert.Equal(HttpStatusCode.Unauthorized, exception.StatusCode);
        Assert.Equal("Invalid token", exception.Problem!.Detail);
        Assert.DoesNotContain("Invalid token", exception.Message, StringComparison.Ordinal);
        Assert.DoesNotContain("super-secret-token", exception.ToString(), StringComparison.Ordinal);
        Assert.DoesNotContain("super-secret-token", exception.ResponseBody ?? string.Empty, StringComparison.Ordinal);
    }

    [Fact]
    public async Task GetAsyncPropagatesCancellationToTheHttpHandler()
    {
        using var cancellation = new CancellationTokenSource();
        using var httpClient = new HttpClient(new HttpClientTestHandler(async (_, token) =>
        {
            await Task.Delay(Timeout.InfiniteTimeSpan, token);
            throw new InvalidOperationException("Unreachable.");
        }))
        {
            BaseAddress = new Uri("https://api.example.test/"),
        };
        var executor = new BinaryLaneHttpExecutor(httpClient, new StaticBinaryLaneTokenProvider("token"));

        cancellation.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            () => executor.GetAsync<JsonElement>("v2/account", cancellationToken: cancellation.Token));
    }

    [Fact]
    public async Task TokenProviderIsEvaluatedForEveryRequestSoTokensCanRotate()
    {
        var authorizationTokens = new List<string?>();
        using var httpClient = new HttpClient(new HttpClientTestHandler((request, _) =>
        {
            authorizationTokens.Add(request.Headers.Authorization?.Parameter);
            return Task.FromResult(JsonResponse(HttpStatusCode.OK, "{}"));
        }))
        {
            BaseAddress = new Uri("https://api.example.test/"),
        };
        var executor = new BinaryLaneHttpExecutor(
            httpClient,
            new RotatingTokenProvider("first-token", "second-token"));

        await executor.GetAsync<JsonElement>("v2/account");
        await executor.GetAsync<JsonElement>("v2/account");

        Assert.Collection(
            authorizationTokens,
            token => Assert.Equal("first-token", token),
            token => Assert.Equal("second-token", token));
    }

    [Fact]
    public async Task QueryFormattingOmitsNullsAndUsesInvariantWireValues()
    {
        HttpRequestMessage? received = null;
        using var httpClient = new HttpClient(new HttpClientTestHandler((request, _) =>
        {
            received = request;
            return Task.FromResult(JsonResponse(HttpStatusCode.OK, "{}"));
        }))
        {
            BaseAddress = new Uri("https://api.example.test/"),
        };
        var executor = new BinaryLaneHttpExecutor(httpClient, new StaticBinaryLaneTokenProvider("token"));
        var timestamp = new DateTimeOffset(2026, 8, 16, 12, 34, 56, TimeSpan.Zero);

        await executor.GetAsync<JsonElement>(
            "v2/actions?existing=true",
            new Dictionary<string, object?>
            {
                ["after"] = timestamp,
                ["amount"] = 10.5m,
                ["omit_me"] = null,
            });

        Assert.NotNull(received);
        var query = received!.RequestUri!.Query;
        Assert.Contains("existing=true", query, StringComparison.Ordinal);
        Assert.Contains("after=2026-08-16T12%3A34%3A56.0000000%2B00%3A00", query, StringComparison.Ordinal);
        Assert.Contains("amount=10.5", query, StringComparison.Ordinal);
        Assert.DoesNotContain("omit_me", query, StringComparison.Ordinal);
    }

    [Fact]
    public async Task ValidationErrorsMapToValidationExceptionAndPreserveProblemDetails()
    {
        using var httpClient = new HttpClient(new HttpClientTestHandler((_, _) =>
            Task.FromResult(JsonResponse(
                (HttpStatusCode)422,
                "{\"title\":\"Validation failed\",\"detail\":\"region is required\",\"status\":422}"))))
        {
            BaseAddress = new Uri("https://api.example.test/"),
        };
        var executor = new BinaryLaneHttpExecutor(httpClient, new StaticBinaryLaneTokenProvider("token"));

        var exception = await Assert.ThrowsAsync<BinaryLaneValidationException>(
            () => executor.PostAsync<JsonElement>("v2/servers", new { }));

        Assert.Equal((HttpStatusCode)422, exception.StatusCode);
        Assert.Equal("Validation failed", exception.Problem!.Title);
        Assert.Equal("region is required", exception.Problem.Detail);
    }

    [Fact]
    public async Task MutationsAreNotRetriedImplicitly()
    {
        var requestCount = 0;
        using var httpClient = new HttpClient(new HttpClientTestHandler((_, _) =>
        {
            requestCount++;
            return Task.FromResult(JsonResponse(HttpStatusCode.InternalServerError, "{\"title\":\"Failure\"}"));
        }))
        {
            BaseAddress = new Uri("https://api.example.test/"),
        };
        var executor = new BinaryLaneHttpExecutor(httpClient, new StaticBinaryLaneTokenProvider("token"));

        await Assert.ThrowsAsync<BinaryLaneApiException>(
            () => executor.PostAsync<JsonElement>("v2/servers", new { name = "must-not-retry" }));

        Assert.Equal(1, requestCount);
    }

    [Fact]
    public async Task EmptyTokenFailsBeforeTheRequestIsDispatched()
    {
        var requestCount = 0;
        using var httpClient = new HttpClient(new HttpClientTestHandler((_, _) =>
        {
            requestCount++;
            return Task.FromResult(JsonResponse(HttpStatusCode.OK, "{}"));
        }))
        {
            BaseAddress = new Uri("https://api.example.test/"),
        };
        var executor = new BinaryLaneHttpExecutor(httpClient, new RotatingTokenProvider(string.Empty));

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => executor.GetAsync<JsonElement>("v2/account"));

        Assert.Equal(0, requestCount);
    }

    [Fact]
    public async Task AbsoluteUrlsOutsideTheConfiguredOriginDoNotReceiveTheBearerToken()
    {
        var requestCount = 0;
        using var httpClient = new HttpClient(new HttpClientTestHandler((_, _) =>
        {
            requestCount++;
            return Task.FromResult(JsonResponse(HttpStatusCode.OK, "{}"));
        }))
        {
            BaseAddress = new Uri("https://api.example.test/"),
        };
        var executor = new BinaryLaneHttpExecutor(httpClient, new StaticBinaryLaneTokenProvider("token"));

        await Assert.ThrowsAsync<ArgumentException>(
            () => executor.GetAsync<JsonElement>("https://other.example.test/v2/servers"));

        Assert.Equal(0, requestCount);
    }

    [Fact]
    public async Task NonHttpAbsoluteUrlsAreRejectedBeforeTheRequestIsDispatched()
    {
        var requestCount = 0;
        using var httpClient = new HttpClient(new HttpClientTestHandler((_, _) =>
        {
            requestCount++;
            return Task.FromResult(JsonResponse(HttpStatusCode.OK, "{}"));
        }))
        {
            BaseAddress = new Uri("https://api.example.test/"),
        };
        var executor = new BinaryLaneHttpExecutor(httpClient, new StaticBinaryLaneTokenProvider("token"));

        await Assert.ThrowsAsync<ArgumentException>(
            () => executor.GetAsync<JsonElement>("ftp://other.example.test/v2/servers"));

        Assert.Equal(0, requestCount);
    }

    [Fact]
    public async Task CallerCannotOverrideTheTokenProviderAuthorizationHeader()
    {
        var requestCount = 0;
        using var httpClient = new HttpClient(new HttpClientTestHandler((_, _) =>
        {
            requestCount++;
            return Task.FromResult(JsonResponse(HttpStatusCode.OK, "{}"));
        }))
        {
            BaseAddress = new Uri("https://api.example.test/"),
        };
        var executor = new BinaryLaneHttpExecutor(httpClient, new StaticBinaryLaneTokenProvider("token"));
        var request = new BinaryLaneRequest(HttpMethod.Get, "v2/account")
        {
            Headers = new Dictionary<string, string>
            {
                ["Authorization"] = "Bearer caller-token",
            },
        };

        await Assert.ThrowsAsync<ArgumentException>(() => executor.SendAsync<JsonElement>(request));

        Assert.Equal(0, requestCount);
    }

    [Fact]
    public async Task CallerCannotOverrideTheRequestAuthorityWithAHostHeader()
    {
        var requestCount = 0;
        using var httpClient = new HttpClient(new HttpClientTestHandler((_, _) =>
        {
            requestCount++;
            return Task.FromResult(JsonResponse(HttpStatusCode.OK, "{}"));
        }))
        {
            BaseAddress = new Uri("https://api.example.test/"),
        };
        var executor = new BinaryLaneHttpExecutor(httpClient, new StaticBinaryLaneTokenProvider("token"));
        var request = new BinaryLaneRequest(HttpMethod.Get, "v2/account")
        {
            Headers = new Dictionary<string, string>
            {
                ["Host"] = "other.example.test",
            },
        };

        await Assert.ThrowsAsync<ArgumentException>(() => executor.SendAsync<JsonElement>(request));

        Assert.Equal(0, requestCount);
    }

    [Fact]
    public async Task PlaintextBaseAddressesAreRejectedBeforeTheRequestIsDispatched()
    {
        var requestCount = 0;
        using var httpClient = new HttpClient(new HttpClientTestHandler((_, _) =>
        {
            requestCount++;
            return Task.FromResult(JsonResponse(HttpStatusCode.OK, "{}"));
        }))
        {
            BaseAddress = new Uri("http://api.example.test/"),
        };
        var executor = new BinaryLaneHttpExecutor(httpClient, new StaticBinaryLaneTokenProvider("token"));

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => executor.GetAsync<JsonElement>("v2/account"));

        Assert.Equal(0, requestCount);
    }

    [Fact]
    public async Task SuccessResponseBodiesLargerThanTheLimitAreRejectedBeforeBuffering()
    {
        using var httpClient = new HttpClient(new HttpClientTestHandler((_, _) =>
        {
            var content = new ByteArrayContent(Array.Empty<byte>());
            content.Headers.ContentLength = (16L * 1024 * 1024) + 1;
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK) { Content = content });
        }))
        {
            BaseAddress = new Uri("https://api.example.test/"),
        };
        var executor = new BinaryLaneHttpExecutor(httpClient, new StaticBinaryLaneTokenProvider("token"));

        var exception = await Assert.ThrowsAsync<HttpRequestException>(
            () => executor.GetAsync<JsonElement>("v2/account"));

        Assert.Contains("exceeded", exception.Message, StringComparison.Ordinal);
    }

    [Fact]
    public async Task ErrorResponseBodyIsBoundedBeforeItIsStoredOnTheException()
    {
        var responseBody = "{\"detail\":\"" + new string('x', 40_000) + "\"}";
        using var httpClient = new HttpClient(new HttpClientTestHandler((_, _) =>
            Task.FromResult(JsonResponse(HttpStatusCode.InternalServerError, responseBody))))
        {
            BaseAddress = new Uri("https://api.example.test/"),
        };
        var executor = new BinaryLaneHttpExecutor(httpClient, new StaticBinaryLaneTokenProvider("token"));

        var exception = await Assert.ThrowsAsync<BinaryLaneApiException>(
            () => executor.GetAsync<JsonElement>("v2/account"));

        Assert.NotNull(exception.ResponseBody);
        Assert.Equal(32_769, exception.ResponseBody!.Length);
        Assert.EndsWith("…", exception.ResponseBody, StringComparison.Ordinal);
    }

    private static HttpResponseMessage JsonResponse(HttpStatusCode statusCode, string json) =>
        new(statusCode)
        {
            Content = new StringContent(json),
        };

    private sealed class RotatingTokenProvider : IBinaryLaneTokenProvider
    {
        private readonly Queue<string> _tokens;

        public RotatingTokenProvider(params string[] tokens)
        {
            _tokens = new Queue<string>(tokens);
        }

        public ValueTask<string> GetTokenAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return new ValueTask<string>(_tokens.Dequeue());
        }
    }
}
