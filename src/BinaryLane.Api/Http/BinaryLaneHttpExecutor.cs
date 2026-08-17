using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using BinaryLane.Api.V2.Authentication;
using BinaryLane.Api.V2.Errors;

namespace BinaryLane.Api.V2.Http;

/// <summary>Default HTTP implementation for the BinaryLane API.</summary>
public sealed class BinaryLaneHttpExecutor : IBinaryLaneApiExecutor
{
    private const int MaximumDiagnosticBodyLength = 32 * 1024;
    private const long MaximumSuccessBodyLength = 16L * 1024 * 1024;
    private static readonly Uri DefaultBaseUri = new("https://api.binarylane.com.au/");
    private readonly HttpClient _httpClient;
    private readonly IBinaryLaneTokenProvider _tokenProvider;
    private readonly BinaryLaneJsonSerializerOptions _json;

    /// <summary>Initializes the executor.</summary>
    public BinaryLaneHttpExecutor(
        HttpClient httpClient,
        IBinaryLaneTokenProvider tokenProvider,
        BinaryLaneJsonSerializerOptions? json = null)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _tokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));
        _json = json ?? new BinaryLaneJsonSerializerOptions();
    }

    /// <inheritdoc />
    public async Task<BinaryLaneResponse<TResponse>> SendAsync<TResponse>(
        BinaryLaneRequest request,
        CancellationToken cancellationToken = default)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(request);
#else
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }
#endif

        using var message = await CreateMessageAsync(request, cancellationToken).ConfigureAwait(false);
        using var response = await _httpClient
            .SendAsync(message, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
            .ConfigureAwait(false);

        var headers = CollectHeaders(response);
        var body = response.Content is null
            ? string.Empty
            : response.IsSuccessStatusCode
                ? await ReadBodyAsync(response.Content, cancellationToken).ConfigureAwait(false)
                : await ReadDiagnosticBodyAsync(response.Content, cancellationToken).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
            throw CreateException(response.StatusCode, message.RequestUri!, body, headers);
        }

        var result = Deserialize<TResponse>(body);
        return new BinaryLaneResponse<TResponse>(result, response.StatusCode, message.RequestUri!, headers);
    }

    /// <inheritdoc />
    public async Task<BinaryLaneResponse<object?>> SendAsync(
        BinaryLaneRequest request,
        CancellationToken cancellationToken = default)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(request);
