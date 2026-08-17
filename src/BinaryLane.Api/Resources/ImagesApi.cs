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

/// <summary>Reads and manages account images and backups represented as images.</summary>
public interface IImagesApi
{
    Task<Page<Image>> ListAsync(PageRequest? page = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<Image> ListAllAsync(PageRequest? page = null, CancellationToken cancellationToken = default);
    Task<Image> GetAsync(string imageIdOrSlug, CancellationToken cancellationToken = default);
    Task<Image> UpdateAsync(long imageId, ImageRequest request, CancellationToken cancellationToken = default);
    Task<ImageDownload> GetDownloadAsync(long imageId, CancellationToken cancellationToken = default);
}

/// <inheritdoc />
public sealed class ImagesApi : BinaryLaneResourceBase, IImagesApi
{
    /// <summary>Initializes the images resource.</summary>
    public ImagesApi(IBinaryLaneApiExecutor executor, BinaryLaneJsonSerializerOptions json)
        : base(executor, json)
    {
    }

    /// <inheritdoc />
    public Task<Page<Image>> ListAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetPageAsync<Image>("v2/images", "images", page, null, cancellationToken);

    /// <inheritdoc />
    public IAsyncEnumerable<Image> ListAllAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetAllPagesAsync<Image>("v2/images", "images", page, null, cancellationToken);

    /// <inheritdoc />
    public Task<Image> GetAsync(string imageIdOrSlug, CancellationToken cancellationToken = default) =>
        GetItemAsync<Image>($"v2/images/{EscapePathSegment(imageIdOrSlug)}", "image", cancellationToken);

    /// <inheritdoc />
    public Task<Image> UpdateAsync(long imageId, ImageRequest request, CancellationToken cancellationToken = default) =>
        SendItemAsync<Image>(HttpMethod.Put, $"v2/images/{imageId}", request ?? throw new ArgumentNullException(nameof(request)), "image", cancellationToken);

    /// <inheritdoc />
    public Task<ImageDownload> GetDownloadAsync(long imageId, CancellationToken cancellationToken = default) =>
        GetItemAsync<ImageDownload>($"v2/images/{imageId}/download", "link", cancellationToken);
}
