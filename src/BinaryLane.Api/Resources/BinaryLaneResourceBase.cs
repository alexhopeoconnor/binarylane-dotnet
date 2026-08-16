using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using BinaryLane.Api.V2.Http;
using BinaryLane.Api.V2.Pagination;

namespace BinaryLane.Api.V2.Resources;

/// <summary>Shared JSON-envelope and pagination behavior for resource APIs.</summary>
public abstract class BinaryLaneResourceBase
{
    /// <summary>Initializes a resource using the common request executor.</summary>
    protected BinaryLaneResourceBase(IBinaryLaneApiExecutor executor, BinaryLaneJsonSerializerOptions json)
    {
        Executor = executor ?? throw new ArgumentNullException(nameof(executor));
        Json = json ?? throw new ArgumentNullException(nameof(json));
    }

    /// <summary>Common request executor.</summary>
    protected IBinaryLaneApiExecutor Executor { get; }

    /// <summary>Common JSON options.</summary>
    protected BinaryLaneJsonSerializerOptions Json { get; }

    /// <summary>Gets a raw JSON response, allowing forward-compatible access to a newly added API field.</summary>
    protected Task<JsonElement> GetRawAsync(
        string path,
        IReadOnlyDictionary<string, object?>? query = null,
        CancellationToken cancellationToken = default) =>
        Executor.GetAsync<JsonElement>(path, query, cancellationToken);

    /// <summary>Sends a raw JSON mutation request.</summary>
    protected async Task<JsonElement> SendRawAsync(
        System.Net.Http.HttpMethod method,
        string path,
        object? body = null,
        IReadOnlyDictionary<string, object?>? query = null,
        CancellationToken cancellationToken = default)
    {
        var response = await Executor.SendAsync<JsonElement>(
            new BinaryLaneRequest(method, path) { Body = body, Query = query }, cancellationToken).ConfigureAwait(false);
        return response.Body;
    }

    /// <summary>Sends a raw request and retains the HTTP response metadata.</summary>
    protected Task<BinaryLaneResponse<JsonElement>> SendResponseAsync(
        System.Net.Http.HttpMethod method,
        string path,
        object? body = null,
        IReadOnlyDictionary<string, object?>? query = null,
        CancellationToken cancellationToken = default) =>
        Executor.SendAsync<JsonElement>(
            new BinaryLaneRequest(method, path) { Body = body, Query = query }, cancellationToken);