#else
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }
#endif

        using var message = await CreateMessageAsync(request, cancellationToken).ConfigureAwait(false);
        using var response = await _httpClient
            .SendAsync(message, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
            .ConfigureAwait(false);

        var headers = CollectHeaders(response);
        var body = response.Content is null
            ? string.Empty
            : response.IsSuccessStatusCode
                ? await ReadBodyAsync(response.Content, cancellationToken).ConfigureAwait(false)
                : await ReadDiagnosticBodyAsync(response.Content, cancellationToken).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
            throw CreateException(response.StatusCode, message.RequestUri!, body, headers);
        }

        return new BinaryLaneResponse<object?>(null, response.StatusCode, message.RequestUri!, headers);
    }

    /// <inheritdoc />
    public async Task<TResponse> GetAsync<TResponse>(
        string path,
        IReadOnlyDictionary<string, object?>? query = null,
        CancellationToken cancellationToken = default) =>
        (await SendAsync<TResponse>(new BinaryLaneRequest(HttpMethod.Get, path) { Query = query }, cancellationToken)
            .ConfigureAwait(false)).Body;

    /// <inheritdoc />
    public async Task<TResponse> PostAsync<TResponse>(
        string path,
        object? body = null,
        IReadOnlyDictionary<string, object?>? query = null,
        CancellationToken cancellationToken = default) =>
        (await SendAsync<TResponse>(new BinaryLaneRequest(HttpMethod.Post, path) { Body = body, Query = query }, cancellationToken)
            .ConfigureAwait(false)).Body;

    /// <inheritdoc />
    public async Task<TResponse> PutAsync<TResponse>(
        string path,
        object? body = null,
        IReadOnlyDictionary<string, object?>? query = null,
        CancellationToken cancellationToken = default) =>
        (await SendAsync<TResponse>(new BinaryLaneRequest(HttpMethod.Put, path) { Body = body, Query = query }, cancellationToken)
            .ConfigureAwait(false)).Body;

    /// <inheritdoc />
    public async Task<TResponse> PatchAsync<TResponse>(
        string path,
        object? body = null,
        IReadOnlyDictionary<string, object?>? query = null,
        CancellationToken cancellationToken = default) =>
        (await SendAsync<TResponse>(new BinaryLaneRequest(BinaryLaneHttpMethods.Patch, path) { Body = body, Query = query }, cancellationToken)
            .ConfigureAwait(false)).Body;

    /// <inheritdoc />
    public async Task DeleteAsync(
        string path,
        object? body = null,
        IReadOnlyDictionary<string, object?>? query = null,
        CancellationToken cancellationToken = default)
    {
        await SendAsync(new BinaryLaneRequest(HttpMethod.Delete, path) { Body = body, Query = query }, cancellationToken)
            .ConfigureAwait(false);
    }

    private async Task<HttpRequestMessage> CreateMessageAsync(BinaryLaneRequest request, CancellationToken cancellationToken)
    {
        var requestUri = CreateUri(request.Path, request.Query);
        var token = await _tokenProvider.GetTokenAsync(cancellationToken).ConfigureAwait(false);
        if (string.IsNullOrWhiteSpace(token))
        {
            throw new InvalidOperationException("The configured BinaryLane token provider returned an empty token.");
        }

        var message = new HttpRequestMessage(request.Method, requestUri);
        try
        {
            if (request.Headers is not null)
            {
                foreach (var header in request.Headers)
                {
                    if (string.Equals(header.Key, "Authorization", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(header.Key, "Host", StringComparison.OrdinalIgnoreCase))
                    {
                        throw new ArgumentException(
                            $"The '{header.Key}' header is managed by the BinaryLane client and cannot be overridden.",
                            nameof(request));
                    }

                    try
                    {
                        message.Headers.Add(header.Key, header.Value);
                    }
                    catch (Exception exception) when (exception is FormatException or InvalidOperationException)
                    {
                        throw new ArgumentException(
                            $"The '{header.Key}' header is not a valid request header for BinaryLane API requests.",
                            nameof(request),
                            exception);
                    }
                }
            }

            message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            if (request.Body is not null)
            {
                var json = JsonSerializer.Serialize(request.Body, _json.SerializerOptions);
                message.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            return message;
        }
        catch
        {
            message.Dispose();
            throw;
        }
    }

    private Uri CreateUri(string path, IReadOnlyDictionary<string, object?>? query)
    {
        var baseUri = _httpClient.BaseAddress ?? DefaultBaseUri;
        if (!baseUri.IsAbsoluteUri ||
            baseUri.Scheme != Uri.UriSchemeHttps ||
            !string.IsNullOrEmpty(baseUri.UserInfo) ||
            !string.IsNullOrEmpty(baseUri.Query) ||
            !string.IsNullOrEmpty(baseUri.Fragment))
        {
            throw new InvalidOperationException(
                "HttpClient.BaseAddress must be an absolute HTTPS URL without credentials, a query string, or a fragment.");
        }

        Uri uri;
        if (path[0] != '/' &&
            Uri.TryCreate(path, UriKind.Absolute, out var absoluteUri))
        {
            if (absoluteUri.Scheme != Uri.UriSchemeHttps)
            {
                throw new ArgumentException(
                    "Absolute BinaryLane API URLs must use the HTTPS scheme.",
                    nameof(path));
            }

            if (!HasSameOrigin(baseUri, absoluteUri))
            {
                throw new ArgumentException(
                    "Absolute BinaryLane API URLs must use the configured API origin.",
                    nameof(path));
            }

            uri = absoluteUri;
        }
        else
        {
            uri = new Uri(baseUri, path.TrimStart('/'));
            if (!HasSameOrigin(baseUri, uri))
            {
                throw new ArgumentException(
                    "Relative BinaryLane API paths must resolve on the configured API origin.",
                    nameof(path));
            }
        }

        if (query is null || query.Count == 0)
        {
            return uri;
        }

        var builder = new UriBuilder(uri);
        var queryBuilder = new StringBuilder();
        if (!string.IsNullOrWhiteSpace(builder.Query))
        {
            queryBuilder.Append(builder.Query.TrimStart('?'));
        }

        foreach (var pair in query)
        {
            if (pair.Value is null)
            {
                continue;
            }

            if (queryBuilder.Length > 0)
            {
                queryBuilder.Append('&');
            }

            queryBuilder.Append(Uri.EscapeDataString(pair.Key));
            queryBuilder.Append('=');
            queryBuilder.Append(Uri.EscapeDataString(FormatQueryValue(pair.Value)));
        }

        builder.Query = queryBuilder.ToString();
        return builder.Uri;
    }

    private static bool HasSameOrigin(Uri first, Uri second) =>
        string.Equals(first.Scheme, second.Scheme, StringComparison.OrdinalIgnoreCase) &&
        string.Equals(first.Host, second.Host, StringComparison.OrdinalIgnoreCase) &&
        first.Port == second.Port;

    private static string FormatQueryValue(object value)
    {
        if (value is bool boolean)
        {
            return boolean ? "true" : "false";
        }

        if (value is DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.ToString("O", CultureInfo.InvariantCulture);
        }

        if (value is DateTime dateTime)
        {
            return dateTime.ToString("O", CultureInfo.InvariantCulture);
        }

        if (value is IFormattable formattable)
        {
            return formattable.ToString(null, CultureInfo.InvariantCulture) ?? string.Empty;
        }

        return value.ToString() ?? string.Empty;
    }

    private TResponse Deserialize<TResponse>(string body)
    {
        if (string.IsNullOrWhiteSpace(body))
        {
            return default!;
        }

        if (typeof(TResponse) == typeof(JsonElement))
        {
            using var document = JsonDocument.Parse(body);
            return (TResponse)(object)document.RootElement.Clone();
        }

        var value = JsonSerializer.Deserialize<TResponse>(body, _json.SerializerOptions);
        if (value is null && typeof(TResponse).IsValueType)
        {
            throw new JsonException("BinaryLane returned a JSON null for a non-nullable response value.");
        }

        return value!;
    }

    private static ReadOnlyDictionary<string, IReadOnlyList<string>> CollectHeaders(HttpResponseMessage response)
    {
        var result = new Dictionary<string, IReadOnlyList<string>>(StringComparer.OrdinalIgnoreCase);
        foreach (var header in response.Headers)
        {
            result[header.Key] = new List<string>(header.Value).AsReadOnly();
        }

        if (response.Content is not null)
        {
            foreach (var header in response.Content.Headers)
            {
                result[header.Key] = new List<string>(header.Value).AsReadOnly();
            }
        }

        return new ReadOnlyDictionary<string, IReadOnlyList<string>>(result);
    }

    private static BinaryLaneApiException CreateException(
        HttpStatusCode statusCode,
        Uri requestUri,
        string body,
        IReadOnlyDictionary<string, IReadOnlyList<string>> headers)
    {
        var problem = ParseProblem(body);
        var message = $"BinaryLane API request failed with HTTP {(int)statusCode} ({statusCode}).";
        var diagnosticBody = SanitizeAndTruncate(body);

        if (statusCode == HttpStatusCode.BadRequest || statusCode == (HttpStatusCode)422)
        {
            return new BinaryLaneValidationException(message, statusCode, requestUri, problem, diagnosticBody, headers);
        }

        return statusCode switch
        {
            HttpStatusCode.Unauthorized => new BinaryLaneUnauthorizedException(message, requestUri, problem, diagnosticBody, headers),
            HttpStatusCode.Forbidden => new BinaryLaneForbiddenException(message, requestUri, problem, diagnosticBody, headers),
            HttpStatusCode.NotFound => new BinaryLaneNotFoundException(message, requestUri, problem, diagnosticBody, headers),
            _ => new BinaryLaneApiException(message, statusCode, requestUri, problem, diagnosticBody, headers),
        };
    }

    private static BinaryLaneApiProblem? ParseProblem(string body)
    {
        if (string.IsNullOrWhiteSpace(body))
        {
            return null;
        }

        try
        {
            using var document = JsonDocument.Parse(body);
            var root = document.RootElement;
            if (root.ValueKind != JsonValueKind.Object)
            {
                return null;
            }

            return new BinaryLaneApiProblem
            {
                Type = ReadString(root, "type"),
                Title = ReadString(root, "title"),
                Detail = ReadString(root, "detail"),
                Status = ReadInt32(root, "status"),
            };
        }
        catch (JsonException)
        {
            return null;
        }
    }

    private static string? ReadString(JsonElement element, string name) =>
        element.TryGetProperty(name, out var value) && value.ValueKind == JsonValueKind.String
            ? value.GetString()
            : null;

    private static int? ReadInt32(JsonElement element, string name) =>
        element.TryGetProperty(name, out var value) && value.TryGetInt32(out var number)
            ? number
            : null;

    private static string? SanitizeAndTruncate(string body)
    {
        if (string.IsNullOrWhiteSpace(body))
        {
            return null;
        }

        // Do not expose an unbounded provider response in exception telemetry. Consumers should still
        // treat this diagnostic field as potentially sensitive and avoid logging it indiscriminately.
        if (body.Length <= MaximumDiagnosticBodyLength)
        {
            return body;
        }

#if NET8_0_OR_GREATER
        return string.Concat(body.AsSpan(0, MaximumDiagnosticBodyLength), "…".AsSpan());
#else
        return body.Substring(0, MaximumDiagnosticBodyLength) + "…";
#endif
    }

    private static async Task<string> ReadBodyAsync(HttpContent content, CancellationToken cancellationToken)
    {
        if (content.Headers.ContentLength is long contentLength && contentLength > MaximumSuccessBodyLength)
        {
            throw new HttpRequestException(
                $"BinaryLane response content exceeded the {MaximumSuccessBodyLength} byte limit.");
        }

#if NET8_0_OR_GREATER
        await content.LoadIntoBufferAsync(MaximumSuccessBodyLength).ConfigureAwait(false);
        return await content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
#else
        await content.LoadIntoBufferAsync(MaximumSuccessBodyLength).ConfigureAwait(false);
        return await content.ReadAsStringAsync().ConfigureAwait(false);
#endif
    }

    private static async Task<string> ReadDiagnosticBodyAsync(HttpContent content, CancellationToken cancellationToken)
    {
#if NET8_0_OR_GREATER
        using var stream = await content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
#else
        using var stream = await content.ReadAsStreamAsync().ConfigureAwait(false);
#endif
        using var reader = new StreamReader(stream, Encoding.UTF8, true, 4096, leaveOpen: false);
        var buffer = new char[MaximumDiagnosticBodyLength + 1];
        var count = 0;

        while (count < buffer.Length)
        {
#if NET8_0_OR_GREATER
            var read = await reader
                .ReadAsync(buffer.AsMemory(count, buffer.Length - count), cancellationToken)
                .ConfigureAwait(false);
#else
            var read = await reader
                .ReadAsync(buffer, count, buffer.Length - count)
                .ConfigureAwait(false);
#endif
            if (read == 0)
            {
                break;
            }

            count += read;
        }

        return new string(buffer, 0, count);
    }
}