    /// <summary>Sends a request expected to have no response body.</summary>
    protected async Task SendNoContentAsync(
        System.Net.Http.HttpMethod method,
        string path,
        object? body = null,
        IReadOnlyDictionary<string, object?>? query = null,
        CancellationToken cancellationToken = default)
    {
        await Executor.SendAsync(
            new BinaryLaneRequest(method, path) { Body = body, Query = query }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>Gets a response whose value is wrapped in a named JSON property.</summary>
    protected async Task<T> GetItemAsync<T>(
        string path,
        string propertyName,
        CancellationToken cancellationToken = default)
    {
        var document = await GetRawAsync(path, null, cancellationToken).ConfigureAwait(false);
        return DeserializeEnvelope<T>(document, propertyName);
    }

    /// <summary>Gets a response whose JSON body is the requested value rather than an envelope.</summary>
    protected async Task<T> GetDirectItemAsync<T>(
        string path,
        CancellationToken cancellationToken = default)
    {
        var document = await GetRawAsync(path, null, cancellationToken).ConfigureAwait(false);
        var item = JsonSerializer.Deserialize<T>(document.GetRawText(), Json.SerializerOptions);
        if (item is null)
        {
            throw new JsonException("BinaryLane response did not contain a usable JSON value.");
        }

        return item;
    }

    /// <summary>Sends a mutation whose value is wrapped in a named JSON property.</summary>
    protected async Task<T> SendItemAsync<T>(
        System.Net.Http.HttpMethod method,
        string path,
        object? body,
        string propertyName,
        CancellationToken cancellationToken = default)
    {
        var document = await Executor.SendAsync<JsonElement>(
            new BinaryLaneRequest(method, path) { Body = body }, cancellationToken).ConfigureAwait(false);
        return DeserializeEnvelope<T>(document.Body, propertyName);
    }

    /// <summary>Gets a typed page whose items are wrapped in a named JSON property.</summary>
    protected async Task<Page<T>> GetPageAsync<T>(
        string path,
        string propertyName,
        PageRequest? page = null,
        IReadOnlyDictionary<string, object?>? query = null,
        CancellationToken cancellationToken = default)
    {
        var parameters = MergeQuery(query, page?.ToQuery());
        var document = await GetRawAsync(path, parameters, cancellationToken).ConfigureAwait(false);
        return DeserializePage<T>(document, propertyName);
    }

    /// <summary>Enumerates every available page without enabling retries for mutating requests.</summary>
    protected async IAsyncEnumerable<T> GetAllPagesAsync<T>(
        string path,
        string propertyName,
        PageRequest? page = null,
        IReadOnlyDictionary<string, object?>? query = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var seenNextPageLinks = new HashSet<string>(StringComparer.Ordinal);
        var current = await GetPageAsync<T>(path, propertyName, page, query, cancellationToken).ConfigureAwait(false);
        foreach (var item in current.Items)
        {
            yield return item;
        }

        while (current.NextPage is not null)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var nextPagePath = current.NextPage.IsAbsoluteUri
                ? current.NextPage.AbsoluteUri
                : current.NextPage.OriginalString;

            if (!seenNextPageLinks.Add(nextPagePath))
            {
                throw new InvalidOperationException(
                    $"BinaryLane returned a repeated pagination link: '{nextPagePath}'.");
            }

            current = await GetPageAsync<T>(
                nextPagePath,
                propertyName,
                null,
                null,
                cancellationToken).ConfigureAwait(false);

            foreach (var item in current.Items)
            {
                yield return item;
            }
        }
    }

    /// <summary>Deserializes an item from a standard BinaryLane JSON response envelope.</summary>
    protected T DeserializeEnvelope<T>(JsonElement document, string propertyName)
    {
        if (document.ValueKind != JsonValueKind.Object ||
            !document.TryGetProperty(propertyName, out var payload))
        {
            throw new JsonException($"BinaryLane response did not contain the expected '{propertyName}' envelope property.");
        }

        var item = JsonSerializer.Deserialize<T>(payload.GetRawText(), Json.SerializerOptions);
        if (item is null)
        {
            throw new JsonException($"BinaryLane response did not contain a usable '{propertyName}' value.");
        }

        return item;
    }

    /// <summary>Attempts to deserialize a value only when the named envelope property is present.</summary>
    protected bool TryDeserializeEnvelope<T>(JsonElement document, string propertyName, out T? item)
    {
        item = default;
        if (document.ValueKind != JsonValueKind.Object || !document.TryGetProperty(propertyName, out var payload))
        {
            return false;
        }

        item = JsonSerializer.Deserialize<T>(payload.GetRawText(), Json.SerializerOptions);
        return item is not null;
    }

    /// <summary>Deserializes a standard BinaryLane list response.</summary>
    protected Page<T> DeserializePage<T>(JsonElement document, string propertyName)
    {
        if (document.ValueKind != JsonValueKind.Object ||
            !document.TryGetProperty(propertyName, out var collection) ||
            collection.ValueKind != JsonValueKind.Array)
        {
            throw new JsonException($"BinaryLane response did not contain the expected '{propertyName}' array.");
        }

        var items = new List<T>();
        foreach (var value in collection.EnumerateArray())
        {
            var item = JsonSerializer.Deserialize<T>(value.GetRawText(), Json.SerializerOptions);
            if (item is null)
            {
                throw new JsonException(
                    $"BinaryLane response contained an unusable item in the '{propertyName}' array.");
            }

            items.Add(item);
        }

        var total = items.Count;
        if (document.TryGetProperty("meta", out var meta) &&
            meta.ValueKind == JsonValueKind.Object &&
            meta.TryGetProperty("total", out var totalValue) &&
            totalValue.TryGetInt32(out var providedTotal))
        {
            total = providedTotal;
        }

        Uri? first = null;
        Uri? previous = null;
        Uri? next = null;
        Uri? last = null;
        if (document.TryGetProperty("links", out var links) &&
            links.ValueKind == JsonValueKind.Object &&
            links.TryGetProperty("pages", out var pages) &&
            pages.ValueKind == JsonValueKind.Object)
        {
            first = ReadUri(pages, "first");
            previous = ReadUri(pages, "prev");
            next = ReadUri(pages, "next");
            last = ReadUri(pages, "last");
        }

        return new Page<T>(items, total, first, previous, next, last);
    }

    private static Uri? ReadUri(JsonElement element, string name)
    {
        if (!element.TryGetProperty(name, out var value) || value.ValueKind != JsonValueKind.String)
        {
            return null;
        }

        return Uri.TryCreate(value.GetString(), UriKind.RelativeOrAbsolute, out var uri) ? uri : null;
    }

    /// <summary>Escapes an arbitrary API path segment.</summary>
    protected static string EscapePathSegment(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("A path segment is required.", nameof(value));
        }

        return Uri.EscapeDataString(value);
    }

    private static Dictionary<string, object?>? MergeQuery(
        IReadOnlyDictionary<string, object?>? primary,
        IReadOnlyDictionary<string, object?>? secondary)
    {
        if (primary is null && secondary is null)
        {
            return null;
        }

        var result = new Dictionary<string, object?>();
        if (primary is not null)
        {
            foreach (var pair in primary)
            {
                result[pair.Key] = pair.Value;
            }
        }

        if (secondary is not null)
        {
            foreach (var pair in secondary)
            {
                result[pair.Key] = pair.Value;
            }
        }

        return result;
    }
}
